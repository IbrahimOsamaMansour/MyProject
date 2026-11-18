using Project.Models;

namespace Project.Repository
{
    public interface ICompanyPhoneRepository
    {
        List<Company_Phone> GetAll();
        Company_Phone GetByIdPH(int id );

        //============================== 11 / 24 
        List<Company_Phone> getAllCompanyID(int id);
        void Insert(Company_Phone NewCompany);
        void Update(int id , Company_Phone company_Phone);
        void Delete(int id    );
        Company_Phone Validat(Company_Phone company_Phone);
    }
}




