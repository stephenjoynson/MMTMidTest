using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductData.Interfaces;
using ProductBusiness.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ProductData.Models;

namespace ProductTests
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockRepo;
        [TestInitialize]
        public void Setup()
        {
            var products = new List<Product>();
            products.Add(new Product { Id = 1, Name = "One", Description = "First", ManufacturerId = 1 });
            products.Add(new Product { Id = 2, Name = "Two", Description = "Second", ManufacturerId = 1 });
            products.Add(new Product { Id = 3, Name = "Three", Description = "Third", ManufacturerId = 2 });
            products.Add(new Product { Id = 4, Name = "Four", Description = "Fourth", ManufacturerId = 2 });
            _mockRepo = new Mock<IProductRepository>();
            _mockRepo.Setup(r => r.GetProductByName("One")).Returns(products.ToArray()[0]);
            _mockRepo.Setup(r => r.GetProductByName("Two")).Returns(products.ToArray()[1]);
            _mockRepo.Setup(r => r.GetProductByName("Three")).Returns(products.ToArray()[2]);
            _mockRepo.Setup(r => r.GetProductByName("Four")).Returns(products.ToArray()[3]);

            _mockRepo.Setup(r => r.GetAllProducts()).Returns(products);

        }
        [TestMethod]
        public void ValidNewProductIsAdded()
        {
            _mockRepo.Setup(r => r.AddProduct(It.IsAny<Product>())).Verifiable();

            var productService = new ProductService(_mockRepo.Object, new Mock<IManufacturerRepository>().Object);

            productService.AddProduct(new Product { Id = 5, Name = "Five", Description = "Fifth", ManufacturerId = 2 });

            _mockRepo.Verify();
        }
        [TestMethod]
        public void DuplicateProductIsNotAdded()
        {
            _mockRepo.Setup(r => r.AddProduct(It.IsAny<Product>())).Verifiable();

            var productService = new ProductService(_mockRepo.Object, new Mock<IManufacturerRepository>().Object);

            Assert.ThrowsException<Exception>(delegate { productService.AddProduct(new Product { Id = 5, Name = "Four", Description = "Fifth", ManufacturerId = 2 }); });
            
            _mockRepo.Verify(r=>r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void When_Name_Does_Not_Match_Manufacturer__Then_ValidateProductNameNotEqualToManufacturerName_Returns_True()
        { 

            var mockManufacturerRepository = new Mock<IManufacturerRepository>();
            mockManufacturerRepository.Setup(r => r.GetManufacturerById(4)).Returns(new Manufacturer{Name = "Any Name"});

            var productService = new ProductService(_mockRepo.Object, mockManufacturerRepository.Object);

            var result = productService.ValidateProductNameNotEqualToManufacturerName("Four", 4);
            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void When_Name_Matches_Manufacturer__Then_ValidateProductNameNotEqualToManufacturerName_Returns_False()
        {

            var mockManufacturerRepository = new Mock<IManufacturerRepository>();
            mockManufacturerRepository.Setup(r => r.GetManufacturerById(4)).Returns(new Manufacturer { Name = "Four" });

            var productService = new ProductService(_mockRepo.Object, mockManufacturerRepository.Object);

            var result = productService.ValidateProductNameNotEqualToManufacturerName("Four", 4);

            Assert.IsFalse(result);
        }
    }
}
