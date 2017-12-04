using System.Collections.Generic;
using System.Linq;

namespace MVCWithUnitTesting.Models
{
    public class EmployeeRepository : IRepository
    {
        private northwindEntities _db = new northwindEntities();

        public void DeleteEmployee(int id)
        {
            var empInfo = _db.Employees.Find(id);//GetEmployeeById(id);
            
            _db.Employees.Remove(empInfo);
            _db.SaveChanges();
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _db.Employees.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _db.Employees.FirstOrDefault(e=>e.EmployeeID == id);
            
        }

        public void CreateNewEmployee(Employee employee)
        {
            _db.Employees.Add(employee);
            _db.SaveChanges();
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void SaveChanges(Employee employee)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Employee employee)
        {
            throw new System.NotImplementedException();
        }
    }
}