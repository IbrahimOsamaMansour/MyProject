
using Project.Models;
using Project.Repository;

namespace Project.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        ISubCategoryRepo subCategoryRepo;
        public SubCategoryService(ISubCategoryRepo subCategoryRepo)
        {
            this.subCategoryRepo = subCategoryRepo;
        }
        public List<Category> GetAllSubCategories()
        {
            List<Category> SubCategories = subCategoryRepo.GetAllSubCategories();
            return SubCategories;
        }
        public Category GetSubCategoryById(int id)
        {
            Category? category = subCategoryRepo.GetSubCategoryById(id);
            return category;
        }




        public int DeleteSubCategory(int id)
        {
            return subCategoryRepo.DeleteSubCategory(id);
        }


        public int EditSubCategory(int id, Category category)
        {
            return subCategoryRepo.EditSubCategory(id, category);
        }
        public int AddSubCategory(Category category)
        {
            return subCategoryRepo.AddSubCategory(category);
        }
        public List<Category> FilterSubCategoryPerCategory(int id)
        {
            List<Category> categories = subCategoryRepo.FilterSubCategoryPerCategory(id);
            return categories;
        }
        public List<Category> GetAllCategories()
        {
            return subCategoryRepo.GetAllCategories();
        }
    }
}
