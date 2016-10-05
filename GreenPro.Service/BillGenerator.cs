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
                    //decimal finalPrice = userPackage.TotalPrice + userPackage.TaxAmount + userPackage.TipAmount; // Comment By Nitendra 28 SEP 2016

                    decimal AddOnsPrice = 0M;
                    AddOnsPrice = userPackage.UserPackagesAddons.Sum(s => s.ActualPrice);

                    decimal finalPrice = userPackage.ActualPrice + AddOnsPrice+userPackage.TaxAmount;

                    if (userPackage != null && (car != null && (car.AutoRenewal)))
                    {
                        string message = string.Empty;
                       
                        string OriResponsePaypal = string.Empty;
                        DoReferenceTrasaction paypal = new DoReferenceTrasaction();
                        var response = paypal.DoTransaction(BillingAggrementID, package.Package_Description, package.Package_Name, (double)finalPrice, "Weekly Renewal", out OriResponsePaypal);
                        
                        responseString = JsonConvert.SerializeObject(response);

                        responseString = responseString + " : Paypal Response: " + OriResponsePaypal;


                        PaypalAutoPayment paypalAutoPayment = new PaypalAutoPayment();
                        paypalAutoPayment.UserPackageID = userPackage.Id;
                        paypalAutoPayment.UserID = userPackage.UserId;
                        paypalAutoPayment.IsPaid =response.PaymentStatus=="COMPLETED"? true:false;
                        paypalAutoPayment.GrossAmount = String.Format("{0:0.00}", finalPrice); //Convert.ToString(finalPrice);
                        paypalAutoPayment.PaymentStatus = response.PaymentStatus;
                        paypalAutoPayment.PaymentDate = response.PaymentDate;
                        paypalAutoPayment.TrasactionID = response.TransactionID;
                        paypalAutoPayment.BillingAggrementID = BillingAggrementID;
                        paypalAutoPayment.TransactionDate = DateTime.Now;

                        paypalAutoPayment.ServiceDate = CarServiceDate;
                        paypalAutoPayment.CreatedOn = DateTime.Now;

                        db.PaypalAutoPayments.Add(paypalAutoPayment);
                        db.SaveChanges();

                        if (paypalAutoPayment.IsPaid)
                        {
                            JobDetail.IsPaid = true;
                            db.SaveChanges();

                            UserTransaction _transaction = new UserTransaction()
                            {
                                Userid = userPackage.UserId,
                                PaypalId = "",
                                TransactionDate = DateTime.Now,
                                Amount = finalPrice,
                                PackageId = userPackage.Id,
                                Details = "No Details",
                                BillingAggrementID = BillingAggrementID,
                                TrasactionID = paypalAutoPayment.TrasactionID,

                            };
                            db.UserTransactions.Add(_transaction);
                            db.SaveChanges();
                        }


                       
                        

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
