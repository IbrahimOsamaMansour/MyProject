using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.ViewModel;

namespace Project.Repository
{
    public class EmpSalaryRepo : IEmpSalaryRepo
    {

        ElQasrDbContext context;
        public EmpSalaryRepo(ElQasrDbContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            Emp_Salary emp_Salary = GetById(id);

            if (emp_Salary == null) { return -1; }
            try
            {
                context.Emp_Salarie.Remove(emp_Salary);
                context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;

            }
        }

        public List<Emp_Salary> GetAll()
        {
            List<Emp_Salary> Emp_Salary = context.Emp_Salarie.Include(x=> x.Worker ).ToList();
            return Emp_Salary;
        }

        public Emp_Salary GetById(int id)
        {
            Emp_Salary Emp_Salary = context.Emp_Salarie.Include(e => e.Worker).FirstOrDefault(x => x.ID == id);
            return Emp_Salary;
        }

        //=================================  GetByWorkerId

        public List<Emp_Salary> GetByWorkerId(int id)
        {
            List<Emp_Salary> Emp_Salary = context.Emp_Salarie.Include(e => e.Worker).Where(x => x.WorkerID == id).ToList();
            return Emp_Salary;
        }



        public void Insert(Emp_Salary Emp_Salary)
        {
            context.Emp_Salarie.Add(Emp_Salary);
            context.SaveChanges();
        }

        public void Update(int id, WorkerSalaryViewModel Emp_Salary)
        {

            Emp_Salary OldEmp = GetById(id);
            OldEmp.TotalSalary = Emp_Salary.TotalSalary;
            OldEmp.AbsentDays = Emp_Salary.AbsentDays;  
            OldEmp.Month= Emp_Salary.Month;
            OldEmp.Year = Emp_Salary.Year;


            context.SaveChanges();

        }


        public void UpdateEmpSalary(int id, Emp_Salary emp_Salary)
        {
            Emp_Salary OldEmp = GetById(id);
            OldEmp.AbsentDays += emp_Salary.AbsentDays;
            if (emp_Salary.AbsentDays == 0)
            {
                
                OldEmp.TotalSalary += emp_Salary.TotalSalary;
            }else if(emp_Salary.AbsentDays == -1)
            {
                OldEmp.TotalSalary += emp_Salary.TotalSalary;
            }
            else if (emp_Salary.AbsentDays == 1)
            {
                OldEmp.TotalSalary += emp_Salary.TotalSalary;
            }

            context.SaveChanges();
        }


        public Emp_Salary GetSearchWorkerSalaryById(int id , int month , int year )
        {
            Emp_Salary Emp_Salary = context.Emp_Salarie.FirstOrDefault(x => x.WorkerID == id && x.Year == year && x.Month == month);
            //int emp_Attendance = context.Emp_Attendance.Where(x => x.workerID == id && x.Date.Year ==year && x.Date.Month ==month ).Count();
            return Emp_Salary;
        }



      public Emp_Salary GetSearchWorkerSalaryByIdTOUseDelete(int id, int month, int year)
        {
            Emp_Salary searchsalarytodelete = context.Emp_Salarie.FirstOrDefault(x => x.WorkerID == id && x.Year == year && x.Month == month);
            return searchsalarytodelete;
        }


    }
}
