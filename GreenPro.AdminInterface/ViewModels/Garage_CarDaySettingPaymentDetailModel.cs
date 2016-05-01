using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.AdminInterface.ViewModels
{
    // added by sachin
    public class Garage_CarDaySettingPaymentDetailModel
    {
        public int UserPackageID { get; set; } 
        public int JobId { get; set; }
        public int CarId { get; set; }
        public DateTime CarServiceDate { get; set; }
        public bool IsPaid { get; set; }
        public int LogId { get; set; }
        public string BillingAggrementID { get; set; }
        public string ECToken { get; set; }
        public string CorrelationID { get; set; }
    }
}