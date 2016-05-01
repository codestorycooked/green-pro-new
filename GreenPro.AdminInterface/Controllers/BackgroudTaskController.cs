using GreenPro.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenPro.AdminInterface.Controllers
{
    public class BackgroudTaskController : Controller
    {
        // GET: BackgroudTask
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestAutoPay()
        {
            string responsePayment = string.Empty;
            BillGenerator obj = new BillGenerator();
            responsePayment=obj.TestAutoPayment();
            string text = "Paypal Call: "+DateTime.Now.ToString();
            text += Environment.NewLine+Environment.NewLine+ "responseFrom Paypal: " + responsePayment;
            string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm", CultureInfo.InvariantCulture)+".txt";
            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/" + fileName), text);

            return Content("");
        }
    }
}