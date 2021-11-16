using JsonFlatFileDataStore;
using ProductData.Interfaces;
using ProductData.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductData.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly DataStore _store;
        public ManufacturerRepository()
        {
            _store = new DataStore("data.json");
        }
        public IEnumerable<Manufacturer> GetAllManufacturers()
        {
            var collection = _store.GetCollection<Manufacturer>();
            return collection.AsQueryable().OrderBy(c => c.Name);
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            var collection = _store.GetCollection<Manufacturer>();
            manufacturer.Id = collection.GetNextIdValue();
            collection.InsertOne(manufacturer);
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            var collection = _store.GetCollection<Manufacturer>();
            collection.UpdateOne(p => p.Id == manufacturer.Id, manufacturer);
        }

        public Manufacturer GetManufacturerById(int id)
        {
            var collection = _store.GetCollection<Manufacturer>();
            return collection.AsQueryable().FirstOrDefault(m => m.Id == id);
        }
    }
}
