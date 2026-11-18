using Project.Models;

namespace Project.Services
{
    public interface ISubCategoryService
    {
        int AddSubCategory(Category category);
        int DeleteSubCategory(int id);
        int EditSubCategory(int id, Category category);
        List<Category> FilterSubCategoryPerCategory(int id);
        List<Category> GetAllSubCategories();
        Category GetSubCategoryById(int id);
        public List<Category> GetAllCategories();

    }
}