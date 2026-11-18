using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Bill
    {
        [Key]
        public int ID  { get; set; }
        [Required]
        [UniqueBillNum]
        public string  Bill_Num { get; set; }
        [Required]
        //[UniqueBillNClientName]
        public string Client_Name { get; set; }
        public bool Bill_State { get; set; }
        public bool sell_State { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public DateTime? Exp_Date { get; set; }
        [RegularExpression(@"^(\+201|01|00201)[0-2,5]{1}[0-9]{8}", ErrorMessage = "invalid Phone")]
        public string? Client_phone { get; set; }
        public double? Total_Price { get; set; }
        public string? Note { get; set; }

        [ForeignKey("employee")]
        public int? Employee_ID { get; set; }
        public virtual Employee? employee { get; set; }
        public virtual List<Product_Bill> Product_Bills { get; set; }
        public virtual List<Payed_quantity> PayedQuantities { get; set; }
       
        public Bill()
        {
            Product_Bills = new List<Product_Bill>();
            PayedQuantities = new List<Payed_quantity>();
        }



    }
}
