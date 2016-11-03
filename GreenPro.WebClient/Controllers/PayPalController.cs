using GreenPro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenPro.PayPalSystem;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Globalization;
namespace GreenPro.WebClient.Controllers
{
    public class PayPalController : Controller
    {
        private GreenProDbEntities db;

        WorkflowMessageService _workflowMessageService;
        public PayPalController()
        {
            db = new GreenProDbEntities();
            _workflowMessageService = new WorkflowMessageService();
        }

        // GET: PayPal
        public ActionResult ProcessPaypalPayment(string userpackageID)
        {
            try
            {
                if (userpackageID == string.Empty)
                {
                    return View("Failure");
                }
                else
                {
                    int userpackage = Convert.ToInt32(userpackageID);
                    var payment = db.UserPackages.Where(m => m.Id == userpackage).FirstOrDefault();
                    if (payment == null)
                    {
                        return View("Failure");
                    }
                    var finalPrice = payment.TotalPrice + payment.TaxAmount + payment.TipAmount;
                    SetExpressCheckOut express = new SetExpressCheckOut();
                    PayPalSystem.Models.PaypalResponse response = new PayPalSystem.Models.PaypalResponse();
                    response = express.SetExpressCheckout(userpackageID,payment.AspNetUser.Email, "Car Cleaning Services", "Accept to Pay weekly", null, 
                        "GreenPro", Convert.ToDouble(finalPrice), payment.Package.Package_Name, Convert.ToDouble(finalPrice), payment.Package.Package_Description);
                    string responseString = string.Empty;
                    responseString = JsonConvert.SerializeObject(response);

                    string text = "Paypal ProcessPaypalPayment ActionResult: " + DateTime.Now.ToString();
                    text += Environment.NewLine + Environment.NewLine + "responseFrom Paypal: " + responseString;
                    string fileName = DateTime.Now.ToString("Response_"+userpackageID+"_"+"yyyy-MM-dd-HH-mm", CultureInfo.InvariantCulture) + ".txt";
                    System.IO.File.WriteAllText(Server.MapPath("~/App_Data/" + fileName), text);

                   
                    PayPalLog log = new PayPalLog()
                    {
                        UserId = User.Identity.GetUserId(),
                        ACK = "Express",
                        ApiSatus = response.ApiStatus,
                        BillingAggrementID = (response.BillingAgreementID == null) ? string.Empty : response.BillingAgreementID,
                        CorrelationID = response.CorrelationID,
                        ECToken = response.ECToken,
                        ResponseError = (response.ResponseError == null) ? string.Empty : response.ResponseError.ToString(),
                        ResponseRedirectURL = (response.ResponseRedirectURL == null) ? string.Empty : response.ResponseRedirectURL,
                        ServerDate = DateTime.Now,
                        TimeStamp = response.Timestamp,
                        SubscriptionID = payment.Id
                    };
                    db.PayPalLogs.Add(log);
                    db.SaveChanges();

                    if (response.ResponseRedirectURL != null)
                    {
                        Response.Redirect(response.ResponseRedirectURL);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
            return View();
        }

        public ActionResult Failure()
        {
            return View();
        }

        public ActionResult CancelTransaction()
        {

            return View();
        }

        public ActionResult Success()
        {

            SetExpressCheckOut express = new SetExpressCheckOut();
            PayPalSystem.Models.PaypalResponse response = new PayPalSystem.Models.PaypalResponse();
            response.ECToken = Request.QueryString["token"];
            var token = response.ECToken;
            var user = db.PayPalLogs.Where(a => a.ECToken == token).FirstOrDefault();
            response = express.CreateBillingAgreement(response);

           
            PayPalLog log = new PayPalLog()
            {
                ACK = "BillingAgreement",
                ApiSatus = response.ApiStatus,
                BillingAggrementID = (response.BillingAgreementID == null) ? string.Empty : response.BillingAgreementID,
                CorrelationID = response.CorrelationID,
                ECToken = token,
                ResponseError = (response.ResponseError == null) ? string.Empty : response.ResponseError.ToString(),
                ResponseRedirectURL = (response.ResponseRedirectURL == null) ? string.Empty : response.ResponseRedirectURL,
                ServerDate = DateTime.Now,
                TimeStamp = response.Timestamp,
                UserId = user.UserId,
                SubscriptionID = user.SubscriptionID
            };
            db.PayPalLogs.Add(log);
            db.SaveChanges();

            //Save USer Logs
            var finalPrice = user.UserPackage.TotalPrice + user.UserPackage.TaxAmount + user.UserPackage.TipAmount;
            UserTransaction _transaction = new UserTransaction()
            {
                Userid = user.UserId,
                PaypalId = user.ECToken,
                TransactionDate = DateTime.Now,
                Amount = finalPrice,
                PackageId = user.SubscriptionID,
                Details = "No Details",
                BillingAggrementID = log.BillingAggrementID

            };
            db.UserTransactions.Add(_transaction);
            db.SaveChanges();
            //Logic to disable autorenew if Billingagreement is null



            //Logic to update Transaction status
            var userPackages = db.UserPackages.Find(user.SubscriptionID);
            if (userPackages != null)
            {
                DateTime currentDate = DateTime.Now;
                DateTime serviceDate = currentDate;





                if (userPackages.SubscriptionTypeId == 4)
                {
                    serviceDate = userPackages.NextServiceDate.Value;

                    userPackages.PaymentRecieved = true;
                    userPackages.IsActive = true;                    
                    userPackages.PaymentMethodName = "paypal";
                    db.Entry(userPackages).State = EntityState.Modified;
                    db.SaveChanges();

                    ///Added By Sachin 29 SEP 2016
                    var addOnsServices = userPackages.UserPackagesAddons.ToList();
                    foreach (var addOns in addOnsServices)
                    {
                        if (!addOns.NextServiceDate.HasValue)
                        {
                            addOns.NextServiceDate = serviceDate;
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    string message = string.Empty;
                    string responseString = string.Empty;
                    var package = db.Packages.FirstOrDefault(a => a.PackageId == userPackages.PackageId);
                    string OriResponsePaypal = string.Empty;
                    DoReferenceTrasaction paypal = new DoReferenceTrasaction();
                    var doResponse = paypal.DoTransaction(log.BillingAggrementID, package.Package_Description, package.Package_Name, (double)finalPrice, "Weekly Renewal", out OriResponsePaypal);

                    ///Write paypal response in txt file.
                    responseString = JsonConvert.SerializeObject(response);
                    responseString = responseString + " : Paypal Response: " + OriResponsePaypal;
                    string fileName = userPackages.Id + "-" + log.BillingAggrementID + "__" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm", CultureInfo.InvariantCulture) + ".txt";
                    System.IO.File.WriteAllText(Server.MapPath("~/App_Data/" + fileName), responseString);


                    PaypalAutoPayment paypalAutoPayment = new PaypalAutoPayment();
                    paypalAutoPayment.UserPackageID = userPackages.Id;
                    paypalAutoPayment.UserID = userPackages.UserId;
                    paypalAutoPayment.IsPaid = doResponse.PaymentStatus == "COMPLETED" ? true : false;
                    paypalAutoPayment.GrossAmount = String.Format("{0:0.00}", finalPrice); //Convert.ToString(finalPrice);
                    paypalAutoPayment.PaymentStatus = doResponse.PaymentStatus;
                    paypalAutoPayment.PaymentDate = doResponse.PaymentDate;
                    paypalAutoPayment.TrasactionID = doResponse.TransactionID;
                    paypalAutoPayment.BillingAggrementID = log.BillingAggrementID;
                    paypalAutoPayment.TransactionDate = DateTime.Now;



                    if (userPackages.ServiceDay == currentDate.DayOfWeek.ToString())
                        serviceDate = currentDate.AddDays(7);
                    else
                    {
                        DateTime nextServiceDate;
                        for (int i = 1; i <= 6; i++)
                        {
                            if (i == 1)
                            {
                                nextServiceDate = currentDate.AddDays(i);
                                if (userPackages.ServiceDay == nextServiceDate.DayOfWeek.ToString())
                                {
                                    if (userPackages.SubscriptionTypeId == 1)
                                        serviceDate = nextServiceDate.AddDays(7);
                                    else if (userPackages.SubscriptionTypeId == 2)
                                        serviceDate = nextServiceDate.AddDays(14);
                                    else if (userPackages.SubscriptionTypeId == 3)
                                        serviceDate = nextServiceDate.AddDays(28);
                                    else
                                        serviceDate = nextServiceDate.AddDays(7);

                                    break;
                                }
                            }
                            else
                            {
                                nextServiceDate = currentDate.AddDays(i);
                                if (userPackages.ServiceDay == nextServiceDate.DayOfWeek.ToString())
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
                        userPackages.PaymentRecieved = true;
                        userPackages.IsActive = true;
                        userPackages.NextServiceDate = serviceDate;
                        userPackages.PaymentMethodName = "paypal";
                        db.Entry(userPackages).State = EntityState.Modified;
                        db.SaveChanges();

                        ///Added By Sachin 29 SEP 2016
                        var addOnsServices = userPackages.UserPackagesAddons.ToList();
                        foreach (var addOns in addOnsServices)
                        {
                            if (!addOns.NextServiceDate.HasValue)
                            {
                                addOns.NextServiceDate = serviceDate;
                                db.SaveChanges();
                            }
                        }

                    }

                }


                


            }


            Session["NewServiceCarTypeId"] = null;
            Session["SelectedCar"] = null;
            Session["NewServiceGarageId"] = null;

            var userInfo=userPackages.AspNetUser;

            // Send Notification Mail Admin for buy new subscrition.
            _workflowMessageService.SendNewSubscriptionNotificationToAdmin(userInfo.FirstName + " " + userInfo.LastName, userInfo.UserName, userPackages.Package.Package_Name, userPackages.SubscribedDate.ToString(), userPackages.CarUser.Garage.Garage_Name);

            return View();
        }


        public ActionResult Completed(int id)
        {

            return View();
        }
    }
}