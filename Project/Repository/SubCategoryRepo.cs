using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Linq;

namespace Project.Repository
{
	public class SubCategoryRepo: ISubCategoryRepo
    {
        private ElQasrDbContext db;
        public SubCategoryRepo(ElQasrDbContext _dbcontext)
        {
            db = _dbcontext;
        }
        public List<Category> getAllByID(int id) => db.Category.Where(x=>x.Suc_Categ_Id == id).ToList();


        public List<Category> GetAllSubCategories()
        {
            List<Category> SubCategories = db.Category.Where(c => c.Suc_Categ_Id != null).ToList();
            return SubCategories;
        }
        public Category GetSubCategoryById(int id)
        {
            Category? category = db.Category.Include(c => c.SubCategory).Where(c => c.Suc_Categ_Id != 0).SingleOrDefault(c => c.Id == id);
            return category;
        }
        public int DeleteSubCategory(int id)
        {
            Category category = GetSubCategoryById(id);
            if (category == null)
            {
                return -1;
            }
            db.Category.Remove(category);
            return db.SaveChanges();
        }
        public int EditSubCategory(int id, Category category)
        {
            Category old = GetSubCategoryById(id);
            if (old != null)
            {
                old.Name = category.Name;
                old.Suc_Categ_Id = category.Suc_Categ_Id;
                return db.SaveChanges();
            }
            return -1;
        }
        public int AddSubCategory(Category category)
        {
            if (category == null)
            {
                return -1;
            }
            db.Category.Add(category);
            return db.SaveChanges();
        }
        public List<Category> FilterSubCategoryPerCategory(int id)
        {
            List<Category> categories = db.Category.Include(x=>x.SubCategory).Where(c => c.Suc_Categ_Id == id).ToList();
            return categories;
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            categories = db.Category.Where(c => c.Suc_Categ_Id == null).ToList();
            return categories;
        }
    }
}
