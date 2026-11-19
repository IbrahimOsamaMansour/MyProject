using Microsoft.Build.Framework;
using Project.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Project.ViewModel
{
	public class PurchasingQuantityViewModel
	{
        public int ID { get; set; }
       // [Required]
        public int Purch_id { get; set; }
        public int Product_id { get; set; }
        [Required]
        public int ValueQuantity { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than zero.")]
        public double? UnitPrice { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Unit cost cannot be negative.")]
        public double? UnitCost { get; set; }
        [Required]
        public string UnitQuantity { get; set; }
        public List<string>? units { get; set; }
        public Purchasing? purchasing { get; set; }
        public Product? Product { get; set; }
        public PurchasingQuantityViewModel()
        {
            units = new List<string>()
            {
                "pasteboard",
                "meter",
                "unit"
            };
        }
    }
}
