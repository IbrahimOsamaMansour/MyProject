using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Repository;
using Project.ViewModel;

namespace Project.Controllers
{
    [Authorize]
    public class PurchasingController : Controller
    {
        private readonly IPurchasingRepo purchasingRepo;
        private readonly ICompanyRepository companyRepo;
        private readonly ISubCategoryRepo subCategoryRepo;
        private readonly IPurchasingQuantity purchasingQuantity;
        private readonly IProductPurchasing productPurchasing;
        private readonly IProductRepo productRepo;
        private readonly ICategoryRepo CategoryRepo;
        private readonly IProduct_Quantity_Repo product_Quantity_Repo;

        public PurchasingController(IPurchasingRepo _iIPurchasing, ICompanyRepository _companyRepo, ICategoryRepo _categoryRepo, ISubCategoryRepo _subCategoryRepo, IPurchasingQuantity _purchasingQuantity, IProductPurchasing _productPurchasing, IProductRepo _productRepo, IProduct_Quantity_Repo productQuantityRepo)
        {
            purchasingRepo = _iIPurchasing;
            companyRepo = _companyRepo;
            CategoryRepo = _categoryRepo;
            subCategoryRepo = _subCategoryRepo;
            purchasingQuantity = _purchasingQuantity;
            productPurchasing = _productPurchasing;
            productRepo = _productRepo;
            product_Quantity_Repo = productQuantityRepo;
        }

        // ============================================================================================
        #region Purchasing display all
        public IActionResult Index()
        {
            List<Purchasing> per = purchasingRepo.getAll();

            return View(per);
        }
        #endregion

        // ============================================================================================
        // ============================================================================================
        #region Purchasing Detail
        public IActionResult Detail( int id)
        {
            Purchasing per = purchasingRepo.getByIdwithAlQuantity(id);

            return View(per);
        }
        #endregion

        // ============================================================================================

        #region Add Purchasing
        [HttpGet]
        public IActionResult add()
        {
            PurchesingViewModel newpurchesing = new PurchesingViewModel()
            {
                companies = companyRepo.GetAll(),
                Categories = CategoryRepo.GetAll()
            };
            return View(newpurchesing);
        }
        [HttpPost]
        public IActionResult add(PurchesingViewModel newpurchesing)
        {
            if(newpurchesing.Comp_id == 0 || newpurchesing.Categ_id ==0)
            {
                if(newpurchesing.Comp_id == 0) { ModelState.AddModelError("Comp_id", "Please add Company"); }
                if(newpurchesing.Categ_id == 0) { ModelState.AddModelError("Categ_id", "Please add Category"); }

                newpurchesing.companies = companyRepo.GetAll();
                newpurchesing.Categories = CategoryRepo.GetAll();
                return View(newpurchesing);
            }

            else if (ModelState.IsValid)
            {


                Purchasing pur = new Purchasing()
                {
                    ModelName = newpurchesing.ModelName,
                    Name = newpurchesing.Name,
                    BillNum = newpurchesing.BillNum,
                    Number = newpurchesing.Number,
                    TotalPrice = 0,
                    Comp_id = newpurchesing.Comp_id,
                    Description = newpurchesing.Description,
                    DateSell = DateTime.Now,
                    //Emp_id = User.Identity.Name

                };
                pur.subCateg_id =(newpurchesing.subCateg_id != 0) ? newpurchesing.subCateg_id : null;
                pur.Categ_id =(newpurchesing.Categ_id != 0) ? newpurchesing.Categ_id : null;
                pur.Comp_id = (newpurchesing.Comp_id != 0) ? newpurchesing.Comp_id : null;
                int result = purchasingRepo.insert(pur);
                if (result != 0) { ViewBag.suc = "Done";  return RedirectToAction("addQuantity", new { id = result }) ; }
                else { ViewBag.fail = "fail"; }

             
            }
            PurchesingViewModel newsing = new PurchesingViewModel()
            {
                companies = companyRepo.GetAll(),
                Categories = CategoryRepo.GetAll()
            };
            return View(newsing);

        }

        #endregion
        // ============================================================================================

        // ============================================================================================

        #region Edit Purchasing
        [HttpGet]
        public ActionResult editPurchaching(int id)
        {
            Purchasing p = purchasingRepo.getById(id);
            PurchesingViewModel newpurchesing = new PurchesingViewModel();
            newpurchesing.companies = companyRepo.GetAll();
            newpurchesing.Categories = CategoryRepo.GetAll();
            newpurchesing.ID = p.ID;
            newpurchesing.Name = p.Name;
            newpurchesing.ModelName = p.ModelName;            
            newpurchesing.subCateg_id = p.subCateg_id;
            newpurchesing.Number = p.Number;
            newpurchesing.BillNum = p.BillNum;
            newpurchesing.Description = p.Description;
            newpurchesing.TotalPrice = p.TotalPrice;

            
            if(p.Categ_id != null) { newpurchesing.Categ_id = (int)p.Categ_id; newpurchesing.subCategories = subCategoryRepo.getAllByID((int)p.Categ_id); }
            else { newpurchesing.subCategories = subCategoryRepo.getAllByID(0); }
            if(p.Comp_id != null) { newpurchesing.Comp_id = (int)p.Comp_id; }
            return View(newpurchesing);
        }

        [HttpPost]
        public IActionResult editPurchaching(PurchesingViewModel editpurchesing)
        {
            if (ModelState.IsValid)
            {
                Purchasing pur = new Purchasing()
                {
                    ID = (int)editpurchesing.ID,
                    Name = editpurchesing.Name,
                    ModelName = editpurchesing.ModelName,
                    Number = editpurchesing.Number,
                    BillNum = editpurchesing.BillNum,
                    Description = editpurchesing.Description
                    // DateSell = DateTime.Now,
                    //Emp_id = User.Identity.Name

                };
                pur.subCateg_id = (editpurchesing.subCateg_id != 0) ? editpurchesing.subCateg_id : null;
                pur.Categ_id = (editpurchesing.Categ_id != 0) ? editpurchesing.Categ_id : null;
                pur.Comp_id = (editpurchesing.Comp_id != 0) ? editpurchesing.Comp_id : null;
                int result = purchasingRepo.update(pur);
                if (result != 0) { TempData["suc"] = "Done";}
                else {TempData["fail"] = "fail"; }


            }
            else
            {
                Purchasing p = purchasingRepo.getById((int)editpurchesing.ID);
                PurchesingViewModel newpurchesing = new PurchesingViewModel();
                newpurchesing.companies = companyRepo.GetAll();
                newpurchesing.Categories = CategoryRepo.GetAll();
                newpurchesing.ID = p.ID;
                newpurchesing.Name = p.Name;
                newpurchesing.ModelName = p.ModelName;
                newpurchesing.subCateg_id = p.subCateg_id;
                newpurchesing.Number = p.Number;
                newpurchesing.BillNum = p.BillNum;
                newpurchesing.Description = p.Description;
                newpurchesing.TotalPrice = p.TotalPrice;


                if (p.Categ_id != null) { newpurchesing.Categ_id = (int)p.Categ_id; newpurchesing.subCategories = subCategoryRepo.getAllByID((int)p.Categ_id); }
                else { newpurchesing.subCategories = subCategoryRepo.getAllByID(0); }
                if (p.Comp_id != null) { newpurchesing.Comp_id = (int)p.Comp_id; }
                return View(newpurchesing);
            }

            return RedirectToAction("editPurchaching", new { id = editpurchesing.ID });
        }

        #endregion
        // ============================================================================================

        // ============================================================================================

        #region Add Purchasing Quantity
        [HttpGet]
        public IActionResult addQuantity(int id )
        {
            PurchasingQuantityViewModel pqvm = new PurchasingQuantityViewModel(); 
            pqvm.purchasing = purchasingRepo.getByIdwithAlQuantity(id);
            pqvm.Purch_id = id;
            return View(pqvm);
        }
        [HttpPost]
        public IActionResult addQuantity(PurchasingQuantityViewModel newpurchesing)
        {
            if (newpurchesing.UnitQuantity == "null")
            {
                ModelState.AddModelError("UnitQuantity", "Choose unite please");
                newpurchesing.purchasing = purchasingRepo.getByIdwithAlQuantity(newpurchesing.Purch_id);
                newpurchesing.Purch_id = newpurchesing.Purch_id;
                //newpurchesing.ValueQuantity = newpurchesing.ValueQuantity;
                return View(newpurchesing);
            }
            if (!newpurchesing.UnitPrice.HasValue || newpurchesing.UnitPrice <= 0)
            {
                ModelState.AddModelError(nameof(newpurchesing.UnitPrice), "Enter a unit price greater than zero.");
            }
            if (!ModelState.IsValid)
            {
                newpurchesing.purchasing = purchasingRepo.getByIdwithAlQuantity(newpurchesing.Purch_id);
                return View(newpurchesing);
            }
            if (ModelState.IsValid)
            {
                int numQuantity = purchasingQuantity.getAllBuPurchID(newpurchesing.Purch_id).Count();
                if (numQuantity > 1)
                {
                    TempData["Qua"] = "You can't Add Quuantity you already add two  Quantity Before";
                }else if (newpurchesing.ValueQuantity < 1)
                {
                    TempData["valQua"] = "Please add value";
                }
                else
                {
                    Purchasing_Quantity pq = new Purchasing_Quantity()
                    {
                        Purch_id = newpurchesing.Purch_id,
                        ValueQuantity = newpurchesing.ValueQuantity,
                        UnitQuantity = newpurchesing.UnitQuantity ?? "",
                        UnitPrice = newpurchesing.UnitPrice
                    };

                    int result = purchasingQuantity.insert(pq);
                    if (result == 1)
                    {
                        purchasingRepo.RecalculateTotalPrice(newpurchesing.Purch_id);
                        ViewBag.suc = "Done";
                    }
                    else { ViewBag.fail = "fail"; }
                }
              


            }
            return RedirectToAction("addQuantity", new {id= newpurchesing.Purch_id });

        }

        #endregion
        // ============================================================================================
        // ============================================================================================

        #region Add Purchasing to product
        [HttpGet]
        public IActionResult addPurchasingProduct(int id)
        {
            ProductPurchasingViewModel ppvm = new ProductPurchasingViewModel();
            ppvm.purchasing = purchasingRepo.getByIdwithAlQuantity(id);
            ppvm.product_purchasing = productPurchasing.getById(id);
            ppvm.Purchasing_Id = id;
            return View(ppvm);
        }
        [HttpPost]
        public IActionResult addPurchasingProduct(ProductPurchasingViewModel newpurchesing)
        {

          Purchasing purchasing = purchasingRepo.getById((int)newpurchesing.Purchasing_Id);
            if(purchasing == null) { ViewBag.fail = "fail"; }
            else {
                    Product product = new Product() 
                    { 
                        ModelName = purchasing.ModelName,
                        Name = purchasing.Name,
                        Categ_Id = purchasing.Categ_id,
                        subCateg_id = purchasing.subCateg_id,
                        Comp_Id = purchasing.Comp_id,
                        Description = purchasing.Description,
                        Number = purchasing.Number,
                        ReservedQuantity=0
                    
                    };
               int result =  productRepo.Insert(product);
                if (result >0)
                {
                    List<Purchasing_Quantity> pqs = purchasingQuantity.getAllBuPurchID(purchasing.ID);
                    if(pqs.Count != 0)
                    {
                        foreach (var item in pqs)
                        {
                            Product_Quantity pq = new Product_Quantity()
                            {
                                 Prod_Id = result,
                                 ValueQuantity = item.ValueQuantity,
                                 UnitQuantity = item.UnitQuantity,
                                 UnitCost = item.UnitPrice
                            };
                                product_Quantity_Repo.insert(pq);

                        }
                    }

                    Product_Purchasing productPurch= new Product_Purchasing() {
                    product_Id = product.Id,
                    Purchasing_Id = newpurchesing.Purchasing_Id
                    };
                  int result2 =  productPurchasing.insert(productPurch);
                    if(result2 == 1) { return RedirectToAction("addPurchasingProduct", new { id = newpurchesing.Purchasing_Id }); }
                }
            }
            return RedirectToAction("addPurchasingProduct", new { id = newpurchesing.Purchasing_Id });

        }

        #endregion



        // ============================================================================================
        // ============================================================================================

        #region Delete Purchasing Quantity
        //[HttpGet]
        //public IActionResult deleteQuantity(int id , int prchId)
        //{
        //    PurchasingQuantityViewModel pqvm = new PurchasingQuantityViewModel();
        //    pqvm.purchasing = iIPurchasingRepo.getByIdwithAlQuantity(id);
        //    pqvm.Purch_id = id;
        //    return View(pqvm);
        //}
        #endregion


        // ============================================================================================
        #region delete  Quantity
        public IActionResult deleteQuantity(int id , int idPur)
        {

            int result =  purchasingQuantity.delete(id);
            if(result == 1)
            {
                purchasingRepo.RecalculateTotalPrice(idPur);
                ViewBag.suc = "done";
            }
            else { ViewBag.fail = "fail";  }
            return RedirectToAction("Detail", new { id = idPur });
        }
        #endregion
        // ============================================================================================
        // ============================================================================================

        #region Edit Quantity
        [HttpGet]
        public IActionResult editQuantity(int id)
        {
            Purchasing_Quantity pq = purchasingQuantity.getById(id);
            PurchasingQuantityViewModel pqvm = new PurchasingQuantityViewModel();
            pqvm.Purch_id = pq.Purch_id;
            pqvm.ID = pq.ID;
            pqvm.ValueQuantity = pq.ValueQuantity;
            pqvm.UnitQuantity = pq.UnitQuantity;
            pqvm.UnitPrice = pq.UnitPrice;
            return View(pqvm);
        }
        [HttpPost]
        public IActionResult editQuantity(PurchasingQuantityViewModel editPquantity)
        {
            if(editPquantity.UnitQuantity == "null")
            {
                ModelState.AddModelError("UnitQuantity", "please choose unit");
                Purchasing_Quantity pq = purchasingQuantity.getById(editPquantity.ID);
                editPquantity.Purch_id = pq.Purch_id;
                editPquantity.ID = pq.ID;
                //editPquantity.ValueQuantity = pq.ValueQuantity;
                //editPquantity.UnitQuantity = pq.UnitQuantity;
                return View(editPquantity);
            }
            if (!editPquantity.UnitPrice.HasValue || editPquantity.UnitPrice <= 0)
            {
                ModelState.AddModelError(nameof(editPquantity.UnitPrice), "Enter a unit price greater than zero.");
            }
            if (ModelState.IsValid)
            {
                if (editPquantity.ValueQuantity < 1)
                {
                    TempData["valQua"] = "Please add value";
                }
                else
                {
                    Purchasing_Quantity newpq = new Purchasing_Quantity()
                    {
                        ValueQuantity = editPquantity.ValueQuantity,
                        UnitQuantity = editPquantity.UnitQuantity,
                        UnitPrice = editPquantity.UnitPrice,
                        ID = editPquantity.ID,
                        Purch_id = editPquantity.Purch_id
                    };
                    int result = purchasingQuantity.update(newpq);
                    if (result == 1)
                    {
                        purchasingRepo.RecalculateTotalPrice(newpq.Purch_id);
                        TempData["editQuantity"] = "Edit Quantity is Done";
                        return RedirectToAction("Detail", new { id = newpq.Purch_id });
                    }
                    else { ViewBag.fail = "fail"; }
                }
            }
           
            return View(editPquantity);
        }
        #endregion

        // ============================================================================================
        // ============================================================================================
        // ============================================================================================
        #region delete  Purchasing
        public IActionResult deletePurchasing(int id)
        {
            int quantitypurchaching = purchasingQuantity.getAllBuPurchID(id).Count();
            if(quantitypurchaching != 0) { TempData["delete"]= "Please Delete All Quantity in this Purchasing"; return RedirectToAction("Detail", new { id = id }); }

            List<Product_Purchasing> produPurch = productPurchasing.getByPurchId(id);
              if(produPurch.Count != 0)
                {
                    foreach (Product_Purchasing item in produPurch)
                    {
                         productPurchasing.delete(item.Id);
                    }
                }

            int deleteResult = purchasingRepo.delete(id);
            if(deleteResult == 1) { TempData["sucDelete"] ="Delete Purchasing is Done Succesfully"; return RedirectToAction("Index"); }
            else { ViewBag.delfail = "fail"; }
            return RedirectToAction("Detail", new { id = id });
        }
        #endregion
        // ============================================================================================



        [HttpPost]
        public IActionResult getsubcategorys(int id)
        {
            PurchesingViewModel newpurchesing = new PurchesingViewModel()
            {
                companies = companyRepo.GetAll(),
                Categories = CategoryRepo.GetAll(),
                subCategories = subCategoryRepo.getAllByID(id)
            };
            return PartialView("_subCategories", newpurchesing);
        }
    }
}
