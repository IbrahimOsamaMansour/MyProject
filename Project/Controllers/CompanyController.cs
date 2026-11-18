using Microsoft.AspNetCore.Mvc;
using Project.Repository;
using Project.ViewModel;
using Project.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.OleDb;
using System.Runtime.Intrinsics.X86;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;

namespace Project.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {


        ICompanyRepository COMRepo;
        ICompanyPhoneRepository COMPhoneRepo;
        IProductRepo productRepo;
        IPurchasingRepo purchasingRepo;

        public CompanyController(ICompanyRepository COMRepo, ICompanyPhoneRepository COMPhoneRepo, IProductRepo productRepo, IPurchasingRepo purchasingRepo)
        {
            this.COMRepo = COMRepo;
            this.COMPhoneRepo = COMPhoneRepo;
            this.productRepo = productRepo;
            this.purchasingRepo = purchasingRepo;
        }
        //============================== 1





        public IActionResult Index()
        {
            ViewData["Phoneitem"] = COMPhoneRepo.GetAll();
            List<Company> companies = COMRepo.GetAll();
            return View(companies);
        }





        //============================== 2






        public IActionResult Details(int id)
        {
            ViewData["Phoneitem"] = COMPhoneRepo.GetAll();

            return View(COMRepo.GetById(id));
        }







        //===============================3
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Company newcompany)
        {

            if (ModelState.IsValid)
            {
                int NameExist = COMRepo.FindName(newcompany.Name).Count();
                if (NameExist !=0)
                {
                    ModelState.AddModelError("", "this  name company is Exist");
                    return View("Create");

                }
                COMRepo.Insert(newcompany);
                return RedirectToAction("Index");
            }
            else
                return View(newcompany);

        }






        public IActionResult AddPhone(int id)
        {

            Company_Phone company_Phone = new Company_Phone()
            {
                COMP_ID = id,
            };
            return View(company_Phone);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult AddPhone(Company_Phone company_Phone)
        {
            if (ModelState.IsValid)
            {

                Company_Phone company_Phone1 = new Company_Phone()
                {
                    Phone = company_Phone.Phone,
                    COMP_ID = company_Phone.COMP_ID,
                };


                COMPhoneRepo.Insert(company_Phone1);

                return RedirectToAction("Details" , new {id = company_Phone1.COMP_ID });

            }
            else
                return View(company_Phone);

        }


     









        //================================ 4




        public IActionResult Edit(int id)
        {
            //open view 
            Company company  = COMRepo.GetById(id);

            return View(company);
        }
        [HttpPost]

        public IActionResult Edit(int id, Company company)
        {
           

            if(ModelState.IsValid)
            {
                COMRepo.Update(id, company);
                return RedirectToAction("Details", new {id= company.ID});
                //Company oldcompany = COMRepo.GetById(id);
                //ModelState.AddModelError("", "this  name company is  not change");
                ////return RedirectToAction("Edit" , company);

            }
            else
            {
                return RedirectToAction("Edit", company);
               
            }


            //if (company == null)
            //{
            //    return RedirectToAction("Edit");

            //}
            //else
            //COMRepo.Update(id, company);
            //return RedirectToAction("Index");
        }











        // Editphone
        public IActionResult Editphone(int id)
        {
            Company_Phone company_Phone = COMPhoneRepo.GetByIdPH(id);

            //open view 
            //CompanyPhoneViewModel phoneViewModel = new CompanyPhoneViewModel()
            //{
            //    OldPhone = company_Phone.Phone,
            //    Phone = company_Phone.Phone,
            //    COMP_ID = company_Phone.COMP_ID
            //};

            return View(company_Phone);
        }


        [HttpPost]

        public IActionResult Editphone(int id, Company_Phone pcompany)
        {
            Company_Phone oldcompany = COMPhoneRepo.GetByIdPH(id);

            if (pcompany.Phone.Equals(oldcompany.Phone))
            {
                ModelState.AddModelError("", "this  name company is  not change");
                ModelState.AddModelError("","number is not change ");
                return View("Editphone", pcompany);
                //return RedirectToAction("Editphone");

            }
            else
            {

                
                    COMPhoneRepo.Update(id, pcompany);
                    return RedirectToAction("Details", new { id = pcompany.COMP_ID });
                
            }


            //if (pcompany == null)
            //{
            //    return RedirectToAction("Edit");

            //}
            //else
            //    COMPhoneRepo.Update(id, pcompany);
            //return RedirectToAction("Index");
        }












            //===============================5
            public IActionResult Delete(int id)
            {
                //open view 
           
                Company company = COMRepo.GetById(id);

                ViewData["Phoneitem"] = COMPhoneRepo.GetAll();
                return View(company);
            }

        [HttpPost]
        public IActionResult vDelete(int id)
        {
            //open view 
            //int result = COMRepo.Delete(id);
            //   if(result != 1)
            //   { 
            //       TempData["delFail"] = "Sorry Delete is Not Done"; 
            //       return RedirectToAction("Delete",new { id = id }); 
            //   }
            //   //========================== phone
            //   TempData["delsuc"] = "Delete Company is Done";
            //   return RedirectToAction("Index");

            int result1 = productRepo.getAllCompanyID(id).Count();
            if (result1 != 0)
            {
                TempData["deleteProduct"] = "Please Delete All Product who has same company";
                return RedirectToAction("Delete", new { id = id });
            }
            int result2 = purchasingRepo.getAllCompanyID(id).Count();
            if (result2 != 0)
            {
                TempData["deletepurchasing"] = "Please, Delete all purchasing who has same company";
                return RedirectToAction("Delete", new { id = id });
            }

            int result3 = COMPhoneRepo.getAllCompanyID(id).Count();
            if (result3 != 0)
            {
                TempData["deletePhone"] = "Please, Delete all phone in this company";
                return RedirectToAction("Delete", new { id = id });
            }


            int result = COMRepo.Delete(id);
            if (result == 1)
            {
                TempData["delsuc"] = "Delete company is Done Succesfully";
                return RedirectToAction("Index");
            }
            else { ViewBag.delfail = " fail"; }
            return RedirectToAction("Delete", new { id = id });








        }










        public IActionResult DeletePhone(int id  )
        {
            //open view 

            Company_Phone companyphone = COMPhoneRepo.GetByIdPH(id );
            int compID = companyphone.COMP_ID;
            //ViewData["Phoneitem"] = COMPhoneRepo.GetAll();
            COMPhoneRepo.Delete(id );

            return RedirectToAction("Details", new {id= compID });
        }




    }
}
