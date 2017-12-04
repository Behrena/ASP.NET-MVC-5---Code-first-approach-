using System.Collections.Generic;

namespace MVCWithUnitTesting.Models
{
    public interface IRepository
    {
        void CreateNewEmployee(Employee employee);
        IEnumerable<Employee> GetAllEmployee();
        void DeleteEmployee(int id);
        Employee GetEmployeeById(int id);
        int SaveChanges();
        void SaveChanges(Employee employee);
        void Add(Employee employee);
    }
}
