using Project.Models;

namespace Project.Repository
{
	public interface IProductRepo
	{
        List<Product> getAll();
        List<Product> getAllBuCategoryID(int id);
        List<Product> getAllBySubCategoryID(int id);
        List<Product> getAllCompanyID(int id);
        Product getById(int id);
        int update(Product editperchasing);
        int updateReservedbooking(int quantity, int productId);
        int updateReservedSelling(int quantity, int productId);
        int delete(int id);
        int Insert(Product product);
	}
}
