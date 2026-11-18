//using Project.Migrations;
using Project.Models;
using System.Linq;

namespace Project.Repository
{
    public class Product_Quantity_Repo : IProduct_Quantity_Repo
    {
        private readonly ElQasrDbContext db;
        public Product_Quantity_Repo(ElQasrDbContext _dbcontext)
        {
            db = _dbcontext;
        }
        public int delete(int id)
        {
            Product_Quantity oldpuq = getById(id);
            if (oldpuq == null) { return -1; }
            try
            {
                db.product_Quantity.Remove(oldpuq);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;

            }
        }

         public List<Product_Quantity> getAllBuProductID(int PID)=> db.product_Quantity.Where(x => x.Prod_Id == PID).ToList();
        public Product_Quantity getById(int id) => db.product_Quantity.FirstOrDefault(x => x.Id == id);

        public int insert(Product_Quantity newperchasing)
        {
            try
            {
                db.product_Quantity.Add(newperchasing);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;

            }
        }

        public int update(Product_Quantity editperchasing)
        {
            Product_Quantity oldpuq = getById(editperchasing.Id);
            if (oldpuq == null) { return -1; }
            try
            {

                oldpuq.ValueQuantity = editperchasing.ValueQuantity;
                oldpuq.UnitQuantity = editperchasing.UnitQuantity;
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;

            }
        }

        public int updateQuantitytoSellProductID(int idproduct, int quantity, string unitquantity)
        {
            Product_Quantity pq = db.product_Quantity.FirstOrDefault(x => x.Prod_Id == idproduct && x.UnitQuantity == unitquantity);
            if(pq == null) { return -1; }
            try
            {
                pq.ValueQuantity += quantity;
                db.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }
    }
}
