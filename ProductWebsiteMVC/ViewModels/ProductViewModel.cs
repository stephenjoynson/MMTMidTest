using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductWebsiteMVC.Extensions;

namespace ProductWebsiteMVC.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Manufacturers = new List<SelectListItem>();
        }

        [Required][StringLength(50, ErrorMessage = "The name cannot be more than 50 characters long")]
        [CustomValidation(typeof(ProductValidator), "ValidateProductNameDoesNotContainManufacturerName")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "The description cannot be more than 500 characters long")]
        public string Description { get; set; }
        public int ManufacturerId { get; set; }
        public List<SelectListItem> Manufacturers { get; set; }
    }
}
