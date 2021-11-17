using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductBusiness.Interfaces;
using ProductData.Models;
using ProductWebsiteMVC.Validation;
using ProductWebsiteMVC.ViewModels;

namespace ProductWebsiteMVC.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private readonly IManufacturerService _manufacturerService;

        public ProductController(IProductService productService, IManufacturerService manufacturerService, IProductValidator productValidator)
        {
            _productService = productService;
            _manufacturerService = manufacturerService;
            _ = productValidator;
        }

        public IActionResult Index()
        {
            var model = _productService.GetAllProducts();
            return View("Index", model);
        }
        public IActionResult Create()
        {
            var model = new ProductViewModel();
            PopulateManufacturerDropDown(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            try
            {
                PopulateManufacturerDropDown(productViewModel);
                if (!ModelState.IsValid)
                {
                    return View(productViewModel);
                }

                _productService.AddProduct(new Product { Name = productViewModel.Name, Description = productViewModel.Description, ManufacturerId = productViewModel.ManufacturerId });
                return Index();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(productViewModel);
            }
        }
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            var model = new ProductViewModel { Name = product.Name, Description = product.Description, ManufacturerId = product.ManufacturerId };
            PopulateManufacturerDropDown(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductViewModel productViewModel)
        {
            try
            {
                PopulateManufacturerDropDown(productViewModel);
                if (!ModelState.IsValid)
                {
                    return View(productViewModel);
                }

                _productService.UpdateProduct(new Product { Id = id,  Name = productViewModel.Name, Description = productViewModel.Description, ManufacturerId = productViewModel.ManufacturerId });
                return Index();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(productViewModel);
            }
        }

        private void PopulateManufacturerDropDown(ProductViewModel model)
        {
            model.Manufacturers.AddRange(_manufacturerService.GetAllManufacturers().Select(m => new SelectListItem(m.Name, m.Id.ToString())));
        }
    }
}
