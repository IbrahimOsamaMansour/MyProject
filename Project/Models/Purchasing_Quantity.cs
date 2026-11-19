using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Purchasing_Quantity
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("purchasing")]
        public int Purch_id { get; set; }
        public virtual Purchasing purchasing { get; set; }
        public int ValueQuantity  { get; set; }
        public string? UnitQuantity { get; set; }
        public double? UnitPrice { get; set; }


    }
}
