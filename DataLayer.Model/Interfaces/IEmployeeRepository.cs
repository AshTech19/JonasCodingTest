using DataAccessLayer.Model.Models;
using System.Collections.Generic;


namespace DataAccessLayer.Model.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetByCode(string employeeCode);
        bool SaveEmployee(Employee employee);
        
    }
}
