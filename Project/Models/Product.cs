
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Project.Models
{
    public class Product
    {
       // private List<Product_Bill> pr;

        public Product()
        {
            Product_Purchasings = new List<Product_Purchasing>();
            Product_Quantities = new List<Product_Quantity>();
            ProductBill = new List<Product_Bill>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string ModelName { get; set; }
        public int? Number { get; set; }
        public string? Description { get; set; }
        public int? ReservedQuantity { get; set; }
        [ForeignKey("category")]
        public int? Categ_Id { get; set; }
        public virtual Category? category { get; set; }
        [ForeignKey("subcategory")]
        public int? subCateg_id { get; set; }
        public virtual Category? subcategory { get; set; }
        [ForeignKey("company")]
        public int? Comp_Id { get; set; }
        public virtual Company? company { get; set; }
        [ForeignKey("empolyee")]
        public int? Emp_Id { get; set; }
        public virtual Employee? empolyee { get; set; }
        public virtual List<Product_Purchasing>? Product_Purchasings { get; set; }
        public virtual List<Product_Quantity>? Product_Quantities { get; set; }
       public virtual List<Product_Bill>? ProductBill { get; set; }
    }
}
