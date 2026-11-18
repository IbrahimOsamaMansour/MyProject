using Project.Models;

namespace Project.Repository
{
    public interface IWorkersRepo
    {
        List<Workers> GetAll();
        Workers GetById(int id);

      
        void Insert(Workers NewWorkers);
        void Update(int id, Workers Workers);
        int Delete(int id);
      
    }
}
