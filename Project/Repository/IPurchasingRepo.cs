using Project.Models;

namespace Project.Repository
{
	public interface IPurchasingRepo
	{
        List<Purchasing> getAll();
        List<Purchasing> getAllByCatID(int id);
        List<Purchasing> getAllBySubCatID(int id);
        List<Purchasing> getAllCompanyID(int id);
        Purchasing getById(int id);
        Purchasing getByIdwithAlQuantity(int id);
        int insert(Purchasing newperchasing);
        int update(Purchasing editperchasing);
        int delete(int id);
    }
}
