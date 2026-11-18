using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Repository;
using System.ComponentModel.DataAnnotations;

namespace Project.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        ICategoryRepo catRepo;
        ISubCategoryRepo subCatRepo;
        IProductRepo proRepo;
        IPurchasingRepo purRepo;

        public CategoriesController(ICategoryRepo catRepo ,ISubCategoryRepo subCatRepo,IProductRepo proRepo,IPurchasingRepo purRepo)
        {
            this.catRepo = catRepo;
            this.subCatRepo = subCatRepo;
            this.proRepo = proRepo; 
            this.purRepo = purRepo; 
         
        }
        // GET: Categories
        public IActionResult Index()
        {
            List<Category> categories = catRepo.GetAll();
            return View(categories);
        }
        //===============================2
        public IActionResult Details(int id)
        {
            //ViewData["Category"] = catRepo.GetAll();
            Category listofsub = new Category();
            listofsub = catRepo.GetByIdWithSubCate(id);
            return View(catRepo.GetById(id));
        }
        //===============================3 Add new
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category newCategory)
        {
                if (ModelState.IsValid)
                {
                    catRepo.Insert(newCategory);
                    return RedirectToAction("Index");
                }
                else
                    return View(newCategory);
        }
        
        //================================ 4
        [HttpGet]
        public IActionResult Edit(int id)
        {
            //open view 
            Category editcat = catRepo.GetById(id);
            return View(editcat);
        }
        [HttpPost]
        public IActionResult saveEdit(int id, Category editcat)
        {
            if (ModelState.IsValid)
            {
                //Category editcategory = catRepo.GetById(id);
               
                    if (editcat == null)
                    {
                        return RedirectToAction("Edit", editcat);
                    }
                    else
                    {
                        catRepo.Update(id, editcat);
                        return RedirectToAction("Index");
                    }
                //}
            }
            return View("Edit",editcat);
            
              
            
           
        }
        //===============================5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            //open view 
            Category listofsub = new Category();
            listofsub = catRepo.GetByIdWithSubCate(id);
            return View(catRepo.GetById(id));

            //Category delcat = catRepo.GetById(id);
            //return View(delcat);
        }
        [HttpPost]
        public IActionResult SaveDelete(int id)
        {
            //open view 
            int subCatResult = subCatRepo.getAllByID(id).Count;
            if (subCatResult > 0)
            {
                TempData["delete subcat"] = "please delete sub categories first !";
                return RedirectToAction("Delete", new { id = id });
            }
            int proudctResult = proRepo.getAllBuCategoryID(id).Count;
            if (proudctResult > 0)
            {
                TempData["delete proudct"] = "please delete Product first !";
                return RedirectToAction("Delete", new { id = id });
            }
            int PurchasingResult = purRepo.getAllByCatID(id).Count;
            if (PurchasingResult > 0)
            {
                TempData["delete Purchasing"] = "please delete Purchasing first !";
                return RedirectToAction("Delete", new { id = id });
            }
            catRepo.Delete(id);
            return RedirectToAction("Index");

        }
        //================================6 search
        public IActionResult DeleteSubCategory(int id, int idCategory)
        {
            Category subDelete =catRepo.GetById(id);
            if(subDelete == null) { TempData["subNotExist"] = "Subcategory is not exist to delete it"; return RedirectToAction("Details", new { id = idCategory }); }
            int proudctResult = proRepo.getAllBySubCategoryID(id).Count;
            if (proudctResult > 0)
            {
                TempData["proudcthasSubCategory"] = "please, Delete Product who has same subcategory!";
                return RedirectToAction("Details", new { id = idCategory });
            }
            int PurchasingResult = purRepo.getAllBySubCatID(id).Count;
            if (PurchasingResult > 0)
            {
                TempData["purchashasSubCategory"] = "please delete Purchasing first !";
                return RedirectToAction("Details", new { id = idCategory });
            }
            subCatRepo.DeleteSubCategory(id);
            TempData["DelSubDone"] = "Deleted subcategory is Done Successfully";
            return RedirectToAction("Details", new { id = idCategory });
        }

    }
}
