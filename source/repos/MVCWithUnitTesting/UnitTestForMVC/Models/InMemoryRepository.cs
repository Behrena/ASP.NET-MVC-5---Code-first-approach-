using MVCWithUnitTesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestForMVC.Models
{
    public class InMemoryRepository : IRepository
    {
        private List<Employee> _db = new List<Employee>();

        public Exception ExceptionToThrow { get; set; }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _db.ToList();
        }
        public Employee GetEmployeeById(int id)
        {
            return _db.FirstOrDefault(e => e.EmployeeID == id);
        }

        public void CreateNewEmployee(Employee employee)
        {
            if (ExceptionToThrow != null) throw ExceptionToThrow;

            _db.Add(employee);
        }

        public void DeleteEmployee(int id)
        {
            _db.Remove(GetEmployeeById(id));
        }
        public void SaveChanges(Employee employeeToUpdate)
        {
            foreach(Employee employee in _db)
            {
                if(employee.EmployeeID == employeeToUpdate.EmployeeID)
                {
                    _db.Remove(employee);
                    _db.Add(employeeToUpdate);
                    break;
                }
            }
        }

        public void Add(Employee employeeToAdd)
        {
            _db.Add(employeeToAdd);
        }

        public int SaveChanges()
        {
            return 1;
        }
        
    }
}
