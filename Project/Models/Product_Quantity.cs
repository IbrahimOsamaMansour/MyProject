using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Product_Quantity
    {
        public int Id { get; set; }
        public int ValueQuantity { get; set; }
        public string? UnitQuantity { get; set; }
        public double? UnitCost { get; set; }
        [ForeignKey("product")]
        public int? Prod_Id { get; set; }
        public virtual Product? product { get; set; }

    }
}
