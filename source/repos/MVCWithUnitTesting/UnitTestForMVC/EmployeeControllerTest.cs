using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCWithUnitTesting.Controllers;
using MVCWithUnitTesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UnitTestForMVC.Models;

namespace UnitTestForMVC
{
    [TestClass]
    public class EmployeeControllerTest
    {
        [TestMethod]
        public void IndexView()
        {
            var empController = GetEmployeeController(new InMemoryRepository());
        }


        /// <summary>
        /// Gets the employee controller.
        /// </summary>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <returns></returns>
        private static EmployeeController GetEmployeeController(IRepository employeeRepository)
        {
            EmployeeController employeeController = new EmployeeController(employeeRepository);
            employeeController.ControllerContext = new ControllerContext()
            {
                Controller = employeeController,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };

            return employeeController;
        }

        [TestMethod]
        public void GetAllEmployeeFromRepository()
        {
            // Arrange
            Employee employee1 = GetEmployeeName(1, "Beniwal", "Raj", "Mr", "H33", "Noida", "U.P", "201301");
            Employee employee2 = GetEmployeeName(2, "Beniwal", "Pari", "Ms", "d77", "Noida", "U.P", "201301");
            InMemoryRepository emprepository = new InMemoryRepository();
            emprepository.Add(employee1);
            emprepository.Add(employee2);
            var controller = GetEmployeeController(emprepository);
            var result = controller.Index() as ViewResult;
            var datamodel = (IEnumerable<Employee>)result.ViewData.Model;
            CollectionAssert.Contains(datamodel.ToList(), employee1);
            CollectionAssert.Contains(datamodel.ToList(), employee2);
        }

        /// <summary>
        /// Creates the post employee in repository.
        /// </summary>
        [TestMethod]
        public void Create_PostEmployeeInRepository()
        {
            InMemoryRepository empRepository = new InMemoryRepository();
            EmployeeController empController = GetEmployeeController(empRepository);
            Employee employee = GetEmployeeID();

            empController.Create(employee);

            IEnumerable<Employee> employees = empRepository.GetAllEmployee();
            Assert.IsTrue(employees.Contains(employee));

        }

        /// <summary>
        /// Creates the post redirect on success.
        /// </summary>
        public void Create_PostRedirectOnSuccess()
        {
            EmployeeController controller = GetEmployeeController(new InMemoryRepository());
            Employee model = GetEmployeeID();
            var result = (RedirectToRouteResult)controller.Create(model);
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }

        [TestMethod]
        public void ViewIsNotValid()
        {
            EmployeeController empcontroller = GetEmployeeController(new InMemoryRepository());
            empcontroller.ModelState.AddModelError("", "mock error message");
            Employee model = GetEmployeeName(1, "", "", "", "", "", "", "");
            var result = (ViewResult)empcontroller.Create(model);
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void RepositoryThrowsException()
        {
            // Arrange
            InMemoryRepository emprepository = new InMemoryRepository();
            Exception exception = new Exception();
            emprepository.ExceptionToThrow = exception;

            EmployeeController controller = GetEmployeeController(emprepository);
            Employee employee = GetEmployeeID();

            var result = (ViewResult)controller.Create(employee);

            Assert.AreEqual("Create", result.ViewName);
            ModelState modelState = result.ViewData.ModelState[""];
            Assert.IsNotNull(modelState);
            Assert.IsTrue(modelState.Errors.Any());
            Assert.AreEqual(exception, modelState.Errors[0].Exception);
        }


        /// <summary>
        /// Gets the employee identifier.
        /// </summary>
        /// <returns></returns>
        Employee GetEmployeeID()
        {
            return GetEmployeeName(1, "Beniwal", "Raj", "Mr", "H33", "Noida", "U.P", "201301");
        }
        Employee GetEmployeeName(int id, string lName, string fName, string title, string address, string city, string region, string postalCode)
        {
            return new Employee
            {
                EmployeeID = id,
                LastName = lName,
                FirstName = fName,
                Title = title,
                Address = address,
                City = city,
                Region = region,
                PostalCode = postalCode

            };
        }

    }

    

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.HttpContextBase" />
    class MockHttpContext : HttpContextBase
    {
        private readonly IPrincipal _user = new GenericPrincipal(new GenericIdentity("someUser"), null/*roles*/);


        public override IPrincipal User
        {
            get { return _user; }
            set => base.User = GetValue(value);
        }

        private static IPrincipal GetValue(IPrincipal value)
        {
            return value;
        }
    }

}
