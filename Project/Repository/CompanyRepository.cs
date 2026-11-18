//using Project.Migrations;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        ElQasrDbContext context;
        public CompanyRepository(ElQasrDbContext context)
        {
            this.context = context;
        }
       
        public int Delete(int id)
        {

            Company company = GetById(id);
            if (company == null) { return -1; }
            try
            {
                context.Company.Remove(company);
                context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;

            }
        
        }

        public List<Company> FindName(string name)
        {

           List<Company> fiCom = context.Company.Where(c=>c.Name==name).ToList();
            return fiCom;
        }

        public List<Company> GetAll()
        {
            List<Company> companys = context.Company.Include(x => x.company_Phones).ToList();
            return companys;
        }

        public Company GetById(int id)
        {

            Company Companyid = context.Company.FirstOrDefault(x => x.ID == id);
            return Companyid;
        }

        public void Insert(Company NewCompany)
        {
            context.Company.Add(NewCompany);
            context.SaveChanges();
        }

        public void Update(int id, Company NewCompany)
        {
            Company Oldcompany = GetById(id);
            Oldcompany.Name = NewCompany.Name;  
            Oldcompany.PersonContact= NewCompany.PersonContact;
            context.SaveChanges();


        }
    }
}
