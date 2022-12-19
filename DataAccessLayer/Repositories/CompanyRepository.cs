using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Changed All Functions to Async Functions
    /// </summary>
    public class CompanyRepository : ICompanyRepository
    {
	    private readonly IDbWrapper<Company> _companyDbWrapper;

	    public CompanyRepository(IDbWrapper<Company> companyDbWrapper)
	    {
		    _companyDbWrapper = companyDbWrapper;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _companyDbWrapper.FindAllAsync();
        }

        public async Task<Company> GetByCode(string companyCode)
        {
            var res = await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode));
            return res?.FirstOrDefault();
        }

        public async Task<bool> SaveCompany(Company company)
        {
            var res = _companyDbWrapper.Find(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode));
            var itemRepo = res?.FirstOrDefault();
            if (itemRepo !=null)
            {
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.Country = company.Country;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.LastModified = company.LastModified;
                return await _companyDbWrapper.UpdateAsync(itemRepo);
            }

            return await _companyDbWrapper.InsertAsync(company);
        }

        /// <summary>
        /// Adding DeleteCompany async Function
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCompany(string companyCode)
        {
            return await _companyDbWrapper.DeleteAsync(t => t.CompanyCode.Equals((companyCode)));
        }
    }
}
