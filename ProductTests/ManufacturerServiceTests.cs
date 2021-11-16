using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductBusiness.Interfaces;
using ProductBusiness.Services;
using ProductData.Interfaces;

namespace ProductTests
{
    [TestClass]
    public class ManufacturerServiceTests
    {
        private Mock<IManufacturerRepository> _mockManufacturerRepository;
        private IManufacturerService _manufacturerService;

        [TestInitialize]
        public void Setup()
        {
            _mockManufacturerRepository = new Mock<IManufacturerRepository>();
            _manufacturerService = new ManufacturerService(_mockManufacturerRepository.Object);
        }

        [TestMethod]
        public void When_Manufacturer_Is_Null__Then_AddManufacturer_Throws_ArgumentNullException()
        {
            // Arrange

            // Act

            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => _manufacturerService.AddManufacturer(null));
        }

        [TestMethod]
        public void When_Manufacturer_Is_Null__Then_UpdateManufacturer_Throws_ArgumentNullException()
        {
            // Arrange

            // Act

            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => _manufacturerService.UpdateManufacturer(null));
        }
    }
}
