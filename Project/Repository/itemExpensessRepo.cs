using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repository
{
    public class itemExpensessRepo : IitemexpensessRepo
    {
        ElQasrDbContext context;

        public itemExpensessRepo(ElQasrDbContext context)
        {
            this.context = context;
        }




        #region delete item-Exp
        public int Delete(int id)
        {
            Item_Expenses delx = GetById(id);
            if(delx== null)
            {
                return -1;
            }
            try
            {
                Item_Expenses del = GetById(id);
                context.item_Expenses.Remove(del);
                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
          
        }
        #endregion





        #region get all item-Exp
        public List<Item_Expenses> GetAll()
        {
            List<Item_Expenses> itemExpList = context.item_Expenses.ToList();
            return itemExpList;
        }
        #endregion

        #region get by id item-Exp 
        public Item_Expenses GetById(int id)
        {
            Item_Expenses itemExptId = context.item_Expenses.FirstOrDefault(i => i.ID == id);
            return itemExptId;
        }
        #endregion

        #region insert item-Exp
        public int Insert(Item_Expenses NewitemeExp)
        {
            try
            {
                context.item_Expenses.Add(NewitemeExp);
                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }
        #endregion

        #region update item-Exp
        public int Update(int id, Item_Expenses NewitemeExp)
        {
            Item_Expenses delx = GetById(id);
            if (delx == null)
            {
                return -1;
            }
            try
            {
                Item_Expenses OlditemExp = GetById(id);
                OlditemExp.Name = NewitemeExp.Name;
                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }
        #endregion
    }
}
