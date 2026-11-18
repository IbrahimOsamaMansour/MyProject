using Project.Models;

namespace Project.Repository
{
	public interface IPurchasingQuantity
	{
        List<Purchasing_Quantity> getAllBuPurchID(int PID);
        Purchasing_Quantity getById(int id);
        int insert(Purchasing_Quantity newperchasing);
        int update(Purchasing_Quantity editperchasing);
        int delete(int id);
    }
}
