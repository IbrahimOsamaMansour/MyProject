using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repository
{
    public class PurchasingRepo : IPurchasingRepo
    {
        private ElQasrDbContext db;
        public PurchasingRepo(ElQasrDbContext _dbcontext)
        {
            db = _dbcontext;
        }

        public List<Purchasing> getAll() => db.purchasing.Include(x => x.Company).Include(x=>x.Purchasing_Quantities).Include(x => x.category).Include(x => x.subcategory).ToList();

        public List<Purchasing> getAllByCatID(int id)=> db.purchasing.Where(x=>x.Categ_id == id).ToList();
        public List<Purchasing> getAllBySubCatID(int id)=> db.purchasing.Where(x=>x.subCateg_id == id).ToList();
        //==================== 11 / 24    //// PurchasingRepo
        public List<Purchasing> getAllCompanyID(int id)=> db.purchasing.Where(x => x.Comp_id == id).ToList();
        public Purchasing getById(int id)=> db.purchasing.Include(x=>x.Company).Include(x => x.category).Include(x => x.subcategory).FirstOrDefault(x => x.ID == id); 
        public int insert(Purchasing newperchasing)
        {
            try
            {
                db.purchasing.Add(newperchasing);
                db.SaveChanges();
                return newperchasing.ID;
            }catch { return 0; }
        }

        public int update( Purchasing editperchasing)
        {
            Purchasing oldpurchasing = getById(editperchasing.ID);
            if (oldpurchasing == null) { return -1; }
            else
            {
                try
                {
                    oldpurchasing.Name = editperchasing.Name;
                    oldpurchasing.Number = editperchasing.Number;
                    oldpurchasing.ModelName = editperchasing.ModelName;
                    oldpurchasing.TotalPrice = editperchasing.TotalPrice;
                    oldpurchasing.UnitPrice = editperchasing.UnitPrice;
                    oldpurchasing.Description = editperchasing.Description;
                    oldpurchasing.DateSell = editperchasing.DateSell;
                    oldpurchasing.BillNum = editperchasing.BillNum;
                    oldpurchasing.subCateg_id = editperchasing.subCateg_id;
                    oldpurchasing.Categ_id = editperchasing.Categ_id;
                    oldpurchasing.Comp_id = editperchasing.Comp_id;
                    //oldpurchasing.Emp_id = editperchasing.Emp_id;
                    db.SaveChanges();
                    return 1;
                }catch { return 0; }
            }
        }
        public int delete(int id)
        {
            Purchasing purchasing = getById(id);
            if (purchasing == null) { return -1; }
            else
            {
                try
                {
                    db.purchasing.Remove(purchasing);
                    db.SaveChanges();
                    return 1;
                } catch{ return 0;}
            }
        }

        public Purchasing getByIdwithAlQuantity(int id)
        {
            Purchasing purchasing = getById(id);
            purchasing.Purchasing_Quantities = db.Purchasing_Quantity.Where(x => x.Purch_id == id).ToList();
            return purchasing;
        }
    }
}
