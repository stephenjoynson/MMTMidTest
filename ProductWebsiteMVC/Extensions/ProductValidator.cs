using System;
using System.ComponentModel.DataAnnotations;
using ProductBusiness.Interfaces;
using ProductWebsiteMVC.ViewModels;

namespace ProductWebsiteMVC.Extensions
{
    public static class ProductValidator
    {
        private static IProductService _productService;

        public static void SetProductService(IProductService productService)
        {
            _productService = productService;
        }
        public static ValidationResult ValidateProductNameDoesNotEqualManufacturerName(string name, ValidationContext context)
        {
            var model = context.ObjectInstance as ProductViewModel;
            if (model == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return _productService.ValidateProductNameNotEqualToManufacturerName(name, model.ManufacturerId) ? ValidationResult.Success : new ValidationResult("Product Name must not be the same as the Manufacturer Name");
        }
    }
}
