using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GreenPro.AdminInterface.Models
{
    public class PaypalAutoPaymentModel
    {
        public int Id { get; set; }
        public string TrasactionID { get; set; }
        public string ReferenceID { get; set; }
        public string PaymentStatus { get; set; }
        public string PendingReason { get; set; }
        public string PaymentDate { get; set; }
        public string GrossAmount { get; set; }
        public Nullable<int> UserPackageID { get; set; }
        public string UserPackageName { get; set; }
        public string UserID { get; set; }
        public string CustomerName { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<System.DateTime> ServiceDate { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string BillingAggrementID { get; set; }
    }

    public partial class PaypalAutoPaymentSearchList 
    {

        public int garageId { get; set; }
        public IList<SelectListItem> AvailableGarages { get; set; }
        public string customerEmail { get; set; }
        

        public PaypalAutoPaymentSearchList()
        {
            PaypalAutoPaymentsList = new List<PaypalAutoPaymentModel>();
            AvailableGarages = new List<SelectListItem>();

        }

        public  IList<PaypalAutoPaymentModel> PaypalAutoPaymentsList { get; set; }
    }
}