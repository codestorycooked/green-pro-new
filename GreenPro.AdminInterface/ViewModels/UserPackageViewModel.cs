using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.AdminInterface.ViewModels
{
    public class UserPackageViewModel
    {
        public UserPackageViewModel()
        {
            Services=new List<CarServiceViewModel>();
            PaymentHistorys = new List<PaypalAutoPaymentsViewModel>();
        }
    
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CustomerName { get; set; }
        public int PackageId { get; set; }
        public int CarId { get; set; }
        public System.DateTime SubscribedDate { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PriceWithAddOns { get; set; }
        public decimal DiscountPrice { get; set; }
        public string Ipaddress { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public bool PaymentRecieved { get; set; }
        public bool Processed { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TipAmount { get; set; }
        public Nullable<System.DateTime> SubscriptionEndDate { get; set; }
        public string ServiceDay { get; set; }   
       
        
       public virtual PackageViewModel Package { get; set; }
        
        public virtual IList<CarServiceViewModel> Services { get; set; }

        public IList<PaypalAutoPaymentsViewModel> PaymentHistorys { get; set; }
        
    }
}