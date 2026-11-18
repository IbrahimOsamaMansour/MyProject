using Project.Models;

namespace Project.Repository
{
    public class CompanyPhonoRepository :ICompanyPhoneRepository
    {
        ElQasrDbContext context;
        public CompanyPhonoRepository(ElQasrDbContext context)
        {
            this.context = context;
        }
        //============================== 11 / 24 
        public List<Company_Phone> getAllCompanyID(int id)
        {
            List<Company_Phone> company_Phone = context.Company_Phone.Where(x => x.COMP_ID == id).ToList();
            return company_Phone;
        }
        public void Delete(int id )
        {
            Company_Phone company_Phone = GetByIdPH(id );
            context.Company_Phone.Remove(company_Phone);
            context.SaveChanges();
        }

        public List<Company_Phone> GetAll()
        {
            List<Company_Phone> companys = context.Company_Phone.ToList();
            return companys;
        }

        public Company_Phone GetByIdPH(int id )
        {

            Company_Phone Companyid = context.Company_Phone.FirstOrDefault(x => x.id == id);
            return Companyid;
        }

        public void Insert(Company_Phone company_Phone)
        {
            context.Company_Phone.Add(company_Phone);
            context.SaveChanges();
        }

        public void Update(int id, Company_Phone company_Phone)
        {
            Company_Phone Oldcompany = GetByIdPH(id);
            Oldcompany.Phone = company_Phone.Phone;
            context.SaveChanges();


        }



        public Company_Phone Validat( Company_Phone company_Phone)

        {
            //Company_Phone company_Phone1 = new Company_Phone("w");          
            Company_Phone valid = context.Company_Phone.FirstOrDefault(s => s.Phone == company_Phone.Phone);
            return valid;   
        }



    }
}
