using GreenPro.Data;
using GreenPro.WebClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace GreenPro.WebClient.Controllers
{
    public class PackagesController : Controller
    {
        GreenPro.Data.GreenProDbEntities _db;
        public PackagesController()
        {
            _db = new Data.GreenProDbEntities();

        }


        //// GET: Packages
        // Comment by circus
        //public ActionResult Index(int id)
        //{
        //    Session["NewServiceGarageId"] = id;
        //    var package = new PackagesServiceViewModel();
        //    package.CarTypes = _db.CarTypes.ToList();
        //    if (Session["SelectedCar"] != null)
        //    {
        //        var car = Session["SelectedCar"] as CarUser;
        //        var l = _db.CarTypes.Where(a => a.Id == car.Type).ToList();
        //        package.CarTypes = l;
        //    }            
        //    package.Packages = null;
        //    return View(package);
        //}


        // GET: Packages
        // Added By circus
        public ActionResult Index(int id)
        {
            // Store Customer Seleted Garage in session
            Session["NewServiceGarageId"] = id;

            var model = new PackagesServiceViewModel();
            var packages = _db.Packages.Include(p => p.Package_Services);
            model.Packages = packages;
            return View(model);
        }



        [HttpPost]
        public ActionResult IndexPost(int id)
        {
            Session["NewServiceCarTypeId"] = id;
            var ids = Convert.ToInt16(id);
            //var packages = _db.Packages.Include(p => p.Package_Services).Where(a => a.CarTypeId == ids);
            var packages = _db.Packages.Include(p => p.Package_Services);
            var packageViewModel = new PackagesServiceViewModel();
            packageViewModel.CarTypes = _db.CarTypes.ToList();
            packageViewModel.Packages = packages;
            var package_services = new Package_Services();           

            return View("_Packages", packageViewModel);
        }


    }
}