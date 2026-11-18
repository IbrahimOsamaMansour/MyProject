using Microsoft.EntityFrameworkCore;
//using Project.Migrations;
using Project.Models;
using System.Collections.Generic;

namespace Project.Repository
{
    public class ProductBillRepo : IProductBillRepo
    {
        ElQasrDbContext context;
        public ProductBillRepo(ElQasrDbContext context)
        {
            this.context = context;
        }


        public  List<Product_Bill> GetByBillId(int id)
        {
            List < Product_Bill> product_Bill = context.Product_Bill.Include(x => x.product).Where(x => x.Bill_ID == id).ToList();
            return  product_Bill;
        }


        public int Delete(int id)
        {
            Product_Bill p = GetById(id);
            if (p == null) { return -1; }
            try
            {
                context.Product_Bill.Remove(p);
                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }

        public List<Product_Bill> GetAll(int id) => context.Product_Bill.Include(x => x.bill).Include(x => x.product).Where(x => x.Bill_ID == id).ToList();
        public List<Product_Bill> GetByProductId(int id) => context.Product_Bill.Where(x => x.product_ID == id).ToList();

        public Product_Bill GetById(int id) => context.Product_Bill.Include(x => x.bill).Include(x => x.product).FirstOrDefault(x => x.ID == id);

        public int Insert(Product_Bill productBill)
        {
            try
            {
                context.Product_Bill.Add(productBill);
                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }

        public int Update(Product_Bill productBill)
        {
            Product_Bill p = GetById(productBill.ID);
            if (p != null) { return -1; }
            try
            {
                p.product_ID = productBill.product_ID;
                p.Bill_ID = productBill.Bill_ID;
                p.Quantity = productBill.Quantity;
                p.unit_Quantity = productBill.unit_Quantity;
                p.Resv_Q_State = productBill.Resv_Q_State;
                p.Sell_Date = productBill.Sell_Date;
                p.Book_Date = productBill.Book_Date;
                p.unit_Price = productBill.unit_Price;

                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }
        // product is sold so reserved quantity change to 
        public int UpdateReservedquantityState(int id)
        {
            Product_Bill p = GetById(id);
            if (p == null) { return -1; }
            try
            {
                p.Resv_Q_State = true;
                p.Sell_Date = DateTime.Now.Date;
                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }
    }
}
