using Project.Models;
using Project.ViewModel;

namespace Project.Repository
{
    public interface IEmpSalaryRepo 
    {
        List<Emp_Salary> GetAll();
        Emp_Salary GetById(int id);

        List<Emp_Salary> GetByWorkerId(int id);
        void Insert(Emp_Salary emp_Salary);
        void Update(int id, WorkerSalaryViewModel emp_Salary);

        void UpdateEmpSalary(int id, Emp_Salary emp_Salary);

        int Delete(int id);

        Emp_Salary GetSearchWorkerSalaryById(int id, int month, int year);

        Emp_Salary GetSearchWorkerSalaryByIdTOUseDelete(int id, int month, int year);
    }
}
