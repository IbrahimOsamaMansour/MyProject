using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Repository;

namespace Project.Controllers
{
    [Authorize]
    public class WorkersController : Controller
    {
      IWorkersRepo workersRepo;
        IEmpAttendanceRepo empAttendanceRepo;
        IEmpSalaryRepo empSalaryRepo;
        public WorkersController(IWorkersRepo workersRepo , IEmpAttendanceRepo empAttendanceRepo , IEmpSalaryRepo empSalaryRepo)
        {
            this.workersRepo = workersRepo;
            this.empAttendanceRepo = empAttendanceRepo; 
            this.empSalaryRepo = empSalaryRepo;
        }

    
        // GET: Workers
        public IActionResult Index()
        {
              return View(workersRepo.GetAll());
        }

        // GET: Workers/Details/5
        public  IActionResult Details(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Workers workers = workersRepo.GetById(id);
                return View(workers);
            }
        
        }

        // GET: Workers/Create
        public IActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Workers workers)
        {
            if (ModelState.IsValid)
            {
                workersRepo.Insert(workers);
                return RedirectToAction("Index");
            }
            return View(workers);
        }

        // GET
        public IActionResult Edit(int id)
        {
          
            return View(workersRepo.GetById(id));
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Name,SSN,Phone,NetSalary")] Workers workers)
        {
           

            //if (ModelState.IsValid)
            //{
                try
                {
                    workersRepo.Update(id, workers);
                    return RedirectToAction("Details" , new { id = id});   
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction("Edit", new { id = id });
                }
               
            //}
            //return View(workers);
        }

        // GET
        public IActionResult Delete(int id)
        {
      
            return View(workersRepo.GetById(id));
        }

        // POST 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Emp_Attendance
           int EmpAttendance =empAttendanceRepo.GetByWorkerId(id).Count();
            if (EmpAttendance != 0)
            {
                TempData["deleteEmpAttendance"] = "Please Delete All Employee Attendance  ";
                return RedirectToAction("Delete", new { id = id });
            }
            //  Emp_Salary
          int EmpSalary = empSalaryRepo.GetByWorkerId(id).Count();
            if (EmpSalary != 0)
            {
                TempData["deleteEmpSalary"] = "Please Delete All Employee Salary  ";
                return RedirectToAction("Delete", new { id = id });
            }
            int result = workersRepo.Delete(id);
            if (result == 1)
            {
                TempData["Done"] = "Delete Workers is Done Succesfully";
                return RedirectToAction("Index");
            }
            else { TempData["WorkerFail"] = "Delete Workers is Failed";  }
            return RedirectToAction("Delete", new { id = id });

          
        }

        
    }
}
