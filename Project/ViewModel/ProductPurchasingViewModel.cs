using Project.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.ViewModel
{
	public class ProductPurchasingViewModel
	{
        public int Purchasing_Id { get; set; }
        public virtual Purchasing purchasing { get; set; }      
        public int? product_Id { get; set; }
        public virtual Product product { get; set; }
        public virtual Product_Purchasing product_purchasing { get; set; }
    }
}
