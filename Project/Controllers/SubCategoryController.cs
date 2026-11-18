using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Models;
using Project.Repository;
using Project.Services;

namespace Project.Controllers
{
    [Authorize]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService subCategoryService;
        private readonly ICategoryRepo catRepo;
        private readonly ISubCategoryRepo subCatRepo;
        private readonly IProductRepo proRepo;
        private readonly IPurchasingRepo purRepo;



        public SubCategoryController(ISubCategoryService subCategoryService , ICategoryRepo carep , ISubCategoryRepo suCare, IProductRepo _proRepo, IPurchasingRepo _purRepo)
        {
            this.subCategoryService = subCategoryService;
            catRepo = carep;
            subCatRepo = suCare;
            proRepo = _proRepo;
            purRepo = _purRepo;
        }
        public IActionResult Index()
        {
            ViewBag.categories = new SelectList(subCategoryService.GetAllCategories(), "Id", "Name");
            return View(subCategoryService.GetAllSubCategories());
        }
        public IActionResult Details(int id)
        {
            return View(subCategoryService.GetSubCategoryById(id));
        }
        [HttpGet]
        public IActionResult Create(int? id)
        {
            ViewBag.categories = new SelectList(subCategoryService.GetAllCategories(), "Id", "Name");
            Category c = new Category();
            if (id!=0)
            {
               c.Suc_Categ_Id = id;
            }
            return View(c);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if(category.Suc_Categ_Id != null)
                {
                    category.Id = 0;
                    subCategoryService.AddSubCategory(category);
                    TempData["Added"] = "Added Sucessfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Please Select Category");
                }
                
            }
            ViewBag.categories = new SelectList(subCategoryService.GetAllCategories(), "Id", "Name");
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.categories = new SelectList(subCategoryService.GetAllCategories(), "Id", "Name");
            Category category = subCategoryService.GetSubCategoryById(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                subCategoryService.EditSubCategory(category.Id,category);
                TempData["Edit"] = "Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.categories = new SelectList(subCategoryService.GetAllCategories(), "Id", "Name");
            return View(category);
        }
        public IActionResult Delete(int id)
        {
            Category subDelete = catRepo.GetById(id);
            if (subDelete == null) 
            { 
                TempData["subNotExist"] = "Subcategory is not exist to delete it"; 
                return RedirectToAction(nameof(Index));
            }
            int proudctResult = proRepo.getAllBySubCategoryID(id).Count;
            if (proudctResult > 0)
            {
                TempData["proudcthasSubCategory"] = "please, Delete Product who has same subcategory!";
                return RedirectToAction(nameof(Index));
            }
            int PurchasingResult = purRepo.getAllBySubCatID(id).Count;
            if (PurchasingResult > 0)
            {
                TempData["purchashasSubCategory"] = "please delete Purchasing first !";
                return RedirectToAction(nameof(Index));
            }

            subCategoryService.DeleteSubCategory(id);
            TempData["Delete"] = "Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Filter(int id)
        {
            List<Category> subca = subCategoryService.FilterSubCategoryPerCategory(id);
            return PartialView("_subCategoryFilter", subca);
        }
    }
}
