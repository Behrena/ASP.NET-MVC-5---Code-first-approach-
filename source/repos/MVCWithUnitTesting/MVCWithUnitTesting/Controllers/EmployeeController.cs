using MVCWithUnitTesting.Models;
using System;
using System.Web.Mvc;

namespace MVCWithUnitTesting.Controllers
{
    public class EmployeeController : Controller
    {
        IRepository _repository;
        public EmployeeController() : this(new EmployeeRepository()) { }


        public EmployeeController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            ViewData["ControllerName"] = this.ToString();
            return View("Index", _repository.GetAllEmployee());
        }

        public ActionResult Details(int id = 0)
        {
            Employee employee = _repository.GetEmployeeById(id);

            return View("Details", employee);
        }

        //Get
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.CreateNewEmployee(employee);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                ViewData["CreateError"] = "Unable to create; view innerexception";
            }

            return View("Create");
        }

        public ActionResult Edit(int id = 0)
        {
            var employee = _repository.GetEmployeeById(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var employee = _repository.GetEmployeeById(id);

            try
            {
                if (TryUpdateModel(employee))
                {
                    _repository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ViewData["EditError"] = ex.InnerException.ToString();
                }
                else { ViewData["EditError"] = ex.ToString(); }
            }
#if DEBUG  
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    if (error.Exception != null)
                    {
                        throw modelState.Errors[0].Exception;
                    }
                }
            }
#endif
            return View(employee);
        }

        public ActionResult Delete(int id = 0)
        {
            var employee = _repository.GetEmployeeById(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(int id,FormCollection collection)
        {
            try
            {
                _repository.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
          
        }
    }
}