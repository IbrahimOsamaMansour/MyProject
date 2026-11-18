//using Project.Migrations;
using Project.Models;

namespace Project.Repository
{
	public class ProductPurchasingRepo : IProductPurchasing
	{
        private ElQasrDbContext db;
        public ProductPurchasingRepo(ElQasrDbContext _dbcontext)
        {
            db = _dbcontext;
        }
        public int delete(int id)
		{
            Product_Purchasing oldpuq = getById(id);
            if (oldpuq == null) { return -1; }
            try
            {
                db.product_Purchasing.Remove(oldpuq);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;

            }
        }

		public Product_Purchasing getById(int id)
		{
			return db.product_Purchasing.FirstOrDefault(x => x.Purchasing_Id == id);
		}

        public List<Product_Purchasing> getByPurchId(int id) => db.product_Purchasing.Where(x => x.Purchasing_Id == id).ToList();

        public int insert(Product_Purchasing newperchasing)
		{
            try
            {
                db.product_Purchasing.Add(newperchasing);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

		public int update(Product_Purchasing editperchasing)
		{
			throw new NotImplementedException();
		}
	}
}
