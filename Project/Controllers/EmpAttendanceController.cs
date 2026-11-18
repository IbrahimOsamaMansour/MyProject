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
    //[Authorize]
    public class EmpAttendanceController : Controller
    {

        IEmpAttendanceRepo empAttendanceRepo;
        IWorkersRepo workersRepo;
        IEmpSalaryRepo empSalaryRepo;
        public EmpAttendanceController(IEmpAttendanceRepo empAttendanceRepo, IWorkersRepo workersRepo , IEmpSalaryRepo empSalaryRepo)
        {
            this.empAttendanceRepo= empAttendanceRepo; 
            this.workersRepo= workersRepo;  
            this.empSalaryRepo= empSalaryRepo;
        }

        // GET
        public IActionResult Index()
        {
            return View(empAttendanceRepo.GetAll());
        }

        // GET
        public IActionResult Details(int id)
        {
       

            return View(empAttendanceRepo.GetById(id));
        }

        // GET
        [HttpGet]

        public IActionResult Create()
        {
            WorkerAttendeViewModel workerAttende = new WorkerAttendeViewModel()
            {
                workerlist= workersRepo.GetAll()
                //emp_Attendances= empAttendanceRepo.GetAll(),    
            };    
            return View(workerAttende);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WorkerAttendeViewModel workerAttendeViewModel)
        {
            if (ModelState.IsValid)
            {
                Emp_Attendance emp_Attendance = new Emp_Attendance()
                {
                    OverTime = workerAttendeViewModel.OverTime,
                    Date = workerAttendeViewModel.Date,
                    Attendance = workerAttendeViewModel.Attendance,
                    workerID=workerAttendeViewModel.workerID,
                };
                empAttendanceRepo.Insert(emp_Attendance);
                //return RedirectToAction(nameof(Index));

                //=================================================== 11 /30 /////////////


                Workers workers = workersRepo.GetById(workerAttendeViewModel.workerID);
                double selDay = (double)workers.NetSalary / 30.0;
                int month = workerAttendeViewModel.Date.Month;
                int year = workerAttendeViewModel.Date.Year;

                Emp_Salary emp_Salary = new Emp_Salary()
                {
                    WorkerID = workerAttendeViewModel.workerID,
                    Month = month,
                    Year = year,
                    //TotalSalary = selDay,
                };

                if (workerAttendeViewModel.Attendance == true)
                {
                    emp_Salary.AbsentDays = 0;
                    emp_Salary.TotalSalary = selDay;
                }
                else
                {
                    emp_Salary.AbsentDays = 1;
                    emp_Salary.TotalSalary = 0;
                }

                Emp_Salary findEmp = empSalaryRepo.GetSearchWorkerSalaryById(workerAttendeViewModel.workerID, month, year);

                if (findEmp != null)
                {
                    emp_Salary.ID = findEmp.ID;
                    empSalaryRepo.UpdateEmpSalary(emp_Salary.ID, emp_Salary);
                }
                else
                {
                    empSalaryRepo.Insert(emp_Salary);
                }


                return RedirectToAction("Index");




                //////////////////////////////////////////




            }
            WorkerAttendeViewModel workerAttende = new WorkerAttendeViewModel()
            {
                workerlist = workersRepo.GetAll()
                //emp_Attendances= empAttendanceRepo.GetAll(),    
            };
            return View(workerAttende);
        }

            // GET
            public IActionResult Edit(int id)
        {
            Emp_Attendance emp_Attendance = empAttendanceRepo.GetById(id);

            WorkerAttendeViewModel workerAttende = new WorkerAttendeViewModel()
            {
                workerlist = workersRepo.GetAll(),
               ID = emp_Attendance.ID,
               OverTime= emp_Attendance.OverTime,   
               Date = emp_Attendance.Date,  
               workerID= emp_Attendance.workerID,
               Attendance= emp_Attendance.Attendance,   
            };
            
            return View(workerAttende);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  WorkerAttendeViewModel workerAttende)
        {
          

            if (ModelState.IsValid)
            {
                try
                {
                    Emp_Attendance OLDemp_Attendance = empAttendanceRepo.GetById(id);

                    //=========================
                   
                    //===========================

                    Workers workers = workersRepo.GetById(workerAttende.workerID);
                    double selDay = (double)workers.NetSalary / 30.0;
                    int month = workerAttende.Date.Month;
                    int year = workerAttende.Date.Year;
                    if (OLDemp_Attendance.Attendance == workerAttende.Attendance)
                    {
                        Emp_Salary emp_Salary = new Emp_Salary()
                        {
                            WorkerID = workerAttende.workerID,
                            Month = month,
                            Year = year,
                            TotalSalary = 0,
                            AbsentDays = 0,
                    };
                        //emp_Salary.AbsentDays = 0;
                        //if (workerAttende.Attendance == true)
                        //{
                        //    emp_Salary.AbsentDays = 0;
                        //}
                        //else { emp_Salary.AbsentDays = 0; }


                        Emp_Salary findEmp = empSalaryRepo.GetSearchWorkerSalaryById(workerAttende.workerID, month, year);

                        if (findEmp != null)
                        {
                            emp_Salary.ID = findEmp.ID;
                            empSalaryRepo.UpdateEmpSalary(emp_Salary.ID, emp_Salary);
                        }
                        else
                        {
                            empSalaryRepo.Insert(emp_Salary);
                        }
                    }
                    else
                    {
                        if (workerAttende.Attendance == true && OLDemp_Attendance.Attendance == false)
                        {
                            Emp_Salary emp_Salary = new Emp_Salary()
                            {
                                WorkerID = workerAttende.workerID,
                                Month = month,
                                Year = year,
                                TotalSalary = selDay,
                                AbsentDays = -1,
                            };

                            //if (workerAttende.Attendance == true)
                            //{
                            //    emp_Salary.AbsentDays = -1;
                            //    emp_Salary.TotalSalary = selDay;
                            //}
                            //else {
                            //    emp_Salary.AbsentDays = 0;
                            //    emp_Salary.TotalSalary = selDay;
                            //}

                            Emp_Salary findEmp = empSalaryRepo.GetSearchWorkerSalaryById(workerAttende.workerID, month, year);

                            if (findEmp != null)
                            {
                                emp_Salary.ID = findEmp.ID;
                                empSalaryRepo.UpdateEmpSalary(emp_Salary.ID, emp_Salary);
                            }
                            else
                            {
                                empSalaryRepo.Insert(emp_Salary);
                            }
                        }
                        else
                        {
                            if (workerAttende.Attendance == false && OLDemp_Attendance.Attendance == true)
                            {
                                Emp_Salary emp_Salary = new Emp_Salary()
                                {
                                    WorkerID = workerAttende.workerID,
                                    Month = month,
                                    Year = year,
                                    
                                    TotalSalary = -selDay,
                                    AbsentDays = 1,
                                };

                                //if (workerAttende.Attendance == true)
                                //{
                                //    emp_Salary.AbsentDays = 0;
                                //}
                                //else
                                //{
                                //    emp_Salary.AbsentDays = 1;
                                //    emp_Salary.TotalSalary = -selDay;
                                //}

                                Emp_Salary findEmp = empSalaryRepo.GetSearchWorkerSalaryById(workerAttende.workerID, month, year);

                                if (findEmp != null)
                                {
                                    emp_Salary.ID = findEmp.ID;
                                    empSalaryRepo.UpdateEmpSalary(emp_Salary.ID, emp_Salary);
                                }
                                else
                                {
                                    empSalaryRepo.Insert(emp_Salary);
                                }
                            }
                        }
                    }
                    empAttendanceRepo.Update(id, workerAttende);
                    return RedirectToAction("Details", new { id = id });
                }

                //=========================



                

                catch (DbUpdateConcurrencyException)
                {
                    return View(workerAttende);
                }
               
            }
            ViewData["workerID"] = new SelectList(workersRepo.GetAll(), "ID", "ID", workerAttende.workerID);
            return View(workerAttende);
        }

        // GET
        public IActionResult Delete(int id)
        {


            Emp_Attendance emp_Attendance = empAttendanceRepo.GetById(id);

            return View(emp_Attendance);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Emp_Attendance emp_Attendance = empAttendanceRepo.GetById(id);
            Emp_Salary findSalayyresult1 = empSalaryRepo.GetSearchWorkerSalaryByIdTOUseDelete(emp_Attendance.workerID , emp_Attendance.Date.Month , emp_Attendance.Date.Year);

            if(findSalayyresult1 == null)
            {
                int result = empAttendanceRepo.Delete(id);
                if (result == 1)
                {
                    TempData["DoneAttendance"] = "Delete Attendance is Done Succesfully";
                    return RedirectToAction("Index");
                }
                else { ViewBag.delfail = "AttendanceFail"; }
                return RedirectToAction("Delete", new { id = id });

            }
            else
            {
                Workers workers = workersRepo.GetById(emp_Attendance.workerID);
                double selDay = (double)workers.NetSalary / 30.0;
                int month = emp_Attendance.Date.Month;
                int year = emp_Attendance.Date.Year;

                Emp_Salary emp_Salary = new Emp_Salary()
                {
                    WorkerID = emp_Attendance.workerID,
                    Month = month,
                    Year = year,
                    //TotalSalary = selDay,
                };

                if (emp_Attendance.Attendance == true)
                {
                    emp_Salary.AbsentDays =0 ;
                    emp_Salary.TotalSalary = -selDay;
                }
                else { 
                    emp_Salary.AbsentDays = -1;
                    emp_Salary.TotalSalary = 0;      
                }
                emp_Salary.ID = findSalayyresult1.ID;
                empSalaryRepo.UpdateEmpSalary(emp_Salary.ID, emp_Salary);
                //return RedirectToAction("Index", new { id = id });
                int result = empAttendanceRepo.Delete(id);
                if (result == 1)
                {
                    TempData["DoneAttendance"] = "Delete Attendance is Done Succesfully";
                    return RedirectToAction("Index");
                }
                else { ViewBag.delfail = "AttendanceFail"; }
                return RedirectToAction("Delete", new { id = id });
            }




            //return RedirectToAction("Delete", new { id = id });

        }









        //=============== Create New By Worker id


        [HttpGet]
        public IActionResult CreateNewByWorkerid(int id) 
        {
            WorkerAttendeViewModel workerAttende = new WorkerAttendeViewModel()
            {
                workers = workersRepo.GetById(id),

                workerID = id   
                //emp_Attendances= empAttendanceRepo.GetAll(),    
            };

            return View(workerAttende);

        }
        [HttpPost]
        public IActionResult CreateNewByWorkerid( WorkerAttendeViewModel workerAttende)
        {
            if (ModelState.IsValid)
            {
                Emp_Attendance emp_Attendance = new Emp_Attendance()
                {
                    OverTime = workerAttende.OverTime,
                    Date = workerAttende.Date,
                    Attendance = workerAttende.Attendance,
                    workerID = workerAttende.workerID,
                };
               int result = empAttendanceRepo.Insert(emp_Attendance);
                if(result != 1)
                {
                    TempData["InsertAttende"] = "Insert Attende is not valid";
                    return View(workerAttende);
                }
                Workers workers = workersRepo.GetById(workerAttende.workerID);
                double selDay = (double)workers.NetSalary / 30.0;
                int month = workerAttende.Date.Month;   
                int year = workerAttende.Date.Year;
                
                Emp_Salary emp_Salary = new Emp_Salary()
                {
                    WorkerID= workerAttende.workerID,   
                    Month= month,   
                    Year= year,
                    //TotalSalary = selDay,
                };

                if (workerAttende.Attendance == true)
                {
                    emp_Salary.AbsentDays = 0;
                    emp_Salary.TotalSalary = selDay;
                }
                else {
                    emp_Salary.AbsentDays = 1;
                    emp_Salary.TotalSalary = 0;
                }

                Emp_Salary findEmp = empSalaryRepo.GetSearchWorkerSalaryById(workerAttende.workerID,  month, year);

                if (findEmp != null) 
                { 
                    emp_Salary.ID= findEmp.ID;  
                    empSalaryRepo.UpdateEmpSalary(emp_Salary.ID, emp_Salary);
                }else
                {
                    empSalaryRepo.Insert( emp_Salary);
                }


                return RedirectToAction("Index");
            }
            //return RedirectToAction("CreateNewByWorkerid", new { id = workerAttende.workerID });
            return View(workerAttende);
         }










        //=============== ShowAllAttendeOFWorker


       
        public IActionResult ShowAllAttendeOFWorker(int id)
        {
            List<Emp_Attendance> emp_Attendances = empAttendanceRepo.ShowAllAttendeOFWorker(id);


            return View(emp_Attendances);

        }












    }
}
