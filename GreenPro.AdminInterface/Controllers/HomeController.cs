using GreenPro.Data;
using System.Web.Mvc;
using System.Linq;
using System;

namespace GreenPro.AdminInterface.Controllers
{
    [Authorize(Roles = "Crew Leader,Admin")]
    public class HomeController : BaseController
    {
         private GreenProDbEntities db;

         public HomeController()
        {
            db = new GreenProDbEntities();
           
        }

        public ActionResult Index()
        {
             DateTime serviceDate = DateTime.Now.Date.AddDays(1);
             var currentDay = DateTime.Now.Date.AddDays(1).DayOfWeek.ToString();
             DateTime LastServiceDate=serviceDate.AddDays(-7);

            var userPackageList = db.UserPackages.Where(u => u.PaymentRecieved == true && u.IsActive == true
                && u.ServiceDay == currentDay && u.NextServiceDate != serviceDate 
                && (u.NextServiceDate == LastServiceDate || u.NextServiceDate.HasValue==false)
                && (serviceDate < u.NextServiceDate ||  u.NextServiceDate.HasValue==false)).ToList();

            foreach (var userpackageitem in userPackageList)
            {
                userpackageitem.NextServiceDate = serviceDate;
                userpackageitem.LastServiceDate = LastServiceDate;
                db.SaveChanges();
            }

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