using Project.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.ViewModel
{
    public class payedQuantityviewModel
    {
        public int? ID { get; set; }
        public int value { get; set; }
        public DateTime Date_Payed { get; set; }
        public int Bill_ID { get; set; }
        public virtual Bill? bill { get; set; }
        public List<Payed_quantity>? payed_Quantities { get; set; }  
    }
}
