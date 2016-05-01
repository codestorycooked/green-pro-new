using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenPro.WebClient.Controllers
{
    public class CommonController : Controller
    {

       
        GreenPro.Data.GreenProDbEntities _db;
        public CommonController()
        {
            
            _db = new Data.GreenProDbEntities();
        }

        // GET: Common
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserName(string userid)
        {
            var userinfo = _db.AspNetUsers.Where(u => u.Id == userid).SingleOrDefault();
            ViewBag.CustomerFullName = userinfo.FirstName + " " + userinfo.LastName;
            return View();
        }
    }
}