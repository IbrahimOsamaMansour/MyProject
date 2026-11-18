using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repository
{
    public class ExpensesRepo : IExpensesRepo
    {
        private readonly ElQasrDbContext db;

        public ExpensesRepo(ElQasrDbContext db)
        {
            this.db = db;
        }

        public List<Expenses> GetByitemExpId(int id)
        {
            List<Expenses> ExptId = db.Expense.Where(e => e.Item_Ex_ID == id).ToList();
            return ExptId;
        }
        public List<Expenses> GetAllExpenses()
        {
            return db.Expense.Include(x=>x.Itemexpense).ToList();
        }
        public Expenses GetOneExpenseById(int id)
        {
            return db.Expense.Include(e => e.Itemexpense).Include(e => e.employee).SingleOrDefault(e => e.ID == id);
        }
        public int AddExpeses(Expenses expenses)
        {
            try
            {
                db.Expense.Add(expenses);
                return db.SaveChanges();
            }
            catch
            {
                return 0;
            }

        }
        public int EditExpenses(int id, Expenses expenses)
        {
            Expenses old = GetOneExpenseById(id);
            if (old == null && expenses == null)
            {
                return -1;
            }
            try
            {
                old.Value = expenses.Value;
                old.Expense_Date = expenses.Expense_Date;
                old.note = expenses.note;
                old.Item_Ex_ID = expenses.Item_Ex_ID;
                return db.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }
        public int DeleteExpenses(int id)
        {
            Expenses? expenses = db.Expense.SingleOrDefault(e => e.ID == id);
            if (expenses == null)
            {
                return -1;
            }
            try
            {
                db.Expense.Remove(expenses);
                return db.SaveChanges();
            }
            catch { return 0; }
        }

    }
}
