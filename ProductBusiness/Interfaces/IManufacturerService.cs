using System.Collections.Generic;
using ProductData.Models;

namespace ProductBusiness.Interfaces
{
    public interface IManufacturerService
    {
        IEnumerable<Manufacturer> GetAllManufacturers();
        void AddManufacturer(Manufacturer manufacturer);
        Manufacturer GetManufacturerById(int id);
        void UpdateManufacturer(Manufacturer manufacturer);
    }
}