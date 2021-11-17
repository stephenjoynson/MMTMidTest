using ProductData.Models;
using System.Collections.Generic;

namespace ProductData.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        Product GetProductById(int id);
        Product GetProductByName(string name);
    }
}
