using Project.Models;

namespace Project.Repository
{
    public interface IBillRepo
    {
        

        List<Bill> GetAll();
        Bill GetById(int id);
        void Insert(Bill bill);
        void Update(int id, Bill bill);
        // change bill state to true
        int UpdateBillState(int billId);
        int UpdateTotalPrice(double priceProduct , int billId);
     
        int Delete(int id);
    }
}
