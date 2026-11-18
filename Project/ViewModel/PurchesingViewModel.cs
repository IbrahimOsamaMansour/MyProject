using Project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.ViewModel
{
    public class PurchesingViewModel
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        [Required]
        public string ModelName { get; set; }
        public int? Number { get; set; }
        public double? TotalPrice { get; set; }
        public double? UnitPrice { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }  
        public string? BillNum { get; set; }
        [Required]
        public int Categ_id { get; set; }
        [Required]
        public int Comp_id { get; set; }
        public int? Emp_id { get; set; }
        public int? subCateg_id { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Category>? subCategories { get; set; }
        public List<Company>? companies { get; set; }


    }
}
