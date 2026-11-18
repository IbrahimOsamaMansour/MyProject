using Project.Models;

namespace Project.Repository
{
    public interface IProduct_Quantity_Repo
    {
        List<Product_Quantity> getAllBuProductID(int PID);
        Product_Quantity getById(int id);
        int insert(Product_Quantity newperchasing);
        int update(Product_Quantity editperchasing);
        int updateQuantitytoSellProductID(int idproduct , int quantity, string unitquantity);
        int delete(int id);
    }
}
