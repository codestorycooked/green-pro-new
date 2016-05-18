using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.AdminInterface.ViewModels
{
    public class PaypalAutoPaymentsViewModel
    {
        public int Id { get; set; }
        public string TrasactionID { get; set; }
        public string ReferenceID { get; set; }
        public string PaymentStatus { get; set; }
        public string PendingReason { get; set; }
        public string PaymentDate { get; set; }
        public string GrossAmount { get; set; }
        public Nullable<int> UserPackageID { get; set; }
        public string UserID { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<System.DateTime> ServiceDate { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}