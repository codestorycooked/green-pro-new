using System.Web.Mvc;

namespace GreenPro.AdminInterface.Controllers
{
    public class RenewalController : BaseController
    {
        // GET: Renewal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpComingRenewals()
        {

            return View();
        }

        public ActionResult AutoRenewPackages() {

            return View();
        }

        
    }
}