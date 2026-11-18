using Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.ViewModel
{
	public class ProductBillViewModel
	{
        public int ID { get; set; }
        public String unit_Quantity { get; set; }
        [Required]
        public int Quantity { get; set; }
        public double? unit_Price { get; set; }
        
        public DateTime Book_Date { get; set; }

        public DateTime? Sell_Date { get; set; }
   
        public bool? Resv_Q_State { get; set; }

        public int product_ID { get; set; }
        public virtual List<Product>? products { get; set; }
        public virtual List<Product_Bill>? ProductBills { get; set; }

        public int Bill_ID { get; set; }
        public virtual Bill? bill { get; set; }

        public List<string> units { get; set; }

        public ProductBillViewModel()
        {
            units = new List<string>()
            {
                "pasteboard",
                "meter",
                "unit"
            };
            products = new List<Product>();
            ProductBills = new List<Product_Bill>();
        }
    }
}
