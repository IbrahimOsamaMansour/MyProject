using Project.Models;

namespace Project.Repository
{
	public interface IProductPurchasing
	{
        List<Product_Purchasing> getByPurchId(int id);
        Product_Purchasing getById(int id);
        int insert(Product_Purchasing newperchasing);
        int update(Product_Purchasing editperchasing);
        int delete(int id);
    }
}
