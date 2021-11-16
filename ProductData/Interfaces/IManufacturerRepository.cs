using ProductData.Models;
using System.Collections.Generic;

namespace ProductData.Interfaces
{
    public interface IManufacturerRepository
    {
        IEnumerable<Manufacturer> GetAllManufacturers();
        Manufacturer GetManufacturerById(int id);
        void AddManufacturer(Manufacturer manufacturer);
        void UpdateManufacturer(Manufacturer manufacturer);
    }
}
