using System;
using Microsoft.AspNetCore.Mvc;
using ProductBusiness.Interfaces;
using ProductData.Models;
using ProductWebsiteMVC.ViewModels;

namespace ProductWebsiteMVC.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService _manufacturerService;
        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }
        public IActionResult Index()
        {
            var model = _manufacturerService.GetAllManufacturers();
            return View("Index", model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ManufacturerViewModel manufacturerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                _manufacturerService.AddManufacturer(new Manufacturer { Name = manufacturerViewModel.Name });
                return Index();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(manufacturerViewModel);
            }
        }
        public IActionResult Edit(int id)
        {
            var manufacturer = _manufacturerService.GetManufacturerById(id);
            var model = new ManufacturerViewModel { Name = manufacturer.Name };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ManufacturerViewModel manufacturerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                _manufacturerService.UpdateManufacturer(new Manufacturer { Id = id, Name = manufacturerViewModel.Name });
                return Index();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(manufacturerViewModel);
            }
        }
    }
}
