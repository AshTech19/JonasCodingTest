using System;
using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyInfo>> GetAllCompanies()
        {
            var res = await _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public async Task<CompanyInfo> GetCompanyByCode(string companyCode)
        {
            var result = await _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newCompany"></param>
        /// <returns></returns>
        public async Task<bool> InsertCompany(CompanyInfo newCompany)
        {
            // I wrote "SaveCompany" because it was given
            return await _companyRepository.SaveCompany(_mapper.Map<Company>(newCompany));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newCompany"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCompany(int id, CompanyInfo newCompany)
        {
            // check if company exists
            var existingCompany = await _companyRepository.GetByCode(id.ToString());
            if (existingCompany == null)
            {
                // company does not exist, so can not be updatable
                throw new Exception("The company to be updated could not be found");
            }

            newCompany.SiteId = existingCompany.SiteId;
            newCompany.CompanyCode = id.ToString();

            // used SaveCompany because it checks if company exists, inside of it. 
            return await _companyRepository.SaveCompany(_mapper.Map<Company>(newCompany));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCompany(int id)
        {
            // check if the company to be deleted exists
            var existingCompany = await _companyRepository.GetByCode(id.ToString());
            if (existingCompany == null)
            {
                // company does not exist, so can not be updatable
                throw new Exception("The company to be deleted could not be found");
            }

            return await _companyRepository.DeleteCompany(id.ToString());
        }
    }
}