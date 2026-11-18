using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Payed_quantity
    {
        [Key]
        public int ID { get; set; }
        public int value { get; set; }
        [Required]
        public DateTime Date_Payed { get; set; }

        [ForeignKey("bill")]
        public int Bill_ID { get; set; }
        public virtual Bill? bill { get; set; }

    }
}
