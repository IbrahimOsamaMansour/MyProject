using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.ViewModel;

namespace Project.Repository
{
    public class EmpAttendanceRepo : IEmpAttendanceRepo
    {

        ElQasrDbContext context;
        public EmpAttendanceRepo(ElQasrDbContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            Emp_Attendance Emp_Attendance = GetById(id);

            if (Emp_Attendance == null) { return -1; }
            try
            {
                context.Emp_Attendance.Remove(Emp_Attendance);
                context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;

            }
        }

        public List<Emp_Attendance> GetAll()
        {
            List<Emp_Attendance> Emp_Attendance = context.Emp_Attendance.Include(e => e.Workers).OrderByDescending(x => x.ID).ToList();
            return Emp_Attendance;
        }

        public Emp_Attendance GetById(int id)
        {
            Emp_Attendance emp_Attendance = context.Emp_Attendance.Include(e => e.Workers).FirstOrDefault(x => x.ID == id);
            return emp_Attendance;
        }

        //=================================  GetByWorkerId
        public List< Emp_Attendance> GetByWorkerId(int id)
        {
            List<Emp_Attendance> emp_Attendance = context.Emp_Attendance.Where(x => x.workerID == id).ToList();
            return emp_Attendance;
        }



        //=================================          ShowAllAttendeOFWorker

        public List<Emp_Attendance>   ShowAllAttendeOFWorker(int id)
        {
            List<Emp_Attendance> emp_Attendance = context.Emp_Attendance.Include(s=>s.Workers).Where(x => x.workerID == id).ToList();
            return emp_Attendance;
        }








        public int Insert(Emp_Attendance emp_Attendance)
        {
            try
            {
                context.Emp_Attendance.Add(emp_Attendance);
                context.SaveChanges();
                return 1;
            }catch
            { 
                return 0;
            }
            
        }

        public void Update(int id, WorkerAttendeViewModel emp_Attendance)
        {

            Emp_Attendance OldEmp = GetById(id);
            OldEmp.OverTime = emp_Attendance.OverTime;  
            OldEmp.Date = emp_Attendance.Date;
            OldEmp.Attendance = emp_Attendance.Attendance;


            context.SaveChanges();

        }

        public int SearchEmpAttindById(int id, int month, int year)
        {
           int emp_Attendance = context.Emp_Attendance.Where(x => x.workerID == id  && x.Date.Month == month && x.Date.Year == year).Count();
            return emp_Attendance;
        }




    }
}
