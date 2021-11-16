using System;
using System.Collections.Generic;
using ProductBusiness.Interfaces;
using ProductData.Interfaces;
using ProductData.Models;

namespace ProductBusiness.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        public ManufacturerService(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public IEnumerable<Manufacturer> GetAllManufacturers()
        {
            return _manufacturerRepository.GetAllManufacturers();
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                throw new ArgumentNullException(nameof(manufacturer),
                    "Parameter manufacturer of AddManufacturer must not be null");
            }
            _manufacturerRepository.AddManufacturer(manufacturer);
        }

        public Manufacturer GetManufacturerById(int id)
        {
            return _manufacturerRepository.GetManufacturerById(id);
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                throw new ArgumentNullException(nameof(manufacturer),
                    "Parameter manufacturer of AddManufacturer must not be null");
            }
            _manufacturerRepository.UpdateManufacturer(manufacturer);
        }
    }
}