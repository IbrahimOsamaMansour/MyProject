using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Repository;
using System.Numerics;

namespace Project.Controllers
{
    [Authorize]
    public class Item_ExpensesController : Controller
    {
        IitemexpensessRepo itemExpRepo;
        private readonly IExpensesRepo expRepo;

        public Item_ExpensesController(IitemexpensessRepo itemexpRepo , IExpensesRepo ExpRepo)
        {
            this.itemExpRepo = itemexpRepo;
            expRepo = ExpRepo;
        }

        // Item_Expenses
        public IActionResult Index()
        {
            List<Item_Expenses> item_Exps = itemExpRepo.GetAll();
            return View(item_Exps);
        }



        //// Item_Expenses Details
        //public IActionResult Details(int id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    Item_Expenses itemExpDetails = new Item_Expenses();
        //    itemExpDetails = itemExpRepo.GetById(id);
        //    return View(itemExpDetails);
        //}



        // Item_Expenses Create
        [HttpGet]
        public IActionResult Create()
        {
            //open view
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item_Expenses Newitem_Exp)
        {
            // save item Expensses
            if (ModelState.IsValid)
            {
                itemExpRepo.Insert(Newitem_Exp);
                return RedirectToAction("Index");
            }
            return View(Newitem_Exp);
        }



        // Item_Expenses Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            //open view 
            Item_Expenses itemExpedit = itemExpRepo.GetById(id);
            return View(itemExpedit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Item_Expenses edititem_Expenses)
        {
            // save item Expensses edit
            if (ModelState.IsValid)
            {
                Item_Expenses edititemexp = itemExpRepo.GetById(id);

                if (edititem_Expenses == null)
                {
                    return RedirectToAction("Edit", edititem_Expenses);
                }
                else
                {
                    itemExpRepo.Update(id, edititem_Expenses);
                    return RedirectToAction("Index");
                }

            }
            return View("Edit", edititem_Expenses);
        }



        // Item_Expenses Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            //open view 
            return View(itemExpRepo.GetById(id));
        }

        // POST: Item_Expenses/Delete
        [HttpPost]
        public IActionResult SaveDelete(int id)
        {
            int result = expRepo.GetByitemExpId(id).Count;
            if (result != 0)
            {
                TempData["Delete"]=" Please Delete Expenses of this item first !";
                return RedirectToAction("Delete",new {id=id});

            }
            itemExpRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
