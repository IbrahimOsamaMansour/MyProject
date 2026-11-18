using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project.Models;
using Project.Repository;
using Project.ViewModel;

namespace Project.Controllers
{
    [Authorize]
    public class BillProductController : Controller
    {
        private readonly IProductRepo productRepo;
        private readonly IBillRepo billRepo;
        private readonly IProductBillRepo ProductbillRepo;
        private readonly IProduct_Quantity_Repo ProductquantityRepo;

        public BillProductController(IProductRepo _proRepo, IBillRepo _billRepo , IProductBillRepo _probillRepo, IProduct_Quantity_Repo _payedquantityRepo)
        {
            productRepo = _proRepo;
            billRepo = _billRepo;
            ProductbillRepo = _probillRepo;
            ProductquantityRepo = _payedquantityRepo;
        }
        //**********************************************************************************
        //**********************************************************************************
        #region add product To Bill
        //id of bill
        [HttpGet]
        public IActionResult Add(int id)
        {
            ProductBillViewModel pbvm = new ProductBillViewModel()
            {
                bill = billRepo.GetById(id),
                Bill_ID = id,
                products = productRepo.getAll(),
                ProductBills = ProductbillRepo.GetByBillId(id),

            };
            return View(pbvm);
        }
        [HttpPost]
        public IActionResult Add(ProductBillViewModel newProductBill)
        {
            if(newProductBill.product_ID == 0 || newProductBill.unit_Quantity=="NULL")
            {
                if(newProductBill.product_ID == 0)
                {
                    ModelState.AddModelError("product_ID", "Please choose product");
                }
                if(newProductBill.unit_Quantity == "NULL") { ModelState.AddModelError("unit_Quantity", "Please choose unit quantity"); }

                newProductBill.bill = billRepo.GetById(newProductBill.Bill_ID);
                newProductBill.Bill_ID = billRepo.GetById(newProductBill.Bill_ID).ID;
                newProductBill.products = productRepo.getAll();
                newProductBill.ProductBills = ProductbillRepo.GetByBillId(newProductBill.Bill_ID);

               
                return View("Add", newProductBill);
            }
            else if (ModelState.IsValid)
            {
                Product_Bill pb = new Product_Bill()
                {
                    Bill_ID=newProductBill.Bill_ID,
                    product_ID =newProductBill.product_ID,
                    Quantity = newProductBill.Quantity,
                    unit_Quantity= newProductBill.unit_Quantity,
                    unit_Price = newProductBill.unit_Price,
                    Book_Date = newProductBill.Book_Date,
                    Resv_Q_State = false                     
                };
                //add product to bill
                int resultInsert = ProductbillRepo.Insert(pb);
                if(resultInsert == 1) 
                { 
                    TempData["sucAddProd"] = "Product is added in bill successfully";
                    //add quantity in reserved quantiity in product
                    int addToReservedQuuantity = productRepo.updateReservedbooking(newProductBill.Quantity, newProductBill.product_ID);
                    if(addToReservedQuuantity == 1) { 
                        TempData["sucAddProd"] += ", Add Quantity to reserved quntity in product ";
                        // calculate total price
                        double totalpriceProdct = (double)newProductBill.unit_Price * (double)newProductBill.Quantity;
                        int totalPrice = billRepo.UpdateTotalPrice(totalpriceProdct, newProductBill.Bill_ID);
                        if (addToReservedQuuantity == 1){TempData["sucAddProd"] += ", Add Price of Product to Bill ";}
                        }
                }
            }
            ProductBillViewModel pbvm = new ProductBillViewModel()
            {
                bill = billRepo.GetById(newProductBill.Bill_ID),
                Bill_ID = billRepo.GetById(newProductBill.Bill_ID).ID,
                products = productRepo.getAll(),
                ProductBills = ProductbillRepo.GetByBillId(newProductBill.Bill_ID),

            };
            return View(pbvm);
        }
        #endregion


        //**********************************************************************************
        //**********************************************************************************
        #region Delete Product from Bill
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product_Bill pb = ProductbillRepo.GetById(id);
            DeleteProductBillViewModel dpbvm = new DeleteProductBillViewModel();
            dpbvm.ID = id;
            dpbvm.product_Bill = pb;
            dpbvm.product = productRepo.getById(pb.product_ID);
            dpbvm.bill = billRepo.GetById(pb.Bill_ID);
            
            return View(dpbvm);
        }
        [HttpPost]
        public IActionResult Delete(DeleteProductBillViewModel product_BillViewModel)
        {
            Product_Bill pb = ProductbillRepo.GetById(product_BillViewModel.ID);
            int billID = pb.Bill_ID;
            if(pb.Resv_Q_State == false)// Not sold just booking  //update reserved quantity in product
            {
                //add quantity in reserved quantiity in product
                int addToReservedQuuantity = productRepo.updateReservedbooking(-(int)pb.Quantity, pb.product_ID);
                if(addToReservedQuuantity == 1) { TempData["resultDeleteProduct"] = "delete quantity from reserved quantity in product"; }
                else { TempData["resultDeleteProduct"] = "Sorry there are error happend during delete try again";
                    return RedirectToAction("Delete", new { id = product_BillViewModel.ID });
                }
            }
            else//product is already sold  //update real quantity in product
            {
                int resultupd=ProductquantityRepo.updateQuantitytoSellProductID(pb.product_ID, (int)pb.Quantity, pb.unit_Quantity);
                if(resultupd == 1) { TempData["resultDeleteProduct"] = "Add quantity of sold product to product"; }
                else { TempData["resultDeleteProduct"] = "Sorry there are error happend during delete try again";
                    return RedirectToAction("Delete", new { id = product_BillViewModel.ID });
                }
            }
            //update total price in bill
            double priceOfQuntty = (double)(pb.unit_Price * pb.Quantity);
           int resultbillupdate=  billRepo.UpdateTotalPrice(-priceOfQuntty, pb.Bill_ID);
            if(resultbillupdate == 1) { TempData["resultDeleteProduct"] += "Update Total Price of bill is Don Successfully"; }
            else { TempData["resultDeleteProduct"] = "Sorry Update Total Price of bill is Failed";
                return RedirectToAction("Delete", new { id = product_BillViewModel.ID });
            }
           int resultdelete =  ProductbillRepo.Delete(pb.ID);
            if(resultdelete == 1) {
                TempData["resultDeleteProduct"] += ", Delete PRoduct is done successfully";
                return RedirectToAction("Details" ,"Bills", new {id = billID });
            }
            else
            {
                TempData["resultDeleteProduct"] = "Delete product is faile";
                return RedirectToAction("Delete", new { id = product_BillViewModel.ID });
            }
            
        }
        #endregion



        //**********************************************************************************
        //**********************************************************************************
        #region Confirm Selling
        [HttpPost]
        [ValidateAntiForgeryToken]
        //id => billID
        public IActionResult ConfirmSelling(Bill bill)
        {
           
            List<Product_Bill> prodBill = ProductbillRepo.GetByBillId(bill.ID);
            if (prodBill.Count() != 0)
            {
                foreach (var item in prodBill)
                {
                    int resultProdBill = ProductbillRepo.UpdateReservedquantityState(item.ID);
                    if (resultProdBill != 1)
                    {
                        TempData["resultConfirm"] = "change state bill to sell";
                        return RedirectToAction("Details", "Bills", new { id = bill.ID });
                    }
                    int resulProductQuantReserv = productRepo.updateReservedSelling((int)item.Quantity, item.product_ID);
                    if (resulProductQuantReserv != 1)
                    {
                        TempData["resultConfirm"] = "update reserved quantity of product is failed";
                        return RedirectToAction("Details", "Bills", new { id = bill.ID });
                    }
                    int resultproductReslQuntity = ProductquantityRepo.updateQuantitytoSellProductID(item.product_ID, -(int)item.Quantity, item.unit_Quantity);
                    if (resultproductReslQuntity != 1)
                    {
                        TempData["resultConfirm"] = "update Real quantity of product is failed please try Again!";
                        return RedirectToAction("Details", "Bills", new { id = bill.ID });
                    }
                }

                //rturn bill to change bill state 
                Bill bi = billRepo.GetById(bill.ID);
                int resultBill = billRepo.UpdateBillState(bill.ID);
                if (resultBill != 1)
                {
                    TempData["resultConfirm"] = "change state bill to sell is failed please try again";
                    return RedirectToAction("Details", "Bills", new { id = bill.ID });
                }
            }
            else
            {
                TempData["resultConfirm"] = "You can't sold bill has no products!";
                return RedirectToAction("Details", "Bills", new { id = bill.ID });
            }
            TempData["resultConfirm"] = "update bill state to sold,  update state of booking to sell to product in bill, update reserved quantity in product,  update real quantity in product";
            return RedirectToAction("Details", "Bills", new { id = bill.ID });

        }
        #endregion




        //**********************************************************************************
        //**********************************************************************************

    }
}
