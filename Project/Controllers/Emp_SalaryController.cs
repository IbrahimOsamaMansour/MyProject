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
using Project.ViewModel;

namespace Project.Controllers
{
    [Authorize]
    public class Emp_SalaryController : Controller
    {
        IEmpSalaryRepo empSalaryRepo;
        IWorkersRepo workersRepo;
        IEmpAttendanceRepo empAttendanceRepo;
       
        public Emp_SalaryController(IEmpSalaryRepo empSalaryRepo, IWorkersRepo workersRepo , IEmpAttendanceRepo empAttendanceRepo)
        {
            this.empSalaryRepo = empSalaryRepo;
            this.workersRepo = workersRepo;
            this.empAttendanceRepo = empAttendanceRepo;
        }





        // GET
        public IActionResult Index()
        {
           List<Emp_Salary> emp_Salary = empSalaryRepo.GetAll();
            return View(emp_Salary);
        }

        // GET
        public IActionResult Details(int id)
        {


            return View(empSalaryRepo.GetById(id));
        }



        // GET
        [HttpGet]

        public IActionResult Create()
        {
        
            WorkerSalaryViewModel workerSalaryView = new WorkerSalaryViewModel()
            {
                workerlist = workersRepo.GetAll()
                //emp_Attendances= empAttendanceRepo.GetAll(),    
            };
            return View(workerSalaryView);

        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WorkerSalaryViewModel workerSalaryViewModel)
        {
            if (ModelState.IsValid)
            {
                Emp_Salary emp_Salary = new Emp_Salary()
                {
                    TotalSalary= workerSalaryViewModel.TotalSalary,
                    Month = workerSalaryViewModel.Month,
                    AbsentDays= workerSalaryViewModel.AbsentDays,   
                    Year= workerSalaryViewModel.Year,   
                    WorkerID = workerSalaryViewModel.WorkerID   
                  
                };
                empSalaryRepo.Insert(emp_Salary);
                return RedirectToAction(nameof(Index));
            }
            WorkerSalaryViewModel workerSalaryView = new WorkerSalaryViewModel()
            {
                workerlist = workersRepo.GetAll()
                  
            };
            return View(workerSalaryView);
        }

        //// GET
        public IActionResult Edit(int id)
        {
            Emp_Salary emp_Salary = empSalaryRepo.GetById(id);

            WorkerSalaryViewModel workerAttende = new WorkerSalaryViewModel()
            {
                workerlist = workersRepo.GetAll(),
                ID = emp_Salary.WorkerID    ,
                WorkerID = emp_Salary.WorkerID ,    
                Year= emp_Salary.Year , 
                AbsentDays = emp_Salary.WorkerID ,
                Month = emp_Salary.WorkerID ,
                TotalSalary =emp_Salary.TotalSalary,

                
               
            };

            return View(workerAttende);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, WorkerSalaryViewModel workerSalaryView)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    empSalaryRepo.Update(id, workerSalaryView);
                    return RedirectToAction("Index", new { id = id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return View(workerSalaryView);
                }

            }
            ViewData["workerID"] = new SelectList(workersRepo.GetAll(), "ID", "ID", workerSalaryView.WorkerID);
            return View(workerSalaryView);
        }

        //// GET
        public IActionResult Delete(int id)
        {


            Emp_Salary emp_Attendance = empSalaryRepo.GetById(id);

            return View(emp_Attendance);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Emp_Salary emp_Salary = empSalaryRepo.GetById(id);
            int resultAttend = empAttendanceRepo.SearchEmpAttindById(emp_Salary.WorkerID, emp_Salary.Year, emp_Salary.Month);
            if(resultAttend != 0)
            {
                TempData["DeleteAttend"] = $"Please, Delete all attendance for this workers in Year {emp_Salary.Year} in month {emp_Salary.Month} ";
                return RedirectToAction("Delete");
            }
            int result = empSalaryRepo.Delete(id);
            if (result == 1)
            {
                TempData["DoneSlary"] = "Delete Attendance is Done Succesfully";
                return RedirectToAction("Index");
            }
            else { ViewBag.delfail = "SalaryeFail"; }
            return RedirectToAction("Delete", new { id = id });



        }









    }
}
