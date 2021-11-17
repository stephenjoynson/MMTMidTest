using ProductBusiness.Interfaces;
using ProductData.Interfaces;
using ProductData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductBusiness.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        public ProductService(IProductRepository productRepository, IManufacturerRepository manufacturerRepository)
        {
            _productRepository = productRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        public void AddProduct(Product product)
        {
            if(_productRepository.GetProductByName(product.Name)!=null)
            {
                throw new Exception("Product Already Exists");
            }
            _productRepository.AddProduct(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            var allProducts = products as Product[] ?? products.ToArray();
            foreach(var product in allProducts)
            {
                product.ManufacturerName = _manufacturerRepository.GetManufacturerById(product.ManufacturerId)?.Name;
            }
            return allProducts;
        }

        public Product GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            product.ManufacturerName = _manufacturerRepository.GetManufacturerById(product.ManufacturerId)?.Name;
            return product;
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
        }

        public bool ValidateProductNameDoesNotContainToManufacturerName(string name, int id)
        {
            return !name.Contains( _manufacturerRepository.GetManufacturerById(id).Name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
