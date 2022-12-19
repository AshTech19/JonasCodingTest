using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyInfo>> GetAllCompanies();
        Task<CompanyInfo> GetCompanyByCode(string companyCode);
        Task<bool> InsertCompany(CompanyInfo companyInfo);
        Task<bool> UpdateCompany(int id, CompanyInfo companyInfo);
        Task<bool> DeleteCompany(int id);
    }
}