using GreenPro.Data;
using GreenPro.Data.Models;
using GreenPro.PayPalSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GreenPro.Service
{
    public class BillGenerator
    {
        //public List<CustomerBillDetails> Generate()
        //{
        //    var today = DateTime.Now;
        //    var weekBackTime = today.AddDays(-7);
        //    var db = new GreenProDbEntities();
        //    var workDoneList = db.WorkDones.Where(a => a.EndTimeStamp >= weekBackTime).ToList();
        //    var list = new List<CustomerBillDetails>();
        //    foreach (var workDone in workDoneList)
        //    {
        //        var user = db.AspNetUsers.Where(a => a.Id == workDone.UserId).FirstOrDefault();
        //        var p = db.Packages.Where(a => a.PackageId == workDone.PackageId).FirstOrDefault();
        //        var package = db.UserPackages.Where(a => a.PackageId == workDone.PackageId).FirstOrDefault();
        //        var car = db.CarUsers.Where(a => a.UserId == workDone.UserId && a.CarId == package.CarId).FirstOrDefault();
        //        var services = (from a in db.Package_Services
        //                        from b in db.UserPackagesAddons
        //                        from c in db.Services
        //                        where a.PackageID == package.PackageId && a.ServiceID == c.ServiceID && b.UserPackageID == package.Id && b.ServiceID == c.ServiceID
        //                        select c).ToList();
        //        var amt = services.Sum(a => a.Service_Price);
        //        var custBillDetails = new CustomerBillDetails { Customer = user, PackageName = p, CarDetails = car, ServiceList = services, Amount = amt };
        //        list.Add(custBillDetails);
        //    }
        //    return list;
        //}

        public void AutoPay()
        {
            var db = new GreenProDbEntities();
            DoReferenceTrasaction paypal = new DoReferenceTrasaction();
            //get customers whose subscription is valid and auto renewal is on
            //get customers whose subscription is valid and auto renewal is off
            var lastTransactionDate = DateTime.Now.AddDays(-7);
            var monthBackDate = DateTime.Now.AddDays(-30);
            var list = (from a in db.PayPalLogs
                        where !string.IsNullOrEmpty(a.BillingAggrementID) && a.ServerDate <= lastTransactionDate
                        orderby a.ServerDate
                        select a).ToList();
            //pay pal logic
            foreach (var item in list)
            {
                var userPackage = db.UserPackages.FirstOrDefault(a => a.Id == item.SubscriptionID);
                var car = db.CarUsers.FirstOrDefault(a => a.CarId == userPackage.CarId);
                var package = db.Packages.FirstOrDefault(a => a.PackageId == userPackage.PackageId);


                if (userPackage != null && (car != null && (car.AutoRenewal || (userPackage.SubscribedDate.AddDays(30) >= DateTime.Now))))
                {
                    string message = string.Empty;
                    var response = paypal.DoTransaction(item.BillingAggrementID, package.Package_Description, package.Package_Name, (double)userPackage.TotalPrice, "Weekly Renewal", out message);

                    //PayPalLog log = new PayPalLog()
                    //{
                    //    UserId = item.UserId,
                    //    ACK = "Express",
                    //    ApiSatus = response.ApiStatus,
                    //    BillingAggrementID = (response.BillingAgreementID == null) ? string.Empty : response.BillingAgreementID,
                    //    CorrelationID = response.CorrelationID,
                    //    ECToken = response.ECToken,
                    //    ResponseError = (response.ResponseError == null) ? string.Empty : response.ResponseError.ToString(),
                    //    ResponseRedirectURL = (response.ResponseRedirectURL == null) ? string.Empty : response.ResponseRedirectURL,
                    //    ServerDate = DateTime.Now,
                    //    TimeStamp = response.Timestamp,
                    //    SubscriptionID = payment.Id
                    //};
                    //db.PayPalLogs.Add(log);

                }
            }
        }


        public string TakePaymentFromPaypal(int UserPackageId, string BillingAggrementID, DateTime CarServiceDate, int JobId)
        {
            string responseString = string.Empty;
            var db = new GreenProDbEntities();
                if (UserPackageId > 0)
                {

                    var userPackage = db.UserPackages.FirstOrDefault(a => a.Id == UserPackageId);
                    var car = db.CarUsers.FirstOrDefault(a => a.CarId == userPackage.CarId);
                    var package = db.Packages.FirstOrDefault(a => a.PackageId == userPackage.PackageId);
                    var JobDetail = db.Garage_CarDaySetting.Where(i => i.Id == JobId).SingleOrDefault();

                    if (userPackage != null && (car != null && (car.AutoRenewal)))
                    {
                        string message = string.Empty;
                        //var response = paypal.DoTransaction(item.BillingAggrementID, package.Package_Description, package.Package_Name, (double)userPackage.TotalPrice, "Weekly Renewal", out message);

                        string OriResponsePaypal = string.Empty;
                        DoReferenceTrasaction paypal = new DoReferenceTrasaction();
                        var response = paypal.DoTransaction(BillingAggrementID, package.Package_Description, package.Package_Name, (double)userPackage.TotalPrice, "Weekly Renewal", out OriResponsePaypal);
                        
                        responseString = JsonConvert.SerializeObject(response);

                        responseString = responseString + " : Paypal Response: " + OriResponsePaypal;


                        PaypalAutoPayment paypalAutoPayment = new PaypalAutoPayment();
                        paypalAutoPayment.UserPackageID = userPackage.Id;
                        paypalAutoPayment.UserID = userPackage.UserId;
                        paypalAutoPayment.IsPaid =response.PaymentStatus=="COMPLETED"? true:false;
                        paypalAutoPayment.GrossAmount = Convert.ToString(userPackage.TotalPrice);
                        paypalAutoPayment.PaymentStatus = response.PaymentStatus;
                        paypalAutoPayment.PaymentDate = response.PaymentDate;
                        paypalAutoPayment.TrasactionID = response.TransactionID;
                        paypalAutoPayment.TransactionDate = DateTime.Now;

                        paypalAutoPayment.ServiceDate = CarServiceDate;
                        paypalAutoPayment.CreatedOn = DateTime.Now;

                        db.PaypalAutoPayments.Add(paypalAutoPayment);
                        db.SaveChanges();

                        if (paypalAutoPayment.IsPaid)
                        {
                            JobDetail.IsPaid = true;
                            db.SaveChanges();
                        }

                        //PayPalLog log = new PayPalLog()
                        //{
                        //    UserId = item.UserId,
                        //    ACK = "Express",
                        //    ApiSatus = response.ApiStatus,
                        //    BillingAggrementID = (response.BillingAgreementID == null) ? string.Empty : response.BillingAgreementID,
                        //    CorrelationID = response.CorrelationID,
                        //    ECToken = response.ECToken,
                        //    ResponseError = (response.ResponseError == null) ? string.Empty : response.ResponseError.ToString(),
                        //    ResponseRedirectURL = (response.ResponseRedirectURL == null) ? string.Empty : response.ResponseRedirectURL,
                        //    ServerDate = DateTime.Now,
                        //    TimeStamp = response.Timestamp,
                        //    SubscriptionID = payment.Id
                        //};
                        //db.PayPalLogs.Add(log);

                    }

                    
                }

                return responseString;
            
        }

        public string TestAutoPayment()
        {
            string OriResponsePaypal = string.Empty;
            DoReferenceTrasaction paypal = new DoReferenceTrasaction();
            var response = paypal.DoTransaction("B-9MJ87472989329538", "Economy Description ", "Economy", (double)61M, "Economy Subscription Weekly Renewal", out OriResponsePaypal);
            string responseString = string.Empty;
            responseString = JsonConvert.SerializeObject(response);

            responseString = responseString+ " : Paypal Response: "+OriResponsePaypal;

            return responseString;

        }
    }
}
