using Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required]
        public string ModelName { get; set; }
        public int? Number { get; set; }
        public string? Description { get; set; }
        public int? ReservedQuantity { get; set; }
        [Required]
        public int? Categ_Id { get; set; }
    
        public int? subCateg_id { get; set; }
        [Required]
        public int Comp_Id { get; set; }
       
        public int? Emp_Id { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Category>? subCategories { get; set; }
        public List<Company>? companies { get; set; }
    }
}
