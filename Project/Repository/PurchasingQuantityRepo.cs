//using Project.Migrations;
using Project.Models;

namespace Project.Repository
{
    
    public class PurchasingQuantityRepo : IPurchasingQuantity
	{
        private ElQasrDbContext db;
        public PurchasingQuantityRepo(ElQasrDbContext _dbcontext)
        {
            db = _dbcontext;
        }
        public int delete(int id)
		{
            Purchasing_Quantity oldpuq = getById(id);
            if (oldpuq == null) { return -1; }
            try
            {
                db.Purchasing_Quantity.Remove(oldpuq);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;

            }
        }
        public Purchasing_Quantity getById(int id)
        {
			Purchasing_Quantity pq = db.Purchasing_Quantity.FirstOrDefault(x => x.ID == id);
			return pq;
        }
        public int insert(Purchasing_Quantity newperchasing)
		{
			try
			{
                db.Purchasing_Quantity.Add(newperchasing);
                db.SaveChanges();
				return 1;
			}
			catch
			{
				return 0;

            }
		
		}

		public int update(Purchasing_Quantity editperchasing)
		{
            Purchasing_Quantity oldpuq = getById(editperchasing.ID);
			if(oldpuq == null) { return -1; }
            try
            {
                
			oldpuq.ValueQuantity = editperchasing.ValueQuantity;
			oldpuq.UnitQuantity = editperchasing.UnitQuantity;
			db.SaveChanges();
				return 1;
			}catch
				{
					return 0;

				}

        }

        public List<Purchasing_Quantity> getAllBuPurchID(int PID) => db.Purchasing_Quantity.Where(x => x.Purch_id == PID).ToList();
	}
}
