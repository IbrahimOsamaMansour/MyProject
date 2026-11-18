using Project.Models;

namespace Project.Repository
{
    public interface ICompanyRepository
    {
        List<Company> GetAll();
        List<Company> FindName(string name);
        Company GetById(int id);

        void Insert(Company NewCompany);   
        void Update( int id ,Company NewCompany);    
        int Delete(int id );   
    }
}
