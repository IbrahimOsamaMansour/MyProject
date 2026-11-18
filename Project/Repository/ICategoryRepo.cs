using Project.Models;

namespace Project.Repository
{
    public interface ICategoryRepo
    {
        List<Category> GetAll();
        Category GetById(int id);
        void Insert(Category NewCompany);
        void Update(int id, Category NewCompany);
        void Delete(int id);
        Category GetByIdWithSubCate(int id);
        void DeleteWithSubCat (int id);

    }
}
