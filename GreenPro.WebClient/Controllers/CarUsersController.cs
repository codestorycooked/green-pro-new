using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreenPro.Data;
using Microsoft.AspNet.Identity;
using GreenPro.WebClient.ViewModel;
using GreenPro.WebClient.Models;
using PagedList;

namespace GreenPro.WebClient.Controllers
{
    [Authorize]
    public class CarUsersController : Controller
    {
        private GreenProDbEntities db = new GreenProDbEntities();

        WorkflowMessageService _workflowMessageService;
        public CarUsersController()
        {
            _workflowMessageService = new WorkflowMessageService();
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
        // GET: CarUsers
        public ActionResult Index()
        {
            Session["NewServiceCarTypeId"] = null;
            Session["SelectedCar"] = null;
            Session["NewServiceGarageId"] = null;

            string userid = User.Identity.GetUserId();

            ViewBag.StateId = new SelectList(db.States, "Id", "StateName");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");

            var model = new CarGarageUserViewModel();
            //model.CarUser = db.CarUsers.Include(c => c.AspNetUser).Where(a => a.UserId == userid).ToList();
            var carUserList = db.CarUsers.Include(c => c.AspNetUser).Where(a => a.UserId == userid).ToList();
            var subscriptions = db.UserPackages.Where(a => a.UserId == userid).ToList();
            var adhocSubscriptions = db.AdhocUserPackages.Where(a => a.UserId == userid).ToList();
            List<CarUserModel> carList = new List<CarUserModel>();
            List<AdhocCarViewModel> adhocCarList = new List<AdhocCarViewModel>();
            foreach (var car in carUserList)
            {
                var carUserModel = new CarUserModel
                {
                    CarId = car.CarId,
                    DisplayName = car.DisplayName,
                    Make = car.Make,
                    AutoRenewal = car.AutoRenewal

                    //GarageName = car.Garage.Garage_Name
                };
                if (car.GarageId.HasValue)
                {
                    carUserModel.GarageId = car.GarageId.Value;

                }
                if (car.Garage != null)
                {
                    carUserModel.GarageName = car.Garage.Garage_Name;
                }
                bool isNotAdhocCar = false;
                foreach (var subscription in subscriptions)
                {
                    if (subscription.CarId == car.CarId && subscription.PaymentRecieved)
                    {
                        carUserModel.SubscriptionBought = true;
                        carUserModel.SubscriptionName = subscription.Package.Package_Name;

                        // Added By Sachin
                        carUserModel.ServiceDay = subscription.ServiceDay;
                        isNotAdhocCar = true;
                        break;
                    }
                }
                if (!isNotAdhocCar)
                {
                    isNotAdhocCar = true;
                    foreach (var adhocSubscription in adhocSubscriptions)
                    {
                        if (adhocSubscription.CarId == car.CarId && adhocSubscription.PaymentRecieved && adhocSubscription.Processed==false)
                        {
                            var linkedCarName = carUserList.Where(a => a.CarId == adhocSubscription.CarId).Select(a => a.DisplayName).FirstOrDefault();
                            adhocCarList.Add(new AdhocCarViewModel { CarId = car.CarId, DisplayName = car.DisplayName, Make = car.Make, LinkedCarDisplayName = linkedCarName });
                            isNotAdhocCar = false;
                            break;
                        }
                    }
                }
                if (isNotAdhocCar)
                {
                    carList.Add(carUserModel);
                }
            }
            model.CarUser = carList;
            model.AdhocCarUser = adhocCarList;
            var carUsers = db.CarUsers.Include(c => c.AspNetUser).Where(a => a.UserId == userid);
            var id = carUsers.Where(a => a.AutoRenewal == true).Select(a => a.CarId).FirstOrDefault();
            ViewBag.DefaultCarId = id;
            //GetNotifications(model.CarUser.Count());
            //ViewBag.Notify = "Today your Mercedes Benz will be cleaned at Norway Garage";
            return View(model);


        }

        //private void GetNotifications(int count)
        //{
        //    string userid = User.Identity.GetUserId();
        //    var garageUSer = db.GarageUsers.Where(a => a.UserId == userid).Count();
        //    if (count == 0)
        //    {
        //        ViewBag.Notify = "You have NO Cars added.";
        //    }
        //    else if (garageUSer == 0)
        //    {
        //        ViewBag.Notify = "You have No Garages Selected";
        //    }
        //    else
        //    {

        //        var getGarageDetails = db.GarageUsers.Where(a => a.UserId == userid).First();
        //        var getCarDetails = db.CarUsers.Where(a => a.UserId == userid && a.AutoRenewal == true).First();
        //        ViewBag.Notify = "Your Default Car is <b> <u> " + getCarDetails.DisplayName + " </u></b> and Garage is<b> <u>" + getGarageDetails.Garage.Garage_Name + "</u></b>";
        //    }



        //}


        // GET: CarUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarUser carUser = db.CarUsers.Find(id);
            if (carUser == null)
            {
                return HttpNotFound();
            }
            return View(carUser);
        }

        // GET: CarUsers/Create
        public ActionResult Create()
        {
            if (Session["NewServiceCarTypeId"] != null)
            {
                int carTypeId = Convert.ToInt32(Session["NewServiceCarTypeId"]);
                var list = new SelectList(db.CarTypes.Where(a => a.Id == carTypeId), "Id", "Description");
                ViewBag.Type = list;
            }
            else
                ViewBag.Type = new SelectList(db.CarTypes, "Id", "Description");
            //if (Session["NewServiceCarTypeId"] != null)
            //    ViewBag.SelectedCarType = Session["NewServiceCarTypeId"];
            return PartialView("Create");
        }

        // POST: CarUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarId,DisplayName,Make,LicenseNumber,Color,Type,PurchaseYear,Default")] CarUser carUser)
        {
            carUser.UserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                if (Session["NewServiceGarageId"] != null)
                    carUser.GarageId = Convert.ToInt32(Session["NewServiceGarageId"]);
                db.CarUsers.Add(carUser);
                db.SaveChanges();
                return Json(new { success = true });
            }

            //ViewBag.Type = new SelectList(db.CarTypes, "Id", "Description", carUser.Type);

            return PartialView("Create", carUser);

        }

        // GET: CarUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarUser carUser = db.CarUsers.Find(id);
            if (carUser == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Type = new SelectList(db.CarTypes, "Id", "Description", carUser.Type);

            return PartialView("Edit", carUser);
        }

        // POST: CarUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarId,DisplayName,Make,LicenseNumber,Color,Type,PurchaseYear,Default")] CarUser carUser)
        {
            carUser.UserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {


                db.Entry(carUser).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { success = true });
            }
            //ViewBag.Type = new SelectList(db.CarTypes, "Id", "Description", carUser.Type);

            return PartialView("Edit", carUser);
        }

        // GET: CarUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarUser carUser = db.CarUsers.Find(id);
            if (carUser == null)
            {
                return HttpNotFound();
            }
            else
            {
                //if (carUser.Default)
                //{
                //    ViewBag.Disabled = "true";
                //    ViewBag.Message = "Cannot Delete Default. Please set default car first and try again";
                //}
                //else
                //{
                //    ViewBag.Disabled = "false";
                //    ViewBag.Message = "Do You want to delete this car ?";
                //}

                //ViewBag.Disabled = "false";
                ViewBag.Message = "Do You want to delete this car ?";
            }
            return PartialView("Delete", carUser);
        }

        // POST: CarUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarUser carUser = db.CarUsers.Find(id);
            db.CarUsers.Remove(carUser);
            db.SaveChanges();
            return Json(new { success = true });
        }


        public ActionResult Renewal(int id, bool cancel)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarUser carUser = db.CarUsers.Find(id);
            if (carUser == null)
            {
                return HttpNotFound();
            }
            if (cancel)
            {
                ViewBag.Message = "Renewal cancellation would mean next week onwards the no more amount would be automatically deducted. Do you agree to the terms and conditions?";
            }
            else
            {
                ViewBag.Message = "Renewal would mean next week onwards the amount would be automatically deducted. Do you agree to the terms and conditions?";
            }
            return PartialView("Renewal", carUser);
            //var newDefaultCar = db.CarUsers.Find(carId);
            //newDefaultCar.Default = cancel;

            //db.SaveChanges();

            //return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Renewal")]
        public ActionResult RenewalConfirmed(int id, bool cancel)
        {
            var newDefaultCar = db.CarUsers.Find(id);
            newDefaultCar.AutoRenewal = !cancel;

            db.SaveChanges();

            // added by circus 07 march 2016
            var packageInfo = newDefaultCar.UserPackages.Where(p => p.PaymentRecieved == true).FirstOrDefault();

            var userInfo = newDefaultCar.AspNetUser;

            _workflowMessageService.SendSubscriptionCancelNotificationToAdmin(userInfo.FirstName + " " + userInfo.LastName, userInfo.UserName, userInfo.PhoneNumber, userInfo.Email);

            //return RedirectToAction("Index");
            return Json(new { success = true });
        }


        //public ActionResult GarageSearch(string searchText)
        //{

        //    //CarGarageUserViewModel model = new CarGarageUserViewModel();
        //    //model.Garages = db.Garages.Where(m => m.Garage_Address.ToLower().Contains(searchText.ToLower()) || m.Pincode.Contains(searchText) || m.Garage_Name.ToLower().Contains(searchText.ToLower())).ToList();
        //    ////ViewBag.StateId = new SelectList(db.States, "Id", "StateName");
        //    ////ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");
        //    return PartialView("_GarageSearch", model);
        //}

        public ActionResult SelectGarage(int id)
        {
            var selectedGarage = db.Garages.Where(m => m.GarageId == id).First();
            ViewBag.Message = "Do You want to make <b>" + selectedGarage.Garage_Name + "</b> as your default Garage";
            return PartialView("SelectGarage", selectedGarage);
        }

        //[HttpPost]
        //public ActionResult SelectGarage(Garage model)
        //{
        //    var uid = User.Identity.GetUserId();
        //   var garageUserMapping = db.GarageUsers.Where(a => a.UserId == uid).FirstOrDefault();
        //    if (garageUserMapping != null)
        //    {
        //        garageUserMapping.GarageId = model.GarageId;
        //        db.Entry(garageUserMapping).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return Json(new { success = true });
        //    }
        //    else
        //    {
        //        db.GarageUsers.Add(new GarageUser { GarageId = model.GarageId, UserId = uid });
        //        db.SaveChanges();
        //        return Json(new { success = true });
        //    }

        //}
        public ActionResult Subscription(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var car = db.CarUsers.Where(a => a.CarId == id).Select(a => a).FirstOrDefault();
            Session["SelectedCar"] = car;

            string userid = User.Identity.GetUserId();
            var carIdList = db.UserPackages.Where(a => a.UserId == userid && a.PaymentRecieved == true).ToList();
            var list = new List<SubscriptionViewModel>();
            list.Add(new SubscriptionViewModel { Id = -1, CarName = "Buy New Subscription" });
            foreach (var item in carIdList)
            {
                // comment by cirucs
                //list.Add(new SubscriptionViewModel { Id = item.CarId, CarName = item.CarUser.DisplayName + " (" + item.CarUser.CarType.Description + ")" });
                list.Add(new SubscriptionViewModel { Id = item.CarId, CarName = item.CarUser.DisplayName });
            }
            return PartialView("Subscription", list);
        }
        //[HttpPost, ActionName("Subscription")]
        public ActionResult SubscriptionConfirmed(int id)
        {
            if (id == -1)
                return RedirectToAction("Index", "Garages");
            else
                return RedirectToAction("Index", "AdhocCarService", new { id = id });
        }
        //public ActionResult Subscription(int id)
        //{
        //    var car = db.CarUsers.Where(a => a.CarId == id).Select(a => a).FirstOrDefault();
        //    Session["SelectedCar"] = car;
        //    return RedirectToAction("Index", "Garages");
        //}


        //Get Garages
        public ActionResult GetGarages(int? page, string search_carid, string searchText = "NY")
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;

            var garages = db.Garages;
            int pageSize = 5;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var model = garages.Where(b =>
                     b.Garage_Address.ToLower().Contains(searchText.ToLower()) ||
                     b.Garage_Name.ToLower().Contains(searchText.ToLower()) ||
                     b.Pincode.ToLower().Contains(searchText.ToLower())

                     && b.IsActive != false);
                ViewBag.Carid = search_carid;
                ViewBag.SearchText = searchText;
                int pageNumber = (page ?? 1);
                if (Request.IsAjaxRequest())
                    return PartialView("_SearchAddress", model.ToList().ToPagedList(pageNumber, pageSize));
            }
            else
            {
                page = 1;
            }


            return View();
        }
        public ActionResult ChangeGarage(int search_carid, int garageId)
        {
            var car = db.CarUsers.Where(a => a.CarId == search_carid).FirstOrDefault();
            car.GarageId = garageId;
            db.SaveChanges();

            return RedirectToAction("Index", "CarUsers", null);
        }

        [AllowAnonymous]
        public ActionResult GenerateInvoice(int id, string userid)
        {
            UserPackageAddOnInvoice userAndAddon = new UserPackageAddOnInvoice();
            userAndAddon.UserPackge = db.UserPackages.Where(a => a.Id == id).FirstOrDefault();
            userAndAddon.Addons = db.UserPackagesAddons.Where(a => a.UserPackageID == id);

            userAndAddon.UserDetails = db.AspNetUsers.Where(a => a.Id == userid).FirstOrDefault();
            userAndAddon.UserPackageID = id.ToString();

            return View(userAndAddon);
            //return View();
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
