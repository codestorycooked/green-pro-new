using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreenPro.Data;
using GreenPro.WebClient.ViewModel;
using PagedList;
using Newtonsoft.Json;
using Geocoding;
using Geocoding.Google;
using GreenPro.WebClient.Models;


namespace GreenPro.WebClient.Controllers
{
    public class GaragesController : Controller
    {
        private const int defaultPageSize = 10;
        private GreenProDbEntities db = new GreenProDbEntities();

        // GET: Garages
        public ActionResult GetJsonData(string state, string searchText, int? GarageId)
        {
            int gId = 0;
            gId = GarageId.HasValue ? GarageId.Value : 0;
            var garages = db.Garages.AsQueryable();
            if (!string.IsNullOrEmpty(searchText) || !string.IsNullOrWhiteSpace(state) || gId > 0)
            {
                IList<GreenPro.Data.Garage> model = new List<GreenPro.Data.Garage>();
                               
                if (gId > 0)
                {

                    garages = garages.Where(b => b.GarageId == gId);
                    model = garages.ToList();
                }

                var modelResult = model.Select(a => new { a.Garage_Name, a.Latitute, a.Longitude, a.Pincode, a.Garage_Address, a.IsActive, a.OpenTime, a.ServiceDays }).ToList();
                return Json(modelResult, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = "Success" });


        }

        public ActionResult Index(int? page, string state, string searchText, int? GarageId)
        {
            DateTime currentDate = DateTime.Now;
            

            int currentPageIndex = page.HasValue ? page.Value : 1;
            int gId = 0;
            gId = GarageId.HasValue ? GarageId.Value : 0;
            var garages = db.Garages.AsQueryable();
            garages = garages.Where(b => b.IsActive == true);


            IList<GarageSearchModel> GaragesList = new List<GarageSearchModel>();

            if (!string.IsNullOrWhiteSpace(state))
            {
                int stateId = 0;
                int.TryParse(state, out stateId);
                garages = garages.Where(b => b.City == stateId);

                foreach (var garageItem in garages)
                {
                    GaragesList.Add(new GarageSearchModel() { Garage_Name = garageItem.Garage_Name, GarageId = garageItem.GarageId });
                }
            }

            int pageSize = 5;
            var statesList = db.States.ToList();
            ViewBag.AvailableStates = new SelectList(statesList, "Id", "StateName");
            ViewBag.SearchTextData = searchText;
            ViewBag.stateData = state;            

            var cityList = db.Database.SqlQuery<CityModel>("exec dbo.GetAllAvailableGaragesCitiesList").ToList();

            ViewBag.AvailableCitys = new SelectList(cityList, "Id", "CityName");
            //ViewBag.AvailableGarages = new SelectList(garages, "GarageId", "Garage_Name");
            ViewBag.AvailableGarages = new SelectList(GaragesList, "GarageId", "Garage_Name");

            if ((!string.IsNullOrWhiteSpace(searchText) || !string.IsNullOrWhiteSpace(state)) || gId>0 || Request.IsAjaxRequest())
            {
                IList<GarageModel> model = new List<GarageModel>();
                
                if (!string.IsNullOrWhiteSpace(state))
                {
                    int stateId = 0;
                    int.TryParse(state, out stateId);
                    garages = garages.Where(b => b.City == stateId);
                }

                if (gId > 0)
                {
                    garages = garages.Where(b => b.GarageId == gId);
                    var garagesList = garages.ToList();
                    foreach (var item in garagesList)
                    {
                        GarageModel gModel = new GarageModel();
                        gModel.GarageId = item.GarageId;
                        gModel.Garage_Name = item.Garage_Name;
                        gModel.Garage_Address = item.Garage_Address;
                        gModel.OpenTime = item.OpenTime;

                        if (item.OpenTime.HasValue)
                        {
                            DateTime opentime = DateTime.Today.Add(item.OpenTime.Value);
                            gModel.OpenTimeStr = opentime.ToString("hh:mm tt");
                        }
                        gModel.CloseTime = item.CloseTime;                       
                        DateTime closetime = DateTime.Today.Add(item.CloseTime);
                        gModel.CloseTimeStr = closetime.ToString("hh:mm tt");                        

                        gModel.ServiceDays = item.ServiceDays;
                        if (item.City1 != null)
                            gModel.City = item.City1.CityName;
                        if (item.State1 != null)
                            gModel.State = item.State1.StateName;
                        gModel.Pincode = item.Pincode;
                        model.Add(gModel);
                    }
                }

                
                ViewBag.SearchTextData = searchText;
                ViewBag.SearchGarageData = gId;
                ViewBag.stateData = state;

                int pageNumber = (page ?? 1);
                if (Request.IsAjaxRequest())
                    return PartialView("_SearchAddress", model.ToPagedList(pageNumber, int.MaxValue));
            }
            else
            {
                page = 1;
            }


            return View();
        }

        // GET: Garages/Details/5


        // GET: Garages/Create
        public ActionResult Create()
        {
            ViewBag.StateId = new SelectList(db.States, "Id", "StateName");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");
            return View();
        }

        // POST: Garages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Garage garage, string StateId, string CityId)
        {
            ModelState.Remove("Country");
            ModelState.Remove("CreatedDt");
            ModelState.Remove("CreatedBy");
            ModelState.Remove("IsActive");
            IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyA3CNMI-_JAV9-dWIctroZQTuUwjZygT3A" };
            IEnumerable<Address> addresses = geocoder.Geocode(garage.Garage_Address);
            garage.Latitute = addresses.First().Coordinates.Latitude;
            garage.Longitude = addresses.First().Coordinates.Longitude;
            var response = Request["g-recaptcha-response"];
            CaptchaResponse cap = new CaptchaResponse();
            if (cap.ValidateCaptcha(response))
            {

                if (ModelState.IsValid)
                {
                    garage.City = Convert.ToInt32(CityId);
                    garage.State = Convert.ToInt32(StateId);
                    garage.Country = "US";
                    garage.CreatedBy = "WebsiteUser";
                    garage.CreatedDt = DateTime.Now;
                    garage.IsActive = false;
                    db.Garages.Add(garage);
                    db.SaveChanges();
                    //Send Mail
                    return RedirectToAction("Thankyou");
                }

            }
            else
            {
                ViewBag.CaptchaError = "<p class='alert alert-danger'>Invalid Captcha</p>";
            }
            ViewBag.StateId = new SelectList(db.States, "Id", "StateName", garage.State);
            ViewBag.CityId = new SelectList(db.Cities.Where(b => b.StateID == garage.State), "Id", "CityName", garage.City);
            return View(garage);
        }


        public ActionResult Thankyou()
        {
            return View();

        }

        [AllowAnonymous]
        public JsonResult Citylist(string id)
        {
            var stateId = Convert.ToInt32(id);
            GreenProDbEntities db = new GreenProDbEntities();
            var cities = db.Cities.Where(a => a.StateID.Value == stateId).ToList();
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            foreach (var city in cities)
            {
                selectListItemList.Add(new SelectListItem { Text = city.CityName, Value = city.Id.ToString() });
            }
            return Json(selectListItemList);
        }
               

        public ActionResult GarageSearch(string CityID = "0", string StateID = "0")
        {
            int city = Convert.ToInt32(CityID);
            int state = Convert.ToInt32(StateID);
            GarageSearchViewModel model = new GarageSearchViewModel();
            model.Garages = db.Garages.Where(m => m.City == city && m.State == state).ToList();
            ViewBag.StateId = new SelectList(db.States, "Id", "StateName");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");
            return PartialView("_GarageSearch", model);
        }

        public ActionResult LoadGarageByCityId(int cityId)
        {

            IList<GarageModel> model = new List<GarageModel>();
                
           
            if (cityId>0)
            {
                var garages = db.Garages.AsQueryable();
                garages = garages.Where(b => b.IsActive == true);
                garages = garages.Where(b => b.City == cityId);
                var result = garages.Select(s => new GarageModel { Garage_Name = s.Garage_Name, GarageId=s.GarageId });
                model = result.ToList();
            }




            return Json(model, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
