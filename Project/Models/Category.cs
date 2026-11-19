using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
            purchasings = new List<Purchasing>();
            SubCategories = new List<Category>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "The category Name field is required.")]
        [MaxLength(100)]
        [MinLength(2)]
        [Display(Name="category Name ")]
        [UniqueName]
        public string Name { get; set; }
        [ForeignKey("SubCategory")]
        public int? Suc_Categ_Id { get; set; }
        public virtual Category? SubCategory { get; set; }
        [NotMapped]
        public virtual List<Product>? Products { get; set; }
        [NotMapped]
        public virtual List<Purchasing>? purchasings { get; set; }
        [NotMapped]
        public virtual List<Category>? SubCategories { get; set; }

    }
}
