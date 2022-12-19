using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        public IEnumerable<EmployeeInfo> GetAllEmployees()
        {
            var res = _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public EmployeeInfo GetEmployeeByCode(string employeeCode)
        {
            var result = _employeeRepository.GetByCode(employeeCode);
            return _mapper.Map<EmployeeInfo>(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        public bool InsertEmployee(EmployeeInfo newEmployee)
        {
            // I used same logic with saving company
            return _employeeRepository.SaveEmployee(_mapper.Map<Employee>(newEmployee));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        public bool UpdateEmployee(int id, EmployeeInfo newEmployee)
        {
            // check if employee exists
            var existingEmployee = _employeeRepository.GetByCode(id.ToString());
            if (existingEmployee == null)
            {
                // employee does not exist, so can not be updatable
                throw new Exception("The employee to be updated could not be found");
            }

            newEmployee.SiteId = existingEmployee.SiteId;
            newEmployee.EmployeeCode = id.ToString();

            return _employeeRepository.SaveEmployee(_mapper.Map<Employee>(newEmployee));
        }
    }
}