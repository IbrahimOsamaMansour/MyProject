using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Project.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {

        IBillRepo billRepo;
        IProductBillRepo ProductBillRepo;
        IPayedquantityRepo payedquantityRepo;
       

        public BillsController(IBillRepo billRepo , IProductBillRepo ProductBillRepo , IPayedquantityRepo payedquantityRepo)
        {
            this.billRepo = billRepo;   
            this.ProductBillRepo= ProductBillRepo; 
            this.payedquantityRepo = payedquantityRepo;
           

        }

        // GET
        public IActionResult Index()
        {
              return View(billRepo.GetAll());
        }

        // GET
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Bill bi = billRepo.GetById(id);
                bi.Product_Bills = ProductBillRepo.GetAll(id);
                bi.PayedQuantities = payedquantityRepo.GetByBillId(id);
                return View(bi);
            }
               
        }

        // GET
        public IActionResult Create()
        {
            var nwDt = DateTime.Now.ToShortDateString();
          
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bill bill)
        {
            if (ModelState.IsValid)
            {
                Bill bill1 = new Bill()
                {
                    Bill_Num = bill.Bill_Num,
                    Client_Name = bill.Client_Name,
                    Bill_State = false,
                   sell_State = false,

                      Date = bill.Date,
                     // Date = DateTime.Now.Date,
                      Exp_Date = bill.Date.AddMonths(3),
                    Client_phone = bill.Client_phone,
                    Total_Price = 0,
                    Note = bill.Note,
                    Employee_ID= null
                 };    
               billRepo.Insert(bill1);   
               return RedirectToAction("Index");
        }
            return View(bill);
    }









        // GET
        public IActionResult Edit(int id )
        {
            return View(billRepo.GetById(id));
        }

       
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Bill_Num,Client_Name,Bill_State,sell_State,Date,Exp_Date,Client_phone,Total_Price,Note")] Bill bill)
        {
          

            //if (ModelState.IsValid)
            //{
                try
                {
                    billRepo.Update(id, bill);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                    return RedirectToAction("Index");
                }

            //}
            //return View(bill);
        }

        // GET
        public IActionResult Delete(int id)
        {
            Bill bill = billRepo.GetById(id);

            

            return View(bill);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            int result1 = ProductBillRepo.GetByBillId(id).Count();
            if(result1 != 0) 
            { 
                TempData["deleteProduct"] = "Please Delete All Product  in this Bill";
                return RedirectToAction("Delete", new { id = id }); 
            }
            int result2 = payedquantityRepo.GetByBillId(id).Count();
            if (result2 != 0)
            {
                TempData["deletequantity"] = "Please Delete All Payed quantity  in this Bill";
                return RedirectToAction("Delete", new { id = id });
            }
            int result = billRepo.Delete(id);
            if (result == 1) 
            { TempData["sucDelete"] = "Delete Bill is Done Succesfully";
                return RedirectToAction("Index"); 
            }
            else { ViewBag.delfail = "fail"; }
            return RedirectToAction("Delete", new { id = id });


            //int result = billRepo.Delete(id);
            //if (result != 1) { 
            //    TempData["delFail"] = "Sorry Delete is Not Done"; 
            //    return RedirectToAction("Delete", new { ID = id });
            //}
            ////========================== 
            //TempData["delsuc"] = "Delete Company is Done";
            //return RedirectToAction("Index");
        }

       
    }
}
