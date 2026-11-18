using Project.Models;

namespace Project.Repository
{
    public interface IExpensesRepo
    {
        int AddExpeses(Expenses expenses);
        int DeleteExpenses(int id);
        int EditExpenses(int id, Expenses expenses);
        List<Expenses> GetAllExpenses();
        Expenses GetOneExpenseById(int id);
        List<Expenses> GetByitemExpId(int id);
    }
}