using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.WebClient.ViewModel
{
    public class UserPackageDetailViewModel
    {
        public UserPackageDetailViewModel()
        {
            Services=new List<CarServiceViewModel>();
            AvailableAddOns = new List<AddOnsServiceModel>();
            CarModel = new CarViewModel();
            Package = new PackageDetailViewModel();
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
        public string SubscriptionType { get; set; }
        public string ServiceDay { get; set; }
        public string TimeSlot { get; set; }
        public bool IsActive { get; set; }

        public int[] ServiceID { get; set; }

        public bool AddonsAvailableForEdit { get; set; }
       
       public  PackageDetailViewModel Package { get; set; }
        
        public IList<CarServiceViewModel> Services { get; set; }

        public IList<AddOnsServiceModel> AvailableAddOns { get; set; }

        public IList<PaypalAutoPaymentsViewModel> PaymentHistorys { get; set; }

        public CarViewModel CarModel { get; set; }
    }


    public class PackageDetailViewModel
    {

        public int PackageId { get; set; }
        public string Package_Name { get; set; }
        public string Package_Description { get; set; }        
        public decimal Package_Price { get; set; }
        public System.DateTime CreateDt { get; set; }
        public string CreatedBy { get; set; }
    }

    public class CarServiceViewModel
    {
        public int ServiceID { get; set; }
        public string Service_Name { get; set; }
        public string Service_Description { get; set; }
        public decimal Service_Price { get; set; }
        public bool IsAddOn { get; set; }
    }

    public class CarViewModel
    {
        public int CarId { get; set; }
        public string DisplayName { get; set; }
        public string Make { get; set; }
        public string LicenseNumber { get; set; }
        public string Color { get; set; }
        public int Type { get; set; }
        public string CarType { get; set; }
        public int PurchaseYear { get; set; }
        public string UserId { get; set; }
        public string CustomerName { get; set; }
        public bool AutoRenewal { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> GarageId { get; set; }
        public string Garage { get; set; }       
    }

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