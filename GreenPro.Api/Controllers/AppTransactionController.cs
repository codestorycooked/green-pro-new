//using GreenPro.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Http;

//namespace GreenPro.Api.Controllers
//{
//    [Authorize]
//    public class AppTransactionController : ApiController
//    {
//        private GreenPro.Data.GreenProDbEntities db;
//        public AppTransactionController()
//        {
//            this.db = new Data.GreenProDbEntities();
//        }
//        [HttpGet]
//        [Route("api/AppTrasaction/subscribe")]
//        public IHttpActionResult SubscribePackage(int packageid, int garageid, string carID, string userID, int cartype = 0)
//        {
//            //Check the car type and Pakcage of user may be later
//            UserPackageAddOnViewModel packageDetails = new UserPackageAddOnViewModel();
//            packageDetails.SubscriptionTypeId = 1;
//            packageDetails.SelectedCar = carID;
//            var userid = userID;

//            var carUserList = db.CarUsers.Where(a => a.UserId == userid);
//            var packageCarUserList = db.UserPackages.Where(a => a.UserId == userid && a.PaymentRecieved == true).Select(a => a.CarId).ToList();
//            var list = new List<CarUser>();
//            foreach (var carUser in carUserList)
//            {
//                if (!packageCarUserList.Contains(carUser.CarId))
//                {
//                    list.Add(carUser);
//                }
//            }

//            packageDetails.UserCars = list;

//            int latestCar = list.OrderByDescending(a => a.CarId).Select(a => a.CarId).FirstOrDefault();
//            //ViewBag.LatestCar = latestCar;
//            //Store packageid in view
//            packageDetails.PackageID = packageid;
//            if (packageid == null)
//            {
//                return BadRequest("Package ID is Null");
//            }
//            else
//            {
//                var package = db.Packages.Where(a => a.PackageId == packageid).FirstOrDefault();
//                if (package == null)
//                {
//                    return NotFound();
//                }
//                packageDetails.Packages = package;
//                packageDetails.Services = db.Services.Where(s => s.IsAddOn == true).ToList();

//            }

//            //            if (Session["NewServiceGarageId"] != null)
//            //          {

//            if (garageid > 0)
//            {
//                packageDetails.GarageId = garageid;
//                var garage = db.Garages.Where(i => i.GarageId == garageid).SingleOrDefault();
//                if (garage != null)
//                {
//                    var serviceDays = garage.ServiceDays.Split(',');
//                    packageDetails.ServiceDay = serviceDays[0];
//                    foreach (var day in serviceDays)
//                    {
//                        packageDetails.AvailableServiceDays.Add(new SelectListItem()
//                        {
//                            Text = day,
//                            Value = day
//                        });
//                    }
//                }

//                // prepare Time Slots List
//                var defaultFirstDay = packageDetails.AvailableServiceDays.FirstOrDefault();
//                var userPackagesList = db.UserPackages.Where(a => a.UserId == userid && a.PaymentRecieved == true && a.IsActive == true).ToList();
//                if (defaultFirstDay != null)
//                {
//                    var garageTimeSlotList = db.GargesTimeingSlots.Where(g => g.GarageId == garageid).ToList();
//                    foreach (var timeSlot in garageTimeSlotList)
//                    {
//                        var userPackageByTimeSlot = userPackagesList.Where(u => u.GaragesTimeingSlotId == timeSlot.Id && u.ServiceDay == defaultFirstDay.Text).FirstOrDefault();
//                        if (userPackageByTimeSlot != null)
//                            continue;

//                        packageDetails.AvailableGaragesTimeingSlots.Add(new SelectListItem()
//                        {
//                            Text = timeSlot.SlotTimeing,
//                            Value = timeSlot.Id.ToString()
//                        });
//                    }
//                }

//                var package = db.Packages.Where(a => a.PackageId == packageid).FirstOrDefault();
//                if (package == null)
//                {
//                    return NotFound();
//                }

//                var SubscriptionTypeArray = package.SubscriptionTypes.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
//                var SubsscriptionInfoList = SubscriptionTypeInfo.GetSubscriptionTypeList();

//                foreach (var item in SubsscriptionInfoList)
//                {
//                    if (SubscriptionTypeArray.Contains(item.Value))
//                    {
//                        packageDetails.AvailableSubscriptionTypes.Add(item);
//                    }
//                }


//                //packageDetails.AvailableSubscriptionTypes.Add(new SelectListItem()
//                //{
//                //    Text = "Bi-Weekly",
//                //    Value = "2"
//                //});
//                //packageDetails.AvailableSubscriptionTypes.Add(new SelectListItem()
//                //{
//                //    Text = "Monthly",
//                //    Value = "3"
//                //});





//            }
//            return ok(packageDetails);
//        }
//        [HttpPost]
//        [Route("api/AppTrasaction/subscribe")]
//        public IHttpActionResult SubscribePackage(UserPackageAddOnViewModel model, string[] services)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var package = db.Packages.Where(a => a.PackageId == model.PackageID).FirstOrDefault();
//            if (package == null || model.UserId == string.Empty)
//                return BadRequest("Package or USerId is Null");
//            var userId = model.UserId;
//            //var carList = db.CarUsers.Where(a => a.UserId == userId).FirstOrDefault();
//            //if (carList == null)
//            //    return View();
//            int caridfromuserpage = Convert.ToInt32(model.SelectedCar);
//            //if (Session["SelectedCar"] != null)
//            //{
//            //    var car = Session["SelectedCar"] as CarUser;
//            //    caridfromuserpage = car.CarId;
//            //}

//            //Save User Packges
//            var savingEntity = new UserPackage
//            {
//                UserId = userId,
//                PackageId = model.PackageID,
//                CarId = caridfromuserpage,
//                SubscribedDate = DateTime.Now,
//                ActualPrice = package.Package_Price,
//                TotalPrice = model.Packages.Package_Price,
//                PriceWithAddOns = model.Packages.Package_Price - package.Package_Price,
//                DiscountPrice = 0,
//                CreatedDt = DateTime.Now,
//                PaymentRecieved = false,
//                ServiceDay = model.ServiceDay,
//                SubscriptionTypeId = model.SubscriptionTypeId
//                // GaragesTimeingSlotId=model.GaragesTimeingSlotId,
//            };
//            db.UserPackages.Add(savingEntity);

//            db.SaveChanges();
//            //Sve uer addson
//            if (services != null)
//            {
//                //Save user addons
//                UserPackagesAddon addon = null;
//                foreach (var item in services)
//                {
//                    int serviceId = 0;
//                    int.TryParse(item, out serviceId);
//                    if (serviceId > 0)
//                    {

//                        var serviceAddOns = db.Services.FirstOrDefault(s => s.ServiceID == serviceId);
//                        if (serviceAddOns != null)
//                        {
//                            addon = new UserPackagesAddon();
//                            addon.ServiceID = Convert.ToInt32(item);
//                            addon.UserPackageID = savingEntity.Id;
//                            addon.ActualPrice = serviceAddOns.Service_Price;
//                            addon.DiscountPrice = 0;
//                            addon.CreatedDt = DateTime.Now;
//                            db.UserPackagesAddons.Add(addon);
//                            db.SaveChanges();
//                        }
//                    }
//                }
//            }

//            var car = db.CarUsers.Where(a => a.CarId == caridfromuserpage).FirstOrDefault();
//            car.GarageId = Convert.ToInt32(model.GarageId);
//            db.SaveChanges();

//            //save car auto renewal
//            //If this is first car set Autorenewal to True
//            model.AutoRenewalSubscription = true;
//            //if (model.AutoRenewalSubscription)
//            //{
//            //    if (Session["SelectedCar"] != null)
//            //    {
//            //        var carTemp = Session["SelectedCar"] as CarUser;
//            //        var car = db.CarUsers.Where(a => a.CarId == carTemp.CarId).FirstOrDefault();
//            //        car.AutoRenewal = model.AutoRenewalSubscription;
//            //        db.SaveChanges();
//            //    }
//            //    else
//            //    {
//            //        var carId = Convert.ToInt32(model.SelectedCar);
//            //        var car = db.CarUsers.Where(a => a.CarId == carId).FirstOrDefault();
//            //        car.AutoRenewal = model.AutoRenewalSubscription;
//            //        db.SaveChanges();
//            //    }
//            //}
//            return Ok(savingEntity);
//        }


//        //Prepayment
//        public IHttpActionResult PrePaymentConfirmation(int id)
//        {
//            UserPackageAddOn userAndAddon = new UserPackageAddOn();
//            userAndAddon.UserPackge = db.UserPackages.Where(a => a.Id == id).FirstOrDefault();
//            userAndAddon.Addons = db.UserPackagesAddons.Where(a => a.UserPackageID == id);
//            userAndAddon.UserPackageID = id.ToString();

//            //adding tax
//            if (db.Taxes != null && db.Taxes.Count() > 0)
//            {
//                var taxPercentage = db.Taxes.Select(a => a.TaxPercentage).Sum();
//                userAndAddon.UserPackge.TaxAmount = userAndAddon.TaxAmount = userAndAddon.UserPackge.TotalPrice * (taxPercentage / 100);
//                userAndAddon.TaxPercentage = taxPercentage;
//                db.SaveChanges();
//            }

//            return Ok(userAndAddon);
//        }
//    }
//}
