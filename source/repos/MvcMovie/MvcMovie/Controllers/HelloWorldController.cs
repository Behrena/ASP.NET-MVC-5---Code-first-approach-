using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
      
        public string Welcome(string name,int numTimess = 1)
        {
            // HttpUtility.HtmlEncode to protect the application from malicious input(namely JavaScript)
            return HttpUtility.HtmlEncode("Hello" + name + ", NumTime is: " + numTimess);
                //"This is the Welcome action method...";
        }
    }
}