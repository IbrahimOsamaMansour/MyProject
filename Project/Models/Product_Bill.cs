using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Product_Bill
    {
        [Key]
        public int ID { get; set; }
        public String? unit_Quantity { get; set; }
        public int? Quantity { get; set; }
        public double? unit_Price { get; set; }
        [Required]
        public DateTime? Book_Date { get; set; }
       
        public DateTime? Sell_Date { get; set; }
        public bool Resv_Q_State { get; set; }

        [ForeignKey("product")]
        public int product_ID { get; set; }
        public virtual Product product { get; set; }

        [ForeignKey("bill")]
        public int Bill_ID { get; set; }
        public virtual Bill bill { get; set; }

    }
}
