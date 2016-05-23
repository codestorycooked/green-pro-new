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

namespace GreenPro.WebClient.Controllers
{
    public class UserPackagesController : Controller
    {
        private GreenProDbEntities db = new GreenProDbEntities();

        // GET: UserPackages
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var userPackages = db.UserPackages.Include(u => u.AspNetUser).Include(u => u.CarUser).Include(u => u.Package).Where(a=>a.UserId==userid && a.PaymentRecieved==true);
            return View(userPackages.ToList());
        }

        // GET: UserPackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPackage userPackage = db.UserPackages.Find(id);
            if (userPackage == null)
            {
                return HttpNotFound();
            }

            UserPackageDetailViewModel userPackageModel = new UserPackageDetailViewModel();
            userPackageModel.Id = userPackage.Id;
            userPackageModel.ActualPrice = userPackage.ActualPrice;
            userPackageModel.TotalPrice = userPackage.TotalPrice;
            userPackageModel.TaxAmount = userPackage.TaxAmount;
            userPackageModel.TipAmount = userPackage.TipAmount;
            userPackageModel.ServiceDay = userPackage.ServiceDay;
            userPackageModel.TimeSlot = userPackage.GaragesTimeingSlotId.HasValue ? userPackage.GargesTimeingSlot.SlotTimeing : "";
            userPackageModel.SubscribedDate = userPackage.SubscribedDate;
            userPackageModel.IsActive = userPackage.IsActive;

            userPackageModel.Package = new PackageDetailViewModel();
            userPackageModel.Package.Package_Name = userPackage.Package.Package_Name;
            userPackageModel.Package.Package_Description = userPackage.Package.Package_Description;


            userPackageModel.Services = new List<CarServiceViewModel>();

            var ServiceList = userPackage.Package.Package_Services.Select(s => s.Service).ToList();
            foreach (var service in ServiceList)
            {
                CarServiceViewModel serviceModel = new CarServiceViewModel();
                serviceModel.Service_Name = service.Service_Name;
                userPackageModel.Services.Add(serviceModel);
            }

            if (userPackage.UserPackagesAddons.Count > 0)
            {
                foreach (var userPackagesAddon in userPackage.UserPackagesAddons)
                {
                    CarServiceViewModel serviceModel = new CarServiceViewModel();
                    serviceModel.Service_Name = userPackagesAddon.Service.Service_Name;
                    serviceModel.IsAddOn = true;
                    userPackageModel.Services.Add(serviceModel);
                }
            }

            /// Prepare Car Model
            /// 
            CarViewModel carModel = new CarViewModel();

            var item = db.CarUsers.Where(c => c.CarId == userPackage.CarId).SingleOrDefault();

            if (item == null)
                return RedirectToAction("Index");

            carModel = new CarViewModel();
            carModel.AutoRenewal = item.AutoRenewal;
            carModel.CarId = item.CarId;
            carModel.Color = item.Color;
            carModel.DisplayName = item.DisplayName;
            carModel.GarageId = item.GarageId;
            carModel.IsDeleted = item.IsDeleted;
            carModel.LicenseNumber = item.LicenseNumber;
            carModel.Make = item.Make;
            carModel.PurchaseYear = item.PurchaseYear;
            //carModel.Type = item.Type; //comment by circus
            carModel.UserId = item.UserId;

            // carModel.CarType = item.CarType.Description; //comment by circus
            if (item.Garage != null)
                carModel.Garage = item.Garage.Garage_Name;

            userPackageModel.CarModel = carModel;


            /// Prepare Payments Historys
            var paypalAutoPaymentList = db.PaypalAutoPayments.Where(p => p.UserPackageID == id && p.IsPaid == true).ToList();
            if (paypalAutoPaymentList.Count > 0)
            {
                foreach (var payment in paypalAutoPaymentList)
                {
                    PaypalAutoPaymentsViewModel paymentViewModel = new PaypalAutoPaymentsViewModel();
                    paymentViewModel.Id = payment.Id;
                    paymentViewModel.ReferenceID = payment.ReferenceID;
                    paymentViewModel.TrasactionID = payment.TrasactionID;
                    paymentViewModel.TransactionDate = payment.TransactionDate;
                    paymentViewModel.GrossAmount = payment.GrossAmount;
                    paymentViewModel.CreatedOn = payment.CreatedOn;
                    paymentViewModel.ServiceDate = payment.ServiceDate;
                    userPackageModel.PaymentHistorys.Add(paymentViewModel);

                }
            }

            return View(userPackageModel);

            
        }

        [HttpPost, ActionName("Details")]
        [FormValueRequired("btnSubscriptionCancel")]
        public ActionResult Details(UserPackageDetailViewModel model)
        {
            UserPackage userPackage = db.UserPackages.Find(model.Id);
            if (userPackage == null)
            {
                return HttpNotFound();
            }

            userPackage.IsActive = false;
            db.SaveChanges();

            return RedirectToAction("Details", new { Id = model.Id });
        }
        // GET: UserPackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPackage userPackage = db.UserPackages.Find(id);
            if (userPackage == null)
            {
                return HttpNotFound();
            }
            return View(userPackage);
        }

        // POST: UserPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserPackage userPackage = db.UserPackages.Find(id);
            db.UserPackages.Remove(userPackage);
            db.SaveChanges();
            return RedirectToAction("Index");
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
