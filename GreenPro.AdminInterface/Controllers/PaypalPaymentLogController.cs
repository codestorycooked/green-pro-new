using GreenPro.AdminInterface.Models;
using GreenPro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenPro.AdminInterface.Controllers
{
    public class PaypalPaymentLogController : Controller
    {
        private GreenProDbEntities _db;
        public PaypalPaymentLogController()
        {
            _db = new GreenProDbEntities();
           
        }

        PaypalAutoPaymentModel PreParePaypalAutoPaymentModelForList(PaypalAutoPayment paypalAutoPayment)
        {
            PaypalAutoPaymentModel model = new PaypalAutoPaymentModel();
            model.Id = paypalAutoPayment.Id;
            model.GrossAmount = paypalAutoPayment.GrossAmount;
            model.BillingAggrementID = paypalAutoPayment.BillingAggrementID;
            model.ReferenceID = paypalAutoPayment.ReferenceID;
            model.TransactionDate = paypalAutoPayment.TransactionDate;
            model.TrasactionID = paypalAutoPayment.TrasactionID;
            model.UserID = paypalAutoPayment.UserID;
            model.CustomerName = paypalAutoPayment.AspNetUser.Email;

            model.UserPackageID = paypalAutoPayment.UserPackageID;
            model.UserPackageName = paypalAutoPayment.UserPackage.Package.Package_Name;

            model.ServiceDate = paypalAutoPayment.ServiceDate;
            model.PaymentStatus = paypalAutoPayment.PaymentStatus;
            model.PendingReason = paypalAutoPayment.PendingReason;
            model.CreatedOn = paypalAutoPayment.CreatedOn;
            return  model;
        }

        // GET: PaypalPaymentLog
        public ActionResult Index(PaypalAutoPaymentSearchList model)
        {

            if (model == null)
                model = new PaypalAutoPaymentSearchList();            

            var paypalAutoPayments = _db.PaypalAutoPayments.ToList();

            // Teams
            var gargeList = _db.Garages.ToList();

            foreach (var item in gargeList)
                model.AvailableGarages.Add(new SelectListItem() { Text=item.Garage_Name, Value=item.GarageId.ToString() });

            model.AvailableGarages.Add(new SelectListItem() { Text = "All", Value = "0" });

            if (!string.IsNullOrEmpty(model.customerEmail))
                paypalAutoPayments = paypalAutoPayments.Where(u => u.AspNetUser.Email.Contains(model.customerEmail)).ToList();

            if (model.garageId > 0)
                paypalAutoPayments = paypalAutoPayments.Where(u => u.UserPackage.CarUser.GarageId == model.garageId).ToList();

            model.PaypalAutoPaymentsList = paypalAutoPayments.Select(PreParePaypalAutoPaymentModelForList).OrderByDescending(o=>o.CreatedOn).ToList();


            return View(model);
        }
    }
}