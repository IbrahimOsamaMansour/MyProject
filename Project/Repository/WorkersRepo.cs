using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repository
{
    public class WorkersRepo : IWorkersRepo
    {

        ElQasrDbContext context;
        public WorkersRepo(ElQasrDbContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            //throw new NotImplementedException();
            Workers workers = GetById(id);
        
            if (workers == null) { return -1; }
            try
            {
                context.workers.Remove(workers);
                context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;

            }
        }

        public List<Workers> GetAll()
        {
            List<Workers> Workers = context.workers.ToList();
            return Workers;
        }

        public Workers GetById(int id)
        {
            Workers workers = context.workers.FirstOrDefault(x => x.ID == id);
            return workers;
        }

        public void Insert(Workers Newworkers)
        {
            context.workers.Add(Newworkers);
            context.SaveChanges();
        }

        public void Update(int id, Workers workers)
        {

            Workers OldWorker = GetById(id);
            OldWorker.Name = workers.Name;
            OldWorker.SSN= workers.SSN;
            OldWorker.Phone= workers.Phone;
            OldWorker.NetSalary= workers.NetSalary;
            OldWorker.EmpAttendances= workers.EmpAttendances;
            OldWorker.EmpSalaries= workers.EmpSalaries;
            context.SaveChanges();

        }
    }
}
