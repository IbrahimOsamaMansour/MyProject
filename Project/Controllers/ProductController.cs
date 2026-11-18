using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Repository;
using Project.ViewModel;

namespace Project.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepo productRepo;
        private readonly IProduct_Quantity_Repo quantity_Repo;
        private readonly ICompanyRepository companyRepository;
        private readonly ICategoryRepo categoryRepo;
        private readonly ISubCategoryRepo subCategoryRepo;
        private readonly IProductPurchasing productPurchasingRepo;
        private readonly IProductBillRepo prodBillRepo;

        public ProductController( IProductRepo _productRepo , IProduct_Quantity_Repo _Quantity_Repo , ICompanyRepository _companyRepository , ICategoryRepo _categoryRepo , ISubCategoryRepo _subCategoryRepo, IProductPurchasing productPurchasingRepo,IProductBillRepo productBillRepo)
        {
            productRepo = _productRepo;
            quantity_Repo = _Quantity_Repo;
            companyRepository = _companyRepository;
            categoryRepo = _categoryRepo;
            subCategoryRepo = _subCategoryRepo;
            this.productPurchasingRepo = productPurchasingRepo;
            prodBillRepo = productBillRepo;
        }

        //=================================================================================
        //=================================================================================
        #region Display All Product
        public IActionResult Index()
        {
           List<Product>products= productRepo.getAll();
            return View(products);
        }
        #endregion
        //=================================================================================
        //=================================================================================

        #region Product Detail
        public IActionResult Detail(int id)
        {
            Product prodduct = productRepo.getById(id);

            return View(prodduct);
        }
        #endregion
        //=================================================================================
        #region Add Product
        [HttpGet]
        public IActionResult add()
        {
            ProductViewModel newpurchesing = new ProductViewModel()
            {
                companies = companyRepository.GetAll(),
                Categories = categoryRepo.GetAll()
            };
            return View(newpurchesing);
        }
        [HttpPost]
        public IActionResult add(ProductViewModel newProduct)
        {
            if (newProduct.Comp_Id == 0 || newProduct.Categ_Id == 0)
            {
                if (newProduct.Comp_Id == 0) { ModelState.AddModelError("Comp_Id", "Please add Company"); }
                if (newProduct.Categ_Id ==  0) { ModelState.AddModelError("Categ_Id", "Please add Category"); }

                newProduct.companies = companyRepository.GetAll();
                newProduct.Categories = categoryRepo.GetAll();
                return View(newProduct);
            }
            if (ModelState.IsValid)
            {
                Product pur = new Product()
                {
                    ModelName = newProduct.ModelName,
                    Name = newProduct.Name,
                    Number = newProduct.Number,
                    Comp_Id = newProduct.Comp_Id,
                    Description = newProduct.Description,
                    ReservedQuantity=0

                    //Emp_id = User.Identity.Name

                };
                pur.subCateg_id = (newProduct.subCateg_id != 0) ? newProduct.subCateg_id : null;
                pur.Categ_Id = (newProduct.Categ_Id != 0) ? newProduct.Categ_Id : null;
                pur.Comp_Id = (newProduct.Comp_Id != 0) ? newProduct.Comp_Id : null;
                int result = productRepo.Insert(pur);
                if (result != 0) { TempData["ProAdd"] = "Product is Add Successfully"; return RedirectToAction("addQuantity", new { id = result }); }
                else { ViewBag.fail = "fail"; }
            }



            newProduct.companies = companyRepository.GetAll();
            newProduct.Categories = categoryRepo.GetAll();
            return View(newProduct);
        }
        #endregion
        //=================================================================================
        //=================================================================================

        #region Add quantity for Product
        [HttpGet]
        public IActionResult addQuantity(int id)
        {
            PurchasingQuantityViewModel pqvm = new PurchasingQuantityViewModel();
            pqvm.Product = productRepo.getById(id);
            pqvm.Product_id = id;
            return View(pqvm);
        }
        [HttpPost]
        public IActionResult addQuantity(PurchasingQuantityViewModel newpurchesing)
        {
            if(newpurchesing.UnitQuantity == "null") {
                ModelState.AddModelError("UnitQuantity", "Choose unite please");
                newpurchesing.Product = productRepo.getById(newpurchesing.Product_id);
                newpurchesing.Product_id = newpurchesing.Product_id;
                return View(newpurchesing);
            }
            else if (ModelState.IsValid)
            {
                if (newpurchesing.ValueQuantity < 1)
                {
                    TempData["valQua"] = "Value invaled";
                }
                else
                {
                int numQuantity = quantity_Repo.getAllBuProductID(newpurchesing.Product_id).Count();
                if (numQuantity > 1) 
                {
                    TempData["Qua"]="You can't Add Quuantity you already add two  Quantity Before";
                }
                else
                {

                    Product_Quantity pq = new Product_Quantity()
                    {
                        Prod_Id = newpurchesing.Product_id,
                        ValueQuantity = newpurchesing.ValueQuantity,
                        UnitQuantity = newpurchesing.UnitQuantity ?? ""
                    };

                    int result = quantity_Repo.insert(pq);
                    if (result == 1) { TempData["suc"] = "Done"; }
                    else { TempData["fail"] = "Sorry! Add Quantity is failed"; }
                }
                }
            }
            return RedirectToAction("addQuantity", new { id = newpurchesing.Product_id });
        }
        #endregion
        //=================================================================================
        //=================================================================================
        #region Edit Quantity
        [HttpGet]
        public IActionResult editQuantity(int id)
        {
            Product_Quantity pq = quantity_Repo.getById(id);

            PurchasingQuantityViewModel pqvm = new PurchasingQuantityViewModel();
            pqvm.Product_id = (int)pq.Prod_Id;
            pqvm.ID = pq.Id;
            pqvm.ValueQuantity = pq.ValueQuantity;
            pqvm.UnitQuantity = pq.UnitQuantity;
            return View(pqvm);
        }
        [HttpPost]
        public IActionResult editQuantity(PurchasingQuantityViewModel editPquantity)
        {
            if (editPquantity.UnitQuantity == "null")
            {
                ModelState.AddModelError("UnitQuantity", "Please choose Unit");
                Product_Quantity pq = quantity_Repo.getById(editPquantity.ID);
                editPquantity.Product_id = (int)pq.Prod_Id;
                editPquantity.ID = pq.Id;
                return View(editPquantity);
            }
            if (ModelState.IsValid)
            {
                if (editPquantity.ValueQuantity < 1)
                {
                    TempData["valQua"] = "Please add value";
                }else{

                    Product_Quantity newpq = new Product_Quantity()
                    {
                        ValueQuantity = editPquantity.ValueQuantity,
                        UnitQuantity = editPquantity.UnitQuantity,
                        Id = editPquantity.ID,
                        Prod_Id = editPquantity.Product_id
                    };
                    int result = quantity_Repo.update(newpq);
                    if (result == 1) { TempData["editQuantity"] = "Edit Quantity is Done"; return RedirectToAction("Detail", new { id = newpq.Prod_Id }); }
                    else { ViewBag.fail = "fail"; }
                }
            }
            return View(editPquantity);
        }
        #endregion

        //=================================================================================
        //=================================================================================
        #region delete  Quantity
        public IActionResult deleteQuantity(int id, int idpro)
        {

            int result = quantity_Repo.delete(id);
            if (result == 1) { TempData["delq"] = "Delete Quantity is success"; }
            else { TempData["fail"] = "Delete Quantity is Faild"; }
            
            return RedirectToAction("Detail", new { id = idpro });
        }
        #endregion

        //=================================================================================
        //=================================================================================
        #region delete  Product
        public IActionResult deleteProduct(int id)
        {
            int quantitypurchaching = quantity_Repo.getAllBuProductID(id).Count();
            if (quantitypurchaching != 0) { TempData["delete"] = "Please Delete All Quantity in this Product"; return RedirectToAction("Detail", new { id = id }); }
            int proBillRewsult = prodBillRepo.GetByProductId(id).Count();
            if (proBillRewsult != 0) { TempData["delete"] = "Please Delete this product from all bills"; return RedirectToAction("Detail", new { id = id }); }
            //  Product p = productRepo.
            //int s = productRepo.update(p);
            List<Product_Purchasing> produPurch = productPurchasingRepo.getByPurchId(id);
            if (produPurch.Count != 0)
            {
                foreach (Product_Purchasing item in produPurch)
                {
                    productPurchasingRepo.delete(item.Id);
                }
            }

            int? deleteResult = productRepo.delete(id);
            if (deleteResult == 1) { TempData["sucDelete"] = "Delete Product is Done Succesfully"; return RedirectToAction("Index"); }
            else { TempData["delfail"] = "Delete Product is Failed"; }
            return RedirectToAction("Detail", new { id = id });
        }
        #endregion

        //=================================================================================
        //=================================================================================
        #region Edit Product
        [HttpGet]
        public IActionResult editProduct(int id)
        {
            Product p = productRepo.getById(id);
            ProductViewModel newproduct = new ProductViewModel()
            {
                companies = companyRepository.GetAll(),
                Categories = categoryRepo.GetAll(),
                subCategories = subCategoryRepo.getAllByID((int)p.Categ_Id),
                Id = p.Id,
                Name = p.Name,
                ModelName = p.ModelName,
                Comp_Id = (int)p.Comp_Id,
                Categ_Id = (int)p.Categ_Id,
                subCateg_id = p.subCateg_id,
                Number = p.Number,
                Description = p.Description,
                ReservedQuantity = p.ReservedQuantity

            };
            return View(newproduct);
        }

        [HttpPost]
        public IActionResult editProduct(ProductViewModel editproduct)
        {
            if (ModelState.IsValid)
            {
                Product pur = new Product()
                {
                    Id = (int)editproduct.Id,
                    Name = editproduct.Name,
                    ModelName = editproduct.ModelName,
                    Number = editproduct.Number,
                    Description = editproduct.Description,
                    ReservedQuantity = editproduct.ReservedQuantity

                };
                pur.subCateg_id = (editproduct.subCateg_id != 0) ? editproduct.subCateg_id : null;
                pur.Categ_Id = (editproduct.Categ_Id != 0) ? editproduct.Categ_Id : null;
                pur.Comp_Id = (editproduct.Comp_Id != 0) ? editproduct.Comp_Id : null;
                int result = productRepo.update(pur);
                if (result != 0) { TempData["suc"] = "Done"; }
                else { TempData["fail"] = "fail"; }


            }
            else
            {
                Product p = productRepo.getById(editproduct.Id);
                ProductViewModel newproduct = new ProductViewModel()
                {
                    companies = companyRepository.GetAll(),
                    Categories = categoryRepo.GetAll(),
                    subCategories = subCategoryRepo.getAllByID((int)p.Categ_Id),
                    Id = p.Id,
                    Name = p.Name,
                    ModelName = p.ModelName,
                    Comp_Id = (int)p.Comp_Id,
                    Categ_Id = (int)p.Categ_Id,
                    subCateg_id = p.subCateg_id,
                    Number = p.Number,
                    Description = p.Description,
                    ReservedQuantity = p.ReservedQuantity

                };
                return View(newproduct);
            }
           

            return RedirectToAction("editProduct", new { id = editproduct.Id });
        }

        #endregion
        //=================================================================================
        //=================================================================================
     
    }
}
