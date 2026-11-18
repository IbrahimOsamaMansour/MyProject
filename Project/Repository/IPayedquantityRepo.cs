using Project.Models;

namespace Project.Repository
{
    public interface IPayedquantityRepo
    {
        List<Payed_quantity> GetByBillId(int id);
        int Insert(Payed_quantity NewpayedQ);
        int Update(Payed_quantity NewpayedQ);
        int Delete(int id);
        Payed_quantity GetByQId(int id);


    }
}
