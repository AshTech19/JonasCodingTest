using System.Collections.Generic;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeInfo> GetAllEmployees();
        EmployeeInfo GetEmployeeByCode(string employeeCode);
        bool InsertEmployee(EmployeeInfo employeeInfo);
        bool UpdateEmployee(int id, EmployeeInfo employeeInfo);
    }
}