using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Purchasing
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]

        public string? Name { get; set; }
        public string ModelName { get; set; }
        public int? Number { get; set; }
        public double? TotalPrice { get; set; }

        public double? UnitPrice { get; set; }

        [StringLength(200)]

        public string? Description { get; set; }
        public DateTime DateSell { get; set; }
        public string? BillNum { get; set; }


        [ForeignKey("category")]
        public int? Categ_id { get; set; }
        public virtual Category category { get; set; }
        [ForeignKey("subcategory")]
        public int? subCateg_id { get; set; }
        public virtual Category subcategory { get; set; }

        [ForeignKey("Company")]
        public int? Comp_id { get; set; }
        public virtual Company Company { get; set; }



        [ForeignKey("employee")]

        public int? Emp_id { get; set; }

        public virtual Employee employee { get; set; }

       public virtual List<Product_Purchasing> product_Purchasings { get; set; } = new List<Product_Purchasing>();
       public virtual List<Purchasing_Quantity> Purchasing_Quantities { get; set; } = new List<Purchasing_Quantity>();

        //public static implicit operator Purchasing(Purchasing v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
