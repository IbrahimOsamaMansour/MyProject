using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Repository;
using Project.ViewModel;
using System.Security.Cryptography.X509Certificates;

namespace Project.Controllers
{
    [Authorize]
    public class payedQuantityController : Controller
    {

        private readonly IPayedquantityRepo payedQRepo;
        private readonly IBillRepo billRepo;

        public payedQuantityController (IPayedquantityRepo payedQRepo, IBillRepo billRepo)
        {
            this.payedQRepo = payedQRepo;
            this.billRepo = billRepo;
        }
        //================================================
        public IActionResult Index()
        {
            return View();
        }
        //================================================
        public IActionResult Create(int id)
        {
            payedQuantityviewModel newpayedQ = new payedQuantityviewModel()
            {
                bill = billRepo.GetById(id),
                Bill_ID= id,
                Date_Payed= DateTime.Now,
                payed_Quantities = payedQRepo.GetByBillId(id)
            };
            return View(newpayedQ);
         }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( payedQuantityviewModel newpayedv)
        {
            if (ModelState.IsValid)
            {

                double? totalp = billRepo.GetById(newpayedv.Bill_ID).Total_Price;
                int totalPayQ = payedQRepo.GetByBillId(newpayedv.Bill_ID).Sum(x=>x.value);
                
                if(totalPayQ <totalp)
                {
                    double? recQ = totalp - totalPayQ;

                    if( newpayedv.value <= recQ )
                    {
                        Payed_quantity payedQ = new Payed_quantity()
                        {
                            Bill_ID = newpayedv.Bill_ID,
                            Date_Payed = newpayedv.Date_Payed,
                            value = newpayedv.value
                        };
                        int result = payedQRepo.Insert(payedQ);

                        int totalPayQnew = payedQRepo.GetByBillId(newpayedv.Bill_ID).Sum(x => x.value);
                        if (totalPayQnew == totalp)
                        {

                            Bill oldbill = billRepo.GetById(newpayedv.Bill_ID);
                            //oldbill.ID = mybill.ID;
                            //billRepo.Update(mybill.ID, mybill);

                            Bill billedit = new Bill()
                            {
                                ID = oldbill.ID,
                                Bill_Num = oldbill.Bill_Num,
                                Client_Name = oldbill.Client_Name,
                                Bill_State = oldbill.Bill_State,
                                sell_State =true,
                                Date = DateTime.Now,
                                Exp_Date = DateTime.Now.AddMonths(3),
                                Client_phone = oldbill.Client_phone,
                                Total_Price = oldbill.Total_Price,
                                Note = oldbill.Note,
                            };
                            billRepo.Update(oldbill.ID, billedit);
                            //bool changeBstat = billRepo.GetById(newpayedv.Bill_ID).sell_State;
                            //changeBstat = true;



                        }
                        if (result == 1)
                        {
                            TempData["createPQ"] = " payed Quantity added sucssesfully";
                            return RedirectToAction("Create", new { id = newpayedv.Bill_ID });
                        }
                        else
                        {
                            TempData["faildPQ"] = "Sorry payed Quantity added failed !";
                            return RedirectToAction("Create", new { id = newpayedv.Bill_ID });

                        }
                    }
                    else
                    {
                        TempData["cantCreatePQ"] = " Sorry! You can't add this payed Quantity because your total price of this bill is " + totalp;
                        return RedirectToAction("Create", new { id = newpayedv.Bill_ID });
                    }
                }
                else
                {
                    TempData["tpanpQareEqual"] = " the total payed Quantity you have entered are equal to total price =  " + totalp;
                    return RedirectToAction("Create", new { id = newpayedv.Bill_ID });
                }

            }
            payedQuantityviewModel newpayedQ = new payedQuantityviewModel()
            {
                bill = billRepo.GetById(newpayedv.Bill_ID),
                Bill_ID = newpayedv.Bill_ID,
                payed_Quantities = payedQRepo.GetByBillId(newpayedv.Bill_ID)
            };
            return View(newpayedQ);
           
        }
        //=================================================

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Payed_quantity editpayedQ = payedQRepo.GetByQId(id);
            return View(editpayedQ);
        }
        [HttpPost]
        public IActionResult Edit( Payed_quantity editpayedQ)
        {
            if (ModelState.IsValid)
            {
                if (editpayedQ == null)
                {
                    return RedirectToAction("Edit", editpayedQ);
                }
                else
                {
                    double? totalp = billRepo.GetById(editpayedQ.Bill_ID).Total_Price;
                    int totalPayQ = payedQRepo.GetByBillId(editpayedQ.Bill_ID).Sum(x => x.value);
                    int oldpayQ = payedQRepo.GetByQId(editpayedQ.ID).value;
                    totalPayQ -= oldpayQ;

                    if (totalPayQ < totalp)
                    {
                        double? recQ = totalp - totalPayQ;

                        if (editpayedQ.value <= recQ)
                        {
                            int result = payedQRepo.Update(editpayedQ);

                            int totalPayQnew = payedQRepo.GetByBillId(editpayedQ.Bill_ID).Sum(x => x.value);
                            if (totalPayQnew < totalp)
                            {
                                Bill oldbill = billRepo.GetById(editpayedQ.Bill_ID);
                                Bill billedit = new Bill()
                                {
                                    ID = oldbill.ID,
                                    Bill_Num = oldbill.Bill_Num,
                                    Client_Name = oldbill.Client_Name,
                                    Bill_State = oldbill.Bill_State,
                                    sell_State = false,
                                    Date = DateTime.Now,
                                    Exp_Date = DateTime.Now.AddMonths(3),
                                    Client_phone = oldbill.Client_phone,
                                    Total_Price = oldbill.Total_Price,
                                    Note = oldbill.Note,
                                };
                                billRepo.Update(oldbill.ID, billedit);
                            }
                            if (totalPayQnew == totalp)
                            {
                                Bill oldbill = billRepo.GetById(editpayedQ.Bill_ID);
                                Bill billedit = new Bill()
                                {
                                    ID = oldbill.ID,
                                    Bill_Num = oldbill.Bill_Num,
                                    Client_Name = oldbill.Client_Name,
                                    Bill_State = oldbill.Bill_State,
                                    sell_State = true,
                                    Date = DateTime.Now,
                                    Exp_Date = DateTime.Now.AddMonths(3),
                                    Client_phone = oldbill.Client_phone,
                                    Total_Price = oldbill.Total_Price,
                                    Note = oldbill.Note,
                                };
                                billRepo.Update(oldbill.ID, billedit);
                            }
                            if (result == 1)
                            {
                                TempData["editpayed"] = "payed quantity Edited succesfully";
                                //return RedirectToAction("Edit", new { id = editpayedQ.ID });
                            }
                            else
                            {
                                TempData["failpayedQEdit"] = " Sorry Edit process didn't complete successfully !";
                                //return RedirectToAction("Edit", new { id = editpayedQ.ID });
                            }
                        }
                        else
                        {
                            TempData["cantCreatePQ"] = " Sorry! You can't Edit this payed Quantity because your total price of this bill is " + totalp;
                            return RedirectToAction("Edit", new { id = editpayedQ.Bill_ID });
                        }
                    }
                    else
                    {
                        TempData["tpanpQareEqual"] = " the total payed Quantity you have entered are equal to total price =  " + totalp;
                        return RedirectToAction("Edit", new { id = editpayedQ.Bill_ID });
                    }
                }
              return RedirectToAction("Edit", new { id = editpayedQ.ID });
            }
            else
            {
                return View("Edit", editpayedQ);
            }
           
        }
        //=================================================

        public IActionResult Delete(int id)
        {
            Payed_quantity payedQ = payedQRepo.GetByQId(id);
            return View(payedQ);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult confirmDelete(Payed_quantity deletepayedQ)
        {

            double? totalp = billRepo.GetById(deletepayedQ.Bill_ID).Total_Price;
            int totalPayQ = payedQRepo.GetByBillId(deletepayedQ.Bill_ID).Sum(x => x.value);

            int billid = payedQRepo.GetByQId(deletepayedQ.ID).Bill_ID;
            int result = payedQRepo.Delete(deletepayedQ.ID);


            int totalPayQnew = payedQRepo.GetByBillId(deletepayedQ.Bill_ID).Sum(x => x.value);
            if (totalPayQnew < totalp)
            {
                Bill oldbill = billRepo.GetById(deletepayedQ.Bill_ID);
                Bill billedit = new Bill()
                {
                    ID = oldbill.ID,
                    Bill_Num = oldbill.Bill_Num,
                    Client_Name = oldbill.Client_Name,
                    Bill_State = oldbill.Bill_State,
                    sell_State = false,
                    Date = DateTime.Now,
                    Exp_Date = DateTime.Now.AddMonths(3),
                    Client_phone = oldbill.Client_phone,
                    Total_Price = oldbill.Total_Price,
                    Note = oldbill.Note,
                };
                billRepo.Update(oldbill.ID, billedit);
            }
            if (result == 1)
            {
                TempData["payedQDelete"] = "payed quantity deleted succesfully";
                return RedirectToAction("Details","Bills",new { id=billid});
            }
            else{
                TempData["failpayedQDelete"] = " Sorry the delete didn't complete successfully !";
                return RedirectToAction("Delete", deletepayedQ);
            }
         

        }

        //=================================================
    }
}






