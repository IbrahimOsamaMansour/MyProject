using Project.Models;

namespace Project.Repository
{
    public interface IProductBillRepo
    {
       List<Product_Bill> GetByBillId( int id);
       List<Product_Bill> GetByProductId( int id);
        List<Product_Bill> GetAll(int id);
        Product_Bill GetById(int id);
        int Insert(Product_Bill productBill);
        int Update(Product_Bill productBill);
        //change reserved quantity to true => product is sold
        int UpdateReservedquantityState(int id);
        int Delete(int id);
    }
}
