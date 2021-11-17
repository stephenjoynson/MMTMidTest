using System;
using System.ComponentModel.DataAnnotations;
using ProductBusiness.Interfaces;
using ProductWebsiteMVC.ViewModels;

namespace ProductWebsiteMVC.Validation
{
    public class ProductValidator : IProductValidator
    {
        private static IProductService _productService;

        public ProductValidator(IProductService productService)
        {
            _productService = productService;
        }

        public static ValidationResult ValidateProductNameDoesNotContainManufacturerName(string name, ValidationContext context)
        {
            var model = context.ObjectInstance as ProductViewModel;
            if (model == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return _productService.ValidateProductNameDoesNotContainToManufacturerName(name, model.ManufacturerId) ? ValidationResult.Success : new ValidationResult("Product Name must not contain the Manufacturer Name");
        }
    }
}
