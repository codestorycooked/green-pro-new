using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using GreenPro.Data;
using GreenPro.WebClient.ViewModel;


namespace GreenPro.WebClient.Controllers
{
    public class HomeController : Controller
    {
        WorkflowMessageService _workflowMessageService;
        GreenPro.Data.GreenProDbEntities _db;
        public HomeController()
        {
            _workflowMessageService = new WorkflowMessageService();
            _db = new Data.GreenProDbEntities();
        }
        public ActionResult Index()
        {
            //GreenPro.Utilities.Mail ms = new Utilities.Mail();
            //ms.SendMail("kunal@innoator.com", "kunal@innoator.com","<b>Test<b>", "Test");
            //Paypal
            //GreenPro.PayPalSystem.SetExpressCheckOut express = new PayPalSystem.SetExpressCheckOut();
            //GreenPro.PayPalSystem.Models.PaypalResponse response = new PayPalSystem.Models.PaypalResponse();
            //response = express.SetExpressCheckout("kunal@innoatro.com", "Cleaning Services", "Accept to pay weekly", null, "GreenPro", 1.0, "Basic Plan", 10, "Basic Weeklyplan");
            //GreenPro.Data.GreenProDbEntities _db = new Data.GreenProDbEntities();
            //GreenPro.Data.PayPalLog log = new Data.PayPalLog()
            // {
            //     ACK = "Express",
            //     ApiSatus = response.ApiStatus,
            //     BillingAggrementID = (response.BillingAgreementID == null) ? string.Empty : response.BillingAgreementID,
            //     CorrelationID = response.CorrelationID,
            //     ECToken = response.ECToken,
            //     ResponseError = (response.ResponseError == null) ? string.Empty : response.ResponseError.ToString(),
            //     ResponseRedirectURL = (response.ResponseRedirectURL == null) ? string.Empty : response.ResponseRedirectURL,
            //     ServerDate = DateTime.Now,
            //     TimeStamp = response.Timestamp
            //     //UserId
            // };
            //_db.PayPalLogs.Add(log);
            //_db.SaveChanges();

            //if (response.ResponseRedirectURL != null)
            //{
            //    Response.Redirect(response.ResponseRedirectURL);
            //}

            //// 

            //ViewBag.Token = response.ECToken;

            //GreenPro.PayPalSystem.DoReferenceTrasaction df = new PayPalSystem.DoReferenceTrasaction();
            //GreenPro.PayPalSystem.Models.PayPalTrasactions response = df.DoTransaction("B-5XE96108MA831044F", "Renewal Weekly", "Basic Plan", 1 - .02, "Basic Plan");
            //var res = response;
            return View();
        }

        public ActionResult FeaturedPackage()
        {
            IList<PackageViewModel> packageList = new List<PackageViewModel>();
            var packages = _db.Packages.Include(p => p.Package_Services).Take(3).ToList();

            if (packages.Count > 0)
            {
                foreach (var package in packages)
                {
                    PackageViewModel model = new PackageViewModel();
                    model.PackageId = package.PackageId;
                    model.Package_Name = package.Package_Name;
                    model.Package_Price = package.Package_Price;

                    if (package.Package_Services.Count > 0)
                    {
                        foreach (var service in package.Package_Services)
                        {
                            ServiceViewModel serviceModel = new ServiceViewModel();
                            serviceModel.ServiceId = service.ServiceID;
                            serviceModel.ServiceName = service.Service.Service_Name;

                            model.Services.Add(serviceModel);
                        }
                    }

                    packageList.Add(model);
                }
            }
            return View(packageList);
        }

        public ActionResult About()
        {
            //GreenPro.PayPalSystem.SetExpressCheckOut express = new PayPalSystem.SetExpressCheckOut();
            //GreenPro.PayPalSystem.Models.PaypalResponse response = new PayPalSystem.Models.PaypalResponse();
            //response.ECToken = Request.QueryString["token"];
            //var token = response.ECToken;
            //response = express.CreateBillingAgreement(response);

            //GreenPro.Data.GreenProDbEntities _db = new Data.GreenProDbEntities();
            //GreenPro.Data.PayPalLog log = new Data.PayPalLog()
            //{
            //    ACK = "BillingAgreement",
            //    ApiSatus = response.ApiStatus,
            //    BillingAggrementID = (response.BillingAgreementID == null) ? string.Empty : response.BillingAgreementID,
            //    CorrelationID = response.CorrelationID,
            //    ECToken = token,
            //    ResponseError = (response.ResponseError == null) ? string.Empty : response.ResponseError.ToString(),
            //    ResponseRedirectURL = (response.ResponseRedirectURL == null) ? string.Empty : response.ResponseRedirectURL,
            //    ServerDate = DateTime.Now,
            //    TimeStamp = response.Timestamp
            //    //UserId=
            //};
            //_db.PayPalLogs.Add(log);
            //_db.SaveChanges();

            //ViewBag.Billid = response.BillingAgreementID;


            return View();
        }

        [Route(Name = "About")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route(Name = "Services")]
        public ActionResult Services()
        {
            return View();
        }

        public ActionResult SendTestMail()
        {
            _workflowMessageService.SendTestMail();
            //GreenPro.Utilities.Mail mail = new Utilities.Mail();
            //GreenPro.Utilities.SiteSetting setting = new Utilities.SiteSetting();

            //GreenPro.Utilities.Mail.SendMail(setting.SenderEmail, setting.SenderName, "circussite1@gmail.com", "", "", DateTime.Now.ToString() + "Test Mail Body", DateTime.Now.ToString() + " subject");
            return Content("");
        }
    }
}