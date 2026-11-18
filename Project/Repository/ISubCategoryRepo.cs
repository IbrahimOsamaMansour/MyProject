using Project.Models;

namespace Project.Repository
{
	public interface ISubCategoryRepo
	{
        List<Category> getAllByID(int id);
        int AddSubCategory(Category category);
        int DeleteSubCategory(int id);
        int EditSubCategory(int id, Category category);
        List<Category> FilterSubCategoryPerCategory(int id);
        List<Category> GetAllSubCategories();
        Category GetSubCategoryById(int id);
        public List<Category> GetAllCategories();
    }
}
