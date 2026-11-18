using Project.Models;
using Project.ViewModel;

namespace Project.Repository
{
    public interface IEmpAttendanceRepo
    {
        List<Emp_Attendance> GetAll();
        Emp_Attendance GetById(int id);

        List<Emp_Attendance> GetByWorkerId(int id);

        List<Emp_Attendance> ShowAllAttendeOFWorker(int id);

        int Insert(Emp_Attendance emp_Attendance);
        void Update(int id, WorkerAttendeViewModel emp_Attendance);
        int Delete(int id);

        int SearchEmpAttindById(int id, int year, int month);


    }
}
