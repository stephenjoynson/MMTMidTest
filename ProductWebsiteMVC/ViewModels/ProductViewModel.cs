using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductData.Models;
using ProductWebsiteMVC.Extensions;

namespace ProductWebsiteMVC.ViewModels
{
    public class ProductViewModel
    {
        private static IEnumerable<Manufacturer> _manufacturers;
        
        [Required][StringLength(50, ErrorMessage = "The name cannot be more than 50 characters long")]
        [CustomValidation(typeof(ProductValidator), "ValidateProductNameDoesNotEqualManufacturerName")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "The description cannot be more than 500 characters long")]
        public string Description { get; set; }
        public int ManufacturerId { get; set; }
        public static List<SelectListItem> Manufacturers { 
            get { return _manufacturers.Select(manufacturer => new SelectListItem(manufacturer.Name, manufacturer.Id.ToString())).ToList(); }
        }

        public void SetManufacturers(IEnumerable<Manufacturer> manufacturers)
        {
            _manufacturers = manufacturers;
        }
    }
}
