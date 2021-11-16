using ProductData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBusiness.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        void AddProduct(Product product);
        Product GetProductByID(int id);
        void UpdateProduct(Product product);
        bool ValidateProductNameNotEqualToManufacturerName(string name, int id);
    }
}
