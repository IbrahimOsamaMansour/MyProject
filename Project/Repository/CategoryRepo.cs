using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        ElQasrDbContext context;
        public CategoryRepo(ElQasrDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            Category cat = GetById(id);
            context.Category.Remove(cat);
            context.SaveChanges();
        }

        public void DeleteWithSubCat(int id)
        {
            Category Categories = GetByIdWithSubCate(id);
            if(Categories.SubCategories.Count != 0 )
            {
                foreach(Category item in Categories.SubCategories)
                {
                    context.Category.Remove(item);
               
                }
            }
            context.Category.Remove(Categories);
            context.SaveChanges();
        }

        public List<Category> GetAll()
        {
            List<Category> categories = context.Category.Where(x=>x.Suc_Categ_Id == null).ToList();
            return categories;
        }

        public Category GetById(int id)
        {

            Category catId = context.Category.FirstOrDefault(c => c.Id == id);
            return catId;
        }
        public Category GetByIdWithSubCate(int id)
        {
            
            Category catId = context.Category.FirstOrDefault(c => c.Id == id);
            catId.SubCategories=context.Category.Where(c => c.Suc_Categ_Id == id).ToList();
            return catId;
        }

        public void Insert(Category Newcat)
        {
            context.Category.Add(Newcat);
            context.SaveChanges();
        }

        public void Update(int id, Category NewCategory)
        {
            Category Oldcategory = GetById(id);
            Oldcategory.Name = NewCategory.Name;
      
            context.SaveChanges();


        }

    }
}
