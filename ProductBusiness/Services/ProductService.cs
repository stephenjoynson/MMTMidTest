using ProductBusiness.Interfaces;
using ProductData.Interfaces;
using ProductData.Models;
using System;
using System.Collections.Generic;

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
            foreach(var product in products)
            {
                product.ManufacturerName = _manufacturerRepository.GetManufacturerById(product.ManufacturerId)?.Name;
            }
            return products;
        }

        public Product GetProductByID(int id)
        {
            var product = _productRepository.GetProductByID(id);
            product.ManufacturerName = _manufacturerRepository.GetManufacturerById(product.ManufacturerId)?.Name;
            return product;
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
        }

        public bool ValidateProductNameNotEqualToManufacturerName(string name, int id)
        {
            return string.Compare(name, _manufacturerRepository.GetManufacturerById(id).Name, StringComparison.InvariantCultureIgnoreCase) > 0;
        }
    }
}
