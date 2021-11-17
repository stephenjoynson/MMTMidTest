using ProductData.Models;
using System.Collections.Generic;

namespace ProductBusiness.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        void AddProduct(Product product);
        Product GetProductById(int id);
        void UpdateProduct(Product product);
        bool ValidateProductNameDoesNotContainToManufacturerName(string name, int id);
    }
}
