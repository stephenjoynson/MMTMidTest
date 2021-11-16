using Microsoft.AspNetCore.Mvc;
using ProductBusiness.Interfaces;
using ProductData.Models;
using ProductWebsiteMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManufacturerWebsiteMVC
{
    public class ManufacturerController : Controller
    {
        private IManufacturerService _ManufacturerService;
        public ManufacturerController(IManufacturerService ManufacturerService)
        {
            _ManufacturerService = ManufacturerService;
        }
        public IActionResult Index()
        {
            var model = _ManufacturerService.GetAllManufacturers();
            return View("Index", model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ManufacturerViewModel ManufacturerViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ManufacturerService.AddManufacturer(new Manufacturer { Name = ManufacturerViewModel.Name });
                    return Index();
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(ManufacturerViewModel);
            }
        }
        public IActionResult Edit(int Id)
        {
            var Manufacturer = _ManufacturerService.GetManufacturerById(Id);
            var model = new ManufacturerViewModel { Name = Manufacturer.Name };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, ManufacturerViewModel ManufacturerViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ManufacturerService.UpdateManufacturer(new Manufacturer { Id = Id, Name = ManufacturerViewModel.Name });
                    return Index();
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(ManufacturerViewModel);
            }
        }
    }
}
