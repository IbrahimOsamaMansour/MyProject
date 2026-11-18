using Project.Models;

namespace Project.ViewModel
{
	public class DeleteProductBillViewModel
	{
		public Bill? bill { get; set; }
		public Product? product { get; set; }
		public List<Product_Quantity> product_Quantities { get; set; }

		public Product_Bill product_Bill { get; set; }
        public int ID { get; set; }       
        public int? product_ID { get; set; }
        public int? Bill_ID { get; set; }

    }
}
