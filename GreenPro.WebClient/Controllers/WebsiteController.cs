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
using GreenPro.PayPalSystem.Payments;
namespace GreenPro.WebClient.Controllers
{
    [Authorize]
    public class WebsiteController : Controller
    {
        private GreenProDbEntities db = new GreenProDbEntities();
        WorkflowMessageService _workflowMessageService;

        public WebsiteController()
        {            
            _workflowMessageService = new WorkflowMessageService();
        }

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

        
        // Added By circus
        public ActionResult SubscribePackage(int packageid, int cartype=0)
        {
            //Check the car type and Pakcage of user may be later
            UserPackageAddOnViewModel packageDetails = new UserPackageAddOnViewModel();
            packageDetails.SubscriptionTypeId = 1;
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



                    packageDetails.AvailableSubscriptionTypes.Add(new SelectListItem()
                    {
                        Text = "Weekly",
                        Value = "1"
                    });
                    packageDetails.AvailableSubscriptionTypes.Add(new SelectListItem()
                    {
                        Text = "Bi-Weekly",
                        Value = "2"
                    });
                    packageDetails.AvailableSubscriptionTypes.Add(new SelectListItem()
                    {
                        Text = "Monthly",
                        Value = "3"
                    });

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
                SubscriptionTypeId=model.SubscriptionTypeId,
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
            string PaymentType = string.Empty;

            PaymentType = Convert.ToString(Request.Form["PaymentMethodName"]);

            //if (package.AcceptAgreement == "true")
            //{
            //    return RedirectToAction("ProcessPaypalPayment", "Paypal", new { userpackageid = UserPackageID });
            //}
            //else
            //    return View();

            if (!string.IsNullOrEmpty(PaymentType))
            {
                if (PaymentType == "Pay using Paypal")
                    return RedirectToAction("ProcessPaypalPayment", "Paypal", new { userpackageid = UserPackageID });
                else if (PaymentType == "Pay by Credit Card")
                    return RedirectToAction("ProcessPayment", new { id = UserPackageID });
            }

            return RedirectToAction("PrepaymentConfirmation", new { id = UserPackageID });
        }

        public ActionResult ProcessPayment(int id)
        {
            if(id>0)
                return RedirectToAction("ProcessPaypalPayment", "Paypal", new { userpackageid = id });

            UserPackageAddOnPaymentInformation userAndAddon = new UserPackageAddOnPaymentInformation();
            userAndAddon.UserPackge = db.UserPackages.Where(a => a.Id == id).FirstOrDefault();
            userAndAddon.Addons = db.UserPackagesAddons.Where(a => a.UserPackageID == id);
            userAndAddon.UserPackageID = id;

            //adding tax
            if (db.Taxes != null && db.Taxes.Count() > 0)
            {
                var taxPercentage = db.Taxes.Select(a => a.TaxPercentage).Sum();
                userAndAddon.UserPackge.TaxAmount = userAndAddon.TaxAmount = userAndAddon.UserPackge.TotalPrice * (taxPercentage / 100);
                userAndAddon.TaxPercentage = taxPercentage;
                db.SaveChanges();
            }

            //CC types
            userAndAddon.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Visa",
                Value = "Visa",
            });
            userAndAddon.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Master card",
                Value = "MasterCard",
            });
            userAndAddon.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Discover",
                Value = "Discover",
            });
            userAndAddon.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Amex",
                Value = "Amex",
            });

            //years            
            userAndAddon.ExpireYears.Add(new SelectListItem
            {
                Text = "Year",
                Value = "",
            });
            for (int i = 0; i < 25; i++)
            {
                string year = Convert.ToString(DateTime.Now.Year + i);
                userAndAddon.ExpireYears.Add(new SelectListItem
                {
                    Text = year,
                    Value = year,
                });
            }

            //months
            //months
            userAndAddon.ExpireMonths.Add(new SelectListItem
            {
                Text = "Month",
                Value = "",
            });
            for (int i = 1; i <= 12; i++)
            {
                string text = (i < 10) ? "0" + i : i.ToString();
                userAndAddon.ExpireMonths.Add(new SelectListItem
                {
                    Text = text,
                    Value = i.ToString(),
                });
            }

            return View(userAndAddon);
        }

        [HttpPost]
        public ActionResult ProcessPayment(UserPackageAddOnPaymentInformation model)
        {
            if (model.UserPackageID > 0)
                return RedirectToAction("ProcessPaypalPayment", "Paypal", new { userpackageid = model.UserPackageID });

            var userid = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                var userPackage = db.UserPackages.Where(a => a.Id == model.UserPackageID).FirstOrDefault();
                var finalPrice = userPackage.TotalPrice + userPackage.TaxAmount + userPackage.TipAmount;

                IPaymentMethod _paymentMethod=new PayPalDirectPaymentProcessor();
                ProcessPaymentRequest processPaymentRequest = new ProcessPaymentRequest();
                processPaymentRequest.OrderTotal = finalPrice;

                processPaymentRequest.CreditCardName = model.NameOnCard;
                processPaymentRequest.CreditCardNumber = model.CardNumber;
                processPaymentRequest.CreditCardCvv2 = model.CardSecurityCode;
                processPaymentRequest.CreditCardExpireMonth = model.CardExpiryMonth;
                processPaymentRequest.CreditCardExpireYear = model.CardExpiryYear;

                processPaymentRequest.CustomerId =  User.Identity.GetUserId(); ;
                if (processPaymentRequest.OrderGuid == Guid.Empty)
                    processPaymentRequest.OrderGuid = Guid.NewGuid();
                try
                {
                    ProcessPaymentResult processPaymentResult = null;
                    processPaymentResult = _paymentMethod.ProcessPayment(processPaymentRequest);


                    if (processPaymentResult.Success)
                    {

                        PayPalLog log = new PayPalLog()
                        {
                            ACK = "SUCCESS",
                            ApiSatus = string.Empty,
                            BillingAggrementID = processPaymentResult.CaptureTransactionId,
                            CorrelationID = string.Empty,
                            ECToken = string.Empty,
                            ResponseError = string.Empty,
                            ResponseRedirectURL = string.Empty,
                            ServerDate = DateTime.Now,
                            TimeStamp = DateTime.Now.ToString(),
                            UserId = userid,
                            SubscriptionID = model.UserPackageID
                        };
                        db.PayPalLogs.Add(log);
                        db.SaveChanges();

                        //Save USer Logs
                        
                        UserTransaction _transaction = new UserTransaction()
                        {
                            Userid = userPackage.UserId,
                            PaypalId = processPaymentResult.CaptureTransactionId,
                            TransactionDate = DateTime.Now,
                            Amount = finalPrice,
                            PackageId = userPackage.Id,
                            Details = "No Details",
                            BillingAggrementID = processPaymentResult.CaptureTransactionId

                        };
                        db.UserTransactions.Add(_transaction);
                        db.SaveChanges();
                        //Logic to disable autorenew if Billingagreement is null



                        //Logic to update Transaction status

                        if (userPackage != null)
                        {

                            string message = string.Empty;
                            string responseString = string.Empty;
                            var package = db.Packages.FirstOrDefault(a => a.PackageId == userPackage.PackageId);
                                                    
                           

                            PaypalAutoPayment paypalAutoPayment = new PaypalAutoPayment();
                            paypalAutoPayment.UserPackageID = userPackage.Id;
                            paypalAutoPayment.UserID = userPackage.UserId;
                            paypalAutoPayment.IsPaid = true;
                            paypalAutoPayment.GrossAmount = String.Format("{0:0.00}", finalPrice); //Convert.ToString(finalPrice);
                            paypalAutoPayment.PaymentStatus = "COMPLETED";
                            paypalAutoPayment.PaymentDate = DateTime.Now.ToString();
                            paypalAutoPayment.TrasactionID = processPaymentResult.CaptureTransactionId;
                            paypalAutoPayment.BillingAggrementID = processPaymentResult.CaptureTransactionId;
                            paypalAutoPayment.TransactionDate = DateTime.Now;

                            DateTime currentDate = DateTime.Now;
                            DateTime serviceDate = currentDate;
                            if (userPackage.ServiceDay == currentDate.DayOfWeek.ToString())
                                serviceDate = currentDate.AddDays(7);
                            else
                            {
                                DateTime nextServiceDate;
                                for (int i = 1; i <= 6; i++)
                                {
                                    if (i == 1)
                                    {
                                        nextServiceDate = currentDate.AddDays(i);
                                        if (userPackage.ServiceDay == nextServiceDate.DayOfWeek.ToString())
                                        {
                                            if (userPackage.SubscriptionTypeId == 1)
                                                serviceDate = nextServiceDate.AddDays(7);
                                            else if (userPackage.SubscriptionTypeId == 2)
                                                serviceDate = nextServiceDate.AddDays(14);
                                            else if (userPackage.SubscriptionTypeId == 3)
                                                serviceDate = nextServiceDate.AddDays(28);
                                            else
                                                serviceDate = nextServiceDate.AddDays(7);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        nextServiceDate = currentDate.AddDays(i);
                                        if (userPackage.ServiceDay == nextServiceDate.DayOfWeek.ToString())
                                        {
                                            serviceDate = currentDate.AddDays(i);
                                            break;
                                        }
                                    }
                                }
                            }

                            paypalAutoPayment.ServiceDate = serviceDate;
                            paypalAutoPayment.CreatedOn = DateTime.Now;
                            db.PaypalAutoPayments.Add(paypalAutoPayment);
                            db.SaveChanges();

                            if (paypalAutoPayment.IsPaid)
                            {
                                userPackage.PaymentRecieved = true;
                                userPackage.IsActive = true;
                                userPackage.NextServiceDate = serviceDate;
                                userPackage.PaymentMethodName = "credit card";
                                db.Entry(userPackage).State = EntityState.Modified;
                                db.SaveChanges();
                            }


                        }


                        Session["NewServiceCarTypeId"] = null;
                        Session["SelectedCar"] = null;
                        Session["NewServiceGarageId"] = null;

                        var userInfo = userPackage.AspNetUser;

                        // Send Notification Mail Admin for buy new subscrition.
                        _workflowMessageService.SendNewSubscriptionNotificationToAdmin(userInfo.FirstName + " " + userInfo.LastName, userInfo.UserName, userPackage.Package.Package_Name, userPackage.SubscribedDate.ToString(), userPackage.CarUser.Garage.Garage_Name);

                        return RedirectToRoute("Completed", new { id = model.UserPackageID });
                       

                    }

                    foreach (var error in processPaymentResult.Errors)
                        model.Warnings.Add(error);
                }
                catch (Exception exc)
                {

                    model.Warnings.Add(exc.Message);
                }

            }

            model.UserPackge = db.UserPackages.Where(a => a.Id == model.UserPackageID).FirstOrDefault();
            model.Addons = db.UserPackagesAddons.Where(a => a.UserPackageID == model.UserPackageID);           

            //adding tax
            if (db.Taxes != null && db.Taxes.Count() > 0)
            {
                var taxPercentage = db.Taxes.Select(a => a.TaxPercentage).Sum();
                model.UserPackge.TaxAmount = model.TaxAmount = model.UserPackge.TotalPrice * (taxPercentage / 100);
                model.TaxPercentage = taxPercentage;
                db.SaveChanges();
            }

            //CC types
            model.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Visa",
                Value = "Visa",
            });
            model.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Master card",
                Value = "MasterCard",
            });
            model.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Discover",
                Value = "Discover",
            });
            model.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Amex",
                Value = "Amex",
            });

            //years            
            model.ExpireYears.Add(new SelectListItem
            {
                Text = "Year",
                Value = "",
            });
            for (int i = 0; i < 25; i++)
            {
                string year = Convert.ToString(DateTime.Now.Year + i);
                model.ExpireYears.Add(new SelectListItem
                {
                    Text = year,
                    Value = year,
                });
            }

            //months
            //months
            model.ExpireMonths.Add(new SelectListItem
            {
                Text = "Month",
                Value = "",
            });
            for (int i = 1; i <= 12; i++)
            {
                string text = (i < 10) ? "0" + i : i.ToString();
                model.ExpireMonths.Add(new SelectListItem
                {
                    Text = text,
                    Value = i.ToString(),
                });
            }

            return View(model);

           
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