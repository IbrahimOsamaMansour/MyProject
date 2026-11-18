using MessagePack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Models;
using Project.Repository;
using Project.ViewModel;

namespace Project.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        //-------------------------------------controller Fields------------------------------------

        private readonly IExpensesRepo expensesRepo;
        private readonly IitemexpensessRepo iitemExpensessRepo;

        //-------------------------------------CTOR------------------------------------

        public ExpensesController(IExpensesRepo expensesRepo, IitemexpensessRepo iitemExpensessRepo)
        {
            this.expensesRepo = expensesRepo;
            this.iitemExpensessRepo = iitemExpensessRepo;
        }


        //-------------------------------------Show All------------------------------------

        public IActionResult ShowAllExpenses()
        {
            return View(expensesRepo.GetAllExpenses());
        }

        //-------------------------------------Get ONE------------------------------------

        public IActionResult Details(int id)
        {
            return View(expensesRepo.GetOneExpenseById(id));
        }

        //-------------------------------------Create------------------------------------

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ItemExpenses = new SelectList(iitemExpensessRepo.GetAll(), "ID", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Expenses expenses)
        {
            if (expenses.Item_Ex_ID == 0)
            {
                ModelState.AddModelError("", "Item Expense is a must And Dont Forget Date");
            }
            if ((int)expenses.Expense_Date.Year > (int)DateTime.Now.Year)
            {
                ModelState.AddModelError("", "Date Must Be Before Today");
            }
            else if ((int)expenses.Expense_Date.Year == (int)DateTime.Now.Year)
            {
                if ((int)expenses.Expense_Date.Month > (int)DateTime.Now.Month)
                {
                    ModelState.AddModelError("", "Date Must Be Before Today");
                }
                else if ((int)expenses.Expense_Date.Month == (int)DateTime.Now.Month)
                {
                    if ((int)expenses.Expense_Date.Day > (int)DateTime.Now.Day)
                    {
                        ModelState.AddModelError("", "Date Must Be Before Today");
                    }
                }
            }
            if (ModelState.IsValid && expenses != null)
            {
                expensesRepo.AddExpeses(expenses);
                expenses = expensesRepo.GetOneExpenseById(expenses.ID);
                TempData["Added"] = $"{expenses.Value} LE as {expenses.Itemexpense.Name} is Added";
                return RedirectToAction(nameof(ShowAllExpenses));
            }
            ViewBag.ItemExpenses = new SelectList(iitemExpensessRepo.GetAll(), "ID", "Name");
            return View(expenses);
        }

        //-------------------------------------Edit------------------------------------

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Expenses ex = expensesRepo.GetOneExpenseById(id);
            ViewBag.ItemExpenses = new SelectList(iitemExpensessRepo.GetAll(), "ID", "Name");
            return View(ex);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Expenses expenses)
        {
            if (expenses.Item_Ex_ID == 0)
            {
                ModelState.AddModelError("", "Item Expense is a must And Dont Forget Date");
            }
            if ((int)expenses.Expense_Date.Year > (int)DateTime.Now.Year)
            {
                ModelState.AddModelError("", "Date Must Be Before Today");
            }
            else if ((int)expenses.Expense_Date.Year == (int)DateTime.Now.Year)
            {
                if ((int)expenses.Expense_Date.Month > (int)DateTime.Now.Month)
                {
                    ModelState.AddModelError("", "Date Must Be Before Today");
                }
                else if ((int)expenses.Expense_Date.Month == (int)DateTime.Now.Month)
                {
                    if ((int)expenses.Expense_Date.Day > (int)DateTime.Now.Day)
                    {
                        ModelState.AddModelError("", "Date Must Be Before Today");
                    }
                }
            }

            if (expenses != null && ModelState.IsValid)
            {
                expensesRepo.EditExpenses(expenses.ID, expenses);
                TempData["Edit"] = "Updated Successfully";
                return RedirectToAction(nameof(ShowAllExpenses));
            }
            ViewBag.ItemExpenses = new SelectList(iitemExpensessRepo.GetAll(), "ID", "Name");
            return View(expenses);
        }

        //-------------------------------------Delete------------------------------------

        public IActionResult Delete(int id)
        {
            return RedirectToAction(nameof(Details), new { id = id });
        }
        public IActionResult SaveDelete(int id)
        {
            expensesRepo.DeleteExpenses(id);
            TempData["Delete"] = "Deleted Successfully";
            return RedirectToAction(nameof(ShowAllExpenses));
        }
    }
}
