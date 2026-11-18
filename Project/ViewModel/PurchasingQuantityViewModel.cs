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
