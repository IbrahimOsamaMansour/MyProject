using Project.Models;

namespace Project.Repository
{
    public class PayedquantityRepo : IPayedquantityRepo
    {
        ElQasrDbContext context;
        public PayedquantityRepo(ElQasrDbContext context)
        {
            this.context = context;
        }
        //===============================================
        public Payed_quantity GetByQId(int id)
        {
            Payed_quantity patyedQ = context.payed_Quantity.FirstOrDefault(x => x.ID == id);
            return patyedQ;
        }

        //===============================================
        public List<Payed_quantity> GetByBillId(int id)
        {

            List<Payed_quantity> payedquantity = context.payed_Quantity.Where(i => i.Bill_ID == id).ToList();
            return payedquantity;
        }
        //===============================================
        public int Insert(Payed_quantity NewpayedQ)
        {
            try
            {
               context.payed_Quantity.Add(NewpayedQ);
                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }

        //====================================================
        public int Update(Payed_quantity NewpayedQ)
        {
            Payed_quantity oldpayedQ = GetByQId(NewpayedQ.ID);
            if(oldpayedQ == null) { return -1; }
            else
            {
                try
                {
                    oldpayedQ.value = NewpayedQ.value;
                    oldpayedQ.Date_Payed = NewpayedQ.Date_Payed;
                    context.SaveChanges();
                    return 1;
                }
                catch { return 0; }
            }
        }

        //====================================================
        public int Delete(int id)
        {
            Payed_quantity exsistpayedQ =GetByQId(id);
            if(exsistpayedQ == null) { return -1; }
            else
            {
                try
                {
                    context.payed_Quantity.Remove(exsistpayedQ);
                    context.SaveChanges();
                    return 1;
                }
                catch { return 0;}
            }
        }
    }
}
