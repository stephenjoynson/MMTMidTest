using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductWebsiteMVC.ViewModels
{
    public class ManufacturerViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "The name cannot be more than 30 characters long")]
        public string Name { get; set; }
        public static List<SelectListItem> Manufacturers
        {
            get
            {
                var items = new List<SelectListItem>();
                items.Add(new SelectListItem("Remote Control Machines", "1"));
                items.Add(new SelectListItem("NetworkDevices", "2"));
                return items;
            }
        }
    }
}
