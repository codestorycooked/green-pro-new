using GreenPro.Data;
using GreenPro.WebClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Core;

namespace GreenPro.WebClient.Controllers
{
    public class AdhocCarServiceController : Controller
    {
        GreenPro.Data.GreenProDbEntities db;
        public AdhocCarServiceController()
        {
            db = new Data.GreenProDbEntities();

        }
        // GET: AdhocCarService
        public ActionResult Index(int id)
        {
            Session["LinkCarId"] = id;
            var package = new PackagesServiceViewModel();
            package.CarTypes = db.CarTypes.ToList();
            if (Session["SelectedCar"] != null)
            {
                var car = Session["SelectedCar"] as CarUser;
                //var l = db.CarTypes.Where(a => a.Id == car.Type).ToList();
                var l = db.CarTypes.ToList();
                package.CarTypes = l;
            }
            package.Packages = null;
            return View(package);
        }

        [HttpPost]
        public ActionResult IndexPost(int id)
        {
            Session["NewServiceCarTypeId"] = id;
            var ids = Convert.ToInt16(id);
            //var packages = db.Packages.Include(p => p.Package_Services).Where(a => a.CarTypeId == ids);
            var packages = db.Packages.Include(p => p.Package_Services);
            var packageViewModel = new PackagesServiceViewModel();
            packageViewModel.CarTypes = db.CarTypes.ToList();
            packageViewModel.Packages = packages;
            var package_services = new Package_Services();

            return View("_Packages", packageViewModel);
        }

        public ActionResult SubscribePackage(int packageid, int cartype)
        {
            //Check the car type and Pakcage of user may be later
            AdhocUserPackageAddOnViewModel packageDetails = new AdhocUserPackageAddOnViewModel();
            if (Session["LinkCarId"] != null)
            {
                var linkCarId = Convert.ToInt32(Session["LinkCarId"]);
                packageDetails.LinkCarId = linkCarId;
                if (Session["SelectedCar"] != null)
                {
                    var car = Session["SelectedCar"] as CarUser;

                    packageDetails.SelectedCar = car.CarId.ToString();
                    ViewBag.SelectedCarName = car.DisplayName;
                }


                var userid = User.Identity.GetUserId();

                //var carUserList = db.CarUsers.Where(a => a.UserId == userid && a.CarType.Id == cartype);
                var carUserList = db.CarUsers.Where(a => a.UserId == userid);
                var packageCarUserList = db.UserPackages.Where(a => a.UserId == userid).Select(a => a.CarId).ToList();
                var list = new List<CarUser>();
                foreach (var carUser in carUserList)
                {
                    if (!packageCarUserList.Contains(carUser.CarId))
                    {
                        list.Add(carUser);
                    }
                }

                packageDetails.UserCars = list;
                //int latestCar = packageDetails.UserCars.ToList().OrderByDescending(a => a.CarId).Select(a => a.CarId).FirstOrDefault();
                //ViewBag.LatestCar = latestCar;
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
                    //packageDetails.Services = db.Services.ToList();
                    foreach (var item in db.Services.ToList())
                    {
                        bool flag = true;
                        foreach (var subItem in package.Package_Services)
                        {
                            if (item.ServiceID == subItem.ServiceID)
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            if (packageDetails.Services == null)
                                packageDetails.Services = new List<Service>();
                            (packageDetails.Services as List<Service>).Add(item);
                        }
                    }
                    //packageDetails.Services = (from s in package.Package_Services
                    //                           join from a in db.Services
                    //                           where s.ServiceID != a.ServiceID
                    //                           select a).ToList();

                }
            }
            return View(packageDetails);
        }

        [HttpPost]
        public ActionResult SubscribePackage(AdhocUserPackageAddOnViewModel model, string[] services)
        {
            var package = db.Packages.Where(a => a.PackageId == model.PackageID).FirstOrDefault();
            if (package == null)
                return View();
            var userId = User.Identity.GetUserId();
            //var carList = db.CarUsers.Where(a => a.UserId == userId).FirstOrDefault();
            //if (carList == null)
            //    return View();
            int existing_user_package_id = 0;
            if (Session["LinkCarId"] != null)
            {
                var linkCarId = Convert.ToInt32(Session["LinkCarId"]);
                existing_user_package_id = db.UserPackages.Where(a => a.CarId == linkCarId && a.PaymentRecieved == true).OrderByDescending(a => a.CreatedDt).Select(a => a.Id).FirstOrDefault();
            }
            else
            {

                return View();
            }
            int caridfromuserpage = Convert.ToInt32(model.SelectedCar);
            if (Session["SelectedCar"] != null)
            {
                var car = Session["SelectedCar"] as CarUser;
                caridfromuserpage = car.CarId;
            }

            //Save User Packges
            var savingEntity = new AdhocUserPackage
            {
                UserId = userId,
                PackageId = package.PackageId,
                CarId = caridfromuserpage,
                SubscribedDate = DateTime.Now,
                ActualPrice = package.Package_Price,
                TotalPrice = model.Packages.Package_Price,
                PriceWithAddOns = model.Packages.Package_Price - package.Package_Price,
                DiscountPrice = 0,
                CreatedDt = DateTime.Now,
                ExistingPackageId = existing_user_package_id,
                PaymentRecieved = false
            };
            db.AdhocUserPackages.Add(savingEntity);

            db.SaveChanges();
            //Save uer addson
            if (services != null)
            {
                //Save user addons
                AdhocUserPackagesAddon addon = new AdhocUserPackagesAddon();
                foreach (var item in services)
                {
                    addon.ServiceID = Convert.ToInt32(item);
                    addon.UserPackageID = savingEntity.Id;
                    addon.ActualPrice = 0;
                    addon.DiscountPrice = 0;
                    addon.CreatedDt = DateTime.Now;
                    db.AdhocUserPackagesAddons.Add(addon);
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
            //if (model.AutoRenewalSubscription)
            //{
            //    if (Session["SelectedCar"] != null)
            //    {
            //        var carTemp = Session["SelectedCar"] as CarUser;
            //        var car = db.CarUsers.Where(a => a.CarId == carTemp.CarId).FirstOrDefault();
            //        car.AutoRenewal = model.AutoRenewalSubscription;
            //        db.SaveChanges();
            //    }
            //    else
            //    {
            //        var carId = Convert.ToInt32(model.SelectedCar);
            //        var car = db.CarUsers.Where(a => a.CarId == carId).FirstOrDefault();
            //        car.AutoRenewal = model.AutoRenewalSubscription;
            //        db.SaveChanges();
            //    }
            //}
            return RedirectToAction("PrepaymentConfirmation", new { id = savingEntity.Id });
        }

        public ActionResult PrePaymentConfirmation(int id)
        {
            AdhocUserPackageAddOn userAndAddon = new AdhocUserPackageAddOn();
            userAndAddon.UserPackge = db.AdhocUserPackages.Where(a => a.Id == id).FirstOrDefault();
            userAndAddon.Addons = db.AdhocUserPackagesAddons.Where(a => a.UserPackageID == id);
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
        public ActionResult PrePaymentConfirmation(AdhocUserPackageAddOn package, string UserPackageID)
        {
            if (package.AcceptAgreement == "true")
            {
                var userid = User.Identity.GetUserId();
                var packageid = Convert.ToInt32(UserPackageID);
                //Get Adhoc Package Details
                var packageDetails = db.AdhocUserPackages.Where(a => a.Id == packageid).FirstOrDefault();
                //Check adhoc package get existing package id which is not processed and latest one.

                var finalPrice = packageDetails.TotalPrice + packageDetails.TaxAmount + packageDetails.TipAmount;
                var existingPackageID = packageDetails.ExistingPackageId;
                var paymentInfo = db.PayPalLogs.Where(a => a.UserId == userid && a.ACK == "BillingAgreement").FirstOrDefault();
                //Get Billing Agreement ID
                var billingAgreementID = paymentInfo.BillingAggrementID;
                //Post payment Request to Paypal
                GreenPro.PayPalSystem.DoReferenceTrasaction doRefrenceTransaction = new PayPalSystem.DoReferenceTrasaction();
                string message = string.Empty;
                var response = doRefrenceTransaction.DoTransaction(billingAgreementID, packageDetails.Package.Package_Description, packageDetails.Package.Package_Name, Convert.ToDouble(finalPrice), packageDetails.Package.Package_Description, out message);
                if (response.ApiStatus == "SUCCESS")
                {
                    //redirect to success & Log response in AdhocPayment
                    packageDetails.PaymentRecieved = true;
                    db.SaveChanges();
                    return RedirectToAction("Success", new { id = UserPackageID });
                }
                else
                {

                    //Redirect to Failure 
                    return View("Failure", new { id = UserPackageID });
                }

                //return RedirectToAction("ProcessPaypalPayment", "Paypal", new { userpackageid = UserPackageID });
            }
            else
                return View();
        }

        public ActionResult Success(int id)
        {
            /*TODO: 
             * 1) Log Paypal Payments
             * 2) SEnd Mail
          
             */
            return View();

        }

        public ActionResult Failure(int id)
        {
            /*TODO
             * Delete package from adhocusers unesseary entry
             * send failure mail
             * 
             */

            return View();



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
                catch (Exception ex)
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