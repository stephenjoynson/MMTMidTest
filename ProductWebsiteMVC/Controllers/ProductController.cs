using Microsoft.AspNetCore.Mvc;
using ProductBusiness.Interfaces;
using ProductData.Models;
using ProductWebsiteMVC.ViewModels;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductWebsiteMVC.Extensions;

namespace ProductWebsiteMVC
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private readonly IManufacturerService _manufacturerService;

        public ProductController(IProductService productService, IManufacturerService manufacturerService)
        {
            _productService = productService;
            _manufacturerService = manufacturerService;
            ProductValidator.SetProductService(_productService);
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
                if (ModelState.IsValid)
                {
                    _productService.AddProduct(new Product { Name = productViewModel.Name, Description = productViewModel.Description, ManufacturerId = productViewModel.ManufacturerId });
                    return Index();
                }
                return View(productViewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(productViewModel);
            }
        }
        public IActionResult Edit(int Id)
        {
            var product = _productService.GetProductByID(Id);
            var model = new ProductViewModel { Name = product.Name, Description = product.Description, ManufacturerId = product.ManufacturerId };
            PopulateManufacturerDropDown(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, ProductViewModel productViewModel)
        {
            try
            {
                PopulateManufacturerDropDown(productViewModel);
                if (ModelState.IsValid)
                {
                    _productService.UpdateProduct(new Product { Id = Id,  Name = productViewModel.Name, Description = productViewModel.Description, ManufacturerId = productViewModel.ManufacturerId });
                    return Index();
                }
                return View(productViewModel);
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
