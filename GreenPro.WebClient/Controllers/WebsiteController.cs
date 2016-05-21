using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenPro.Data;
using GreenPro.WebClient.ViewModel;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Data.Entity.Core;
namespace GreenPro.WebClient.Controllers
{
    [Authorize]
    public class WebsiteController : Controller
    {
        private GreenProDbEntities db = new GreenProDbEntities();
        //
        // GET: /Website/
        public ActionResult Garages()
        {
            GreenProDbEntities _db = new GreenProDbEntities();
            IEnumerable<Garage> places = _db.Garages.ToList();
            return View(places);
        }

        [HttpPost]
        public ActionResult Search(string Location)
        {

            GreenProDbEntities GE = new GreenProDbEntities();
            var result = GE.Garages.Where(x => x.Garage_Name.StartsWith(Location)).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Services()
        {
            return View();
        }

        //public ActionResult SubscribePackage(int packageid, int cartype)
        //{
        //    //Check the car type and Pakcage of user may be later
        //    UserPackageAddOnViewModel packageDetails = new UserPackageAddOnViewModel();

        //    if (Session["SelectedCar"] != null)
        //    {
        //        var car = Session["SelectedCar"] as CarUser;

        //        packageDetails.SelectedCar = car.CarId.ToString();
        //        ViewBag.SelectedCarName = car.DisplayName;
        //    }


        //    var userid = User.Identity.GetUserId();

        //    var carUserList = db.CarUsers.Where(a => a.UserId == userid && a.CarType.Id == cartype);
        //    var packageCarUserList = db.UserPackages.Where(a => a.UserId == userid && a.PaymentRecieved == true).Select(a => a.CarId).ToList();
        //    var list = new List<CarUser>();
        //    foreach (var carUser in carUserList)
        //    {
        //        if (!packageCarUserList.Contains(carUser.CarId))
        //        {
        //            list.Add(carUser);
        //        }
        //    }

        //    packageDetails.UserCars = list;

        //    //int latestCar = packageDetails.UserCars.ToList().OrderByDescending(a => a.CarId).Select(a => a.CarId).FirstOrDefault();
        //    //ViewBag.LatestCar = latestCar;
        //    int latestCar = list.OrderByDescending(a => a.CarId).Select(a => a.CarId).FirstOrDefault();
        //    ViewBag.LatestCar = latestCar;
        //    //Store packageid in view
        //    packageDetails.PackageID = packageid;
        //    if (packageid == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    else
        //    {
        //        var package = db.Packages.Where(a => a.PackageId == packageid).FirstOrDefault();
        //        if (package == null)
        //        {
        //            return HttpNotFound();
        //        }

        //        packageDetails.Packages = package;
        //        //packageDetails.Services = db.Services.ToList();
        //        foreach (var item in db.Services.ToList())
        //        {
        //            bool flag = true;
        //            foreach (var subItem in package.Package_Services)
        //            {
        //                if (item.ServiceID == subItem.ServiceID)
        //                {
        //                    flag = false;
        //                    break;
        //                }
        //            }
        //            if (flag)
        //            {
        //                if (packageDetails.Services == null)
        //                    packageDetails.Services = new List<Service>();
        //                (packageDetails.Services as List<Service>).Add(item);
        //            }
        //        }
        //        //packageDetails.Services = (from s in package.Package_Services
        //        //                           join from a in db.Services
        //        //                           where s.ServiceID != a.ServiceID
        //        //                           select a).ToList();

        //    }

        //    if (Session["NewServiceGarageId"] != null)
        //    {
        //        int garageId = 0;
        //        int.TryParse(Convert.ToString( Session["NewServiceGarageId"]), out garageId);
        //        if (garageId > 0)
        //        {
        //            var garage = db.Garages.Where(i => i.GarageId == garageId).SingleOrDefault();
        //            if (garage != null)
        //            {
        //                var serviceDays = garage.ServiceDays.Split(',');
        //                packageDetails.ServiceDay = serviceDays[0];
        //                foreach (var day in serviceDays)
        //                {
        //                    packageDetails.AvailableServiceDays.Add(new SelectListItem()
        //                    {
        //                        Text = day,
        //                        Value = day
        //                    });
        //                }
        //            }
        //        }
        //    }

        //    return View(packageDetails);
        //}


        // Added By circus
        public ActionResult SubscribePackage(int packageid, int cartype=0)
        {
            //Check the car type and Pakcage of user may be later
            UserPackageAddOnViewModel packageDetails = new UserPackageAddOnViewModel();

            if (Session["SelectedCar"] != null)
            {
                var car = Session["SelectedCar"] as CarUser;

                packageDetails.SelectedCar = car.CarId.ToString();
                ViewBag.SelectedCarName = car.DisplayName;
            }


            var userid = User.Identity.GetUserId();

            var carUserList = db.CarUsers.Where(a => a.UserId == userid);
            var packageCarUserList = db.UserPackages.Where(a => a.UserId == userid && a.PaymentRecieved == true).Select(a => a.CarId).ToList();
            var list = new List<CarUser>();
            foreach (var carUser in carUserList)
            {
                if (!packageCarUserList.Contains(carUser.CarId))
                {
                    list.Add(carUser);
                }
            }

            packageDetails.UserCars = list;           
            
            int latestCar = list.OrderByDescending(a => a.CarId).Select(a => a.CarId).FirstOrDefault();
            ViewBag.LatestCar = latestCar;
            //Store packageid in view
            packageDetails.PackageID = packageid;
            if (packageid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var package = db.Packages.Where(a => a.PackageId == packageid).FirstOrDefault();
                if (package == null)
                {
                    return HttpNotFound();
                }
                packageDetails.Packages = package;
                packageDetails.Services = db.Services.Where(s => s.IsAddOn == true).ToList();                                

            }

            if (Session["NewServiceGarageId"] != null)
            {
                int garageId = 0;
                int.TryParse(Convert.ToString(Session["NewServiceGarageId"]), out garageId);
                if (garageId > 0)
                {
                    packageDetails.GarageId = garageId;
                    var garage = db.Garages.Where(i => i.GarageId == garageId).SingleOrDefault();
                    if (garage != null)
                    {
                        var serviceDays = garage.ServiceDays.Split(',');
                        packageDetails.ServiceDay = serviceDays[0];
                        foreach (var day in serviceDays)
                        {
                            packageDetails.AvailableServiceDays.Add(new SelectListItem()
                            {
                                Text = day,
                                Value = day
                            });
                        }
                    }

                    // prepare Time Slots List
                    var defaultFirstDay = packageDetails.AvailableServiceDays.FirstOrDefault();
                    var userPackagesList = db.UserPackages.Where(a => a.UserId == userid && a.PaymentRecieved == true && a.IsActive==true).ToList();
                    if (defaultFirstDay != null)
                    {
                        var garageTimeSlotList = db.GargesTimeingSlots.Where(g => g.GarageId == garageId).ToList();
                        foreach (var timeSlot in garageTimeSlotList)
                        {
                            var userPackageByTimeSlot = userPackagesList.Where(u => u.GaragesTimeingSlotId == timeSlot.Id && u.ServiceDay == defaultFirstDay.Text).FirstOrDefault();
                            if (userPackageByTimeSlot != null)
                                continue;
                            
                            packageDetails.AvailableGaragesTimeingSlots.Add(new SelectListItem()
                            {
                                Text = timeSlot.SlotTimeing,
                                Value = timeSlot.Id.ToString()
                            });
                        }
                    }


                }
            }

            return View(packageDetails);
        }


        [HttpPost]
        public ActionResult SubscribePackage(UserPackageAddOnViewModel model, string[] services)
        {
            var package = db.Packages.Where(a => a.PackageId == model.PackageID).FirstOrDefault();
            if (package == null)
                return View();
            var userId = User.Identity.GetUserId();
            //var carList = db.CarUsers.Where(a => a.UserId == userId).FirstOrDefault();
            //if (carList == null)
            //    return View();
            int caridfromuserpage = Convert.ToInt32(model.SelectedCar);
            if (Session["SelectedCar"] != null)
            {
                var car = Session["SelectedCar"] as CarUser;
                caridfromuserpage = car.CarId;
            }

            //Save User Packges
            var savingEntity = new UserPackage
            {
                UserId = userId,
                PackageId = model.PackageID,
                CarId = caridfromuserpage,
                SubscribedDate = DateTime.Now,
                ActualPrice = package.Package_Price,
                TotalPrice = model.Packages.Package_Price,
                PriceWithAddOns = model.Packages.Package_Price - package.Package_Price,
                DiscountPrice = 0,
                CreatedDt = DateTime.Now,
                PaymentRecieved = false,
                ServiceDay=model.ServiceDay,
                GaragesTimeingSlotId=model.GaragesTimeingSlotId,
            };
            db.UserPackages.Add(savingEntity);

            db.SaveChanges();
            //Sve uer addson
            if (services != null)
            {
                //Save user addons
                UserPackagesAddon addon = new UserPackagesAddon();
                foreach (var item in services)
                {
                    addon.ServiceID = Convert.ToInt32(item);
                    addon.UserPackageID = savingEntity.Id;
                    addon.ActualPrice = 0;
                    addon.DiscountPrice = 0;
                    addon.CreatedDt = DateTime.Now;
                    db.UserPackagesAddons.Add(addon);
                    db.SaveChanges();
                }
            }
            if (Session["NewServiceGarageId"] != null)
            {
                var car = db.CarUsers.Where(a => a.CarId == caridfromuserpage).FirstOrDefault();
                car.GarageId = Convert.ToInt32(Session["NewServiceGarageId"]);
                db.SaveChanges();
            }
            //save car auto renewal
            //If this is first car set Autorenewal to True
            model.AutoRenewalSubscription = true;
            if (model.AutoRenewalSubscription)
            {
                if (Session["SelectedCar"] != null)
                {
                    var carTemp = Session["SelectedCar"] as CarUser;
                    var car = db.CarUsers.Where(a => a.CarId == carTemp.CarId).FirstOrDefault();
                    car.AutoRenewal = model.AutoRenewalSubscription;
                    db.SaveChanges();
                }
                else
                {
                    var carId = Convert.ToInt32(model.SelectedCar);
                    var car = db.CarUsers.Where(a => a.CarId == carId).FirstOrDefault();
                    car.AutoRenewal = model.AutoRenewalSubscription;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("PrepaymentConfirmation", new { id = savingEntity.Id });
        }



        [AllowAnonymous]
        public JsonResult GarageTimeSlotlist(string serviceDay, int garageId)
        {
            var userid = User.Identity.GetUserId();

            GreenProDbEntities db = new GreenProDbEntities();                     
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            var garageTimeSlotList = db.GargesTimeingSlots.Where(g => g.GarageId == garageId).ToList();
            var userPackagesList = db.UserPackages.Where(a => a.UserId == userid && a.PaymentRecieved == true && a.IsActive == true).ToList();
            foreach (var timeSlot in garageTimeSlotList)
            {
                var userPackageByTimeSlot = userPackagesList.Where(u => u.GaragesTimeingSlotId == timeSlot.Id && u.ServiceDay == serviceDay).FirstOrDefault();
                if (userPackageByTimeSlot != null)
                    continue;
                selectListItemList.Add(new SelectListItem { Text = timeSlot.SlotTimeing, Value = timeSlot.Id.ToString() });
            }
            return Json(selectListItemList);
        }


        public ActionResult PrePaymentConfirmation(int id)
        {
            UserPackageAddOn userAndAddon = new UserPackageAddOn();
            userAndAddon.UserPackge = db.UserPackages.Where(a => a.Id == id).FirstOrDefault();
            userAndAddon.Addons = db.UserPackagesAddons.Where(a => a.UserPackageID == id);
            userAndAddon.UserPackageID = id.ToString();

            //adding tax
            if (db.Taxes != null && db.Taxes.Count() > 0)
            {
                var taxPercentage = db.Taxes.Select(a => a.TaxPercentage).Sum();
                userAndAddon.UserPackge.TaxAmount = userAndAddon.TaxAmount = userAndAddon.UserPackge.TotalPrice * (taxPercentage / 100);
                userAndAddon.TaxPercentage = taxPercentage;
                db.SaveChanges();
            }

            return View(userAndAddon);
        }
        [HttpPost]
        public ActionResult PrePaymentConfirmation(UserPackageAddOn package, string UserPackageID)
        {

            return RedirectToAction("ProcessPaypalPayment", "Paypal", new { userpackageid = UserPackageID });

            //if (package.AcceptAgreement == "true")
            //{
            //    return RedirectToAction("ProcessPaypalPayment", "Paypal", new { userpackageid = UserPackageID });
            //}
            //else
            //    return View();
        }

        public ActionResult TermsCondition()
        {

            return PartialView("_TermsAgreement");
        }

        public ActionResult TipAmount(int id)
        {
            var model = new UserTip { UserPackageId = id };
            //ViewBag.ID = id;
            return PartialView("_TipAmount", model);

        }

        [HttpPost]
        public ActionResult TipAmount(UserTip model)
        {
            if (ModelState.IsValid)
            {
                var userPackage = db.UserPackages.Where(a => a.Id == model.UserPackageId).FirstOrDefault();                
                try
                {
                    userPackage.TipAmount = model.TIPAMOUNT;
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException)
                {

                }
                return Json(new { success = true });
            }
            //ViewBag.ID = model.USERPACKAGEID;
            return PartialView("_TipAmount", model);

        }

        public ActionResult DeleteTip(int id)
        {
            var userPackage = db.UserPackages.Where(a => a.Id == id).FirstOrDefault();
            var tip = new UserTip { UserPackageId = id };
            if (userPackage != null)
            {
                tip.TIPAMOUNT = userPackage.TipAmount;
            }
            //ViewBag.ID = id;
            return PartialView("_Delete", tip);
        }

        [HttpPost]
        public ActionResult DeleteTip(UserTip model)
        {
            var userPackage = db.UserPackages.Where(a => a.Id == model.UserPackageId).FirstOrDefault();
            userPackage.TipAmount = model.TIPAMOUNT;
            db.SaveChanges();            
            //ViewBag.ID = model.USERPACKAGEID;
            return Json(new { success = true });
        }

    }
}