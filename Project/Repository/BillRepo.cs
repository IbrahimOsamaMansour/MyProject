using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repository
{
    public class BillRepo : IBillRepo
    {
        ElQasrDbContext context;
        public BillRepo(ElQasrDbContext context)
        {
            this.context = context;
        }


        public int Delete(int id)
        {
            Bill bill = GetById(id);
            if (bill == null) { return -1; }
            try
            {
                context.Bill.Remove(bill);
                context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;

            }
        }

        public List<Bill> GetAll()
        {
            List<Bill> bill = context.Bill.ToList();
            return bill;
        }

        public Bill GetById(int id)
        {
            Bill billId = context.Bill.FirstOrDefault(x => x.ID == id);
            return billId;
        }

        public void Insert(Bill NewBill)
        {
            context.Bill.Add(NewBill);
            context.SaveChanges();
        }

        public void Update(int id, Bill newbill)
        {
            Bill OldBill = GetById(id);
            OldBill.Bill_Num = newbill.Bill_Num;
            OldBill.Client_Name= newbill.Client_Name;   
            OldBill.Bill_State= newbill.Bill_State;
            OldBill.sell_State = newbill.sell_State;
            OldBill.Date=newbill.Date;  
            OldBill.Exp_Date=newbill.Exp_Date;
            OldBill.Client_phone=newbill.Client_phone;  
            OldBill.Total_Price=newbill.Total_Price;    
            OldBill.Note=newbill.Note;  
           
            context.SaveChanges(); 
        }

        public int UpdateBillState(int billId)
        {
            Bill bill = GetById(billId);
            if (bill == null) { return -1; }
            try
            {
                bill.Bill_State = true;
                context.SaveChanges();
                return 1;
            }catch{ return 0; }
        }

        public int UpdateTotalPrice(double priceProduct, int billId)
        {
            Bill bill =GetById(billId);
            if(bill == null) { return -1; }
            try
            {
                bill.Total_Price += priceProduct;
                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }
    }
}
