using System.Web.Mvc;

namespace GreenPro.AdminInterface.Controllers
{
    [Authorize(Roles = "Crew Leader,Admin")]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            bool result = User.IsInRole("Crew Leader");
            if (result)
            {
                return RedirectToAction("Index", "CrewLeader");
            }

            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ServiceManagement()
        {

            return View();

        }
        public ActionResult CrewUsers()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}