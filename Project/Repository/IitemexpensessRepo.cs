using Project.Models;

namespace Project.Repository
{
    public interface IitemexpensessRepo
    {
        List<Item_Expenses> GetAll();
        Item_Expenses GetById(int id);
        int Insert(Item_Expenses NewitemeExp);
        int Update(int id, Item_Expenses NewitemExp);
        int Delete(int id);
        
    }
}
