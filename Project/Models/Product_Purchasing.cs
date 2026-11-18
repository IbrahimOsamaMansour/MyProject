using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Product_Purchasing
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Purchasing")]
        public int Purchasing_Id { get; set; }
        public virtual Purchasing Purchasing { get; set; }

        [ForeignKey("product")]
        public int product_Id { get; set; }
        public virtual Product product { get; set; }

    }
}
