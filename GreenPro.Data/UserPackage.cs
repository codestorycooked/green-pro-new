//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GreenPro.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserPackage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserPackage()
        {
            this.AdhocUserPackages = new HashSet<AdhocUserPackage>();
            this.PaypalAutoPayments = new HashSet<PaypalAutoPayment>();
            this.PayPalLogs = new HashSet<PayPalLog>();
            this.UnAssignedCars = new HashSet<UnAssignedCar>();
            this.UserPackagesAddons = new HashSet<UserPackagesAddon>();
            this.UserTransactions = new HashSet<UserTransaction>();
        }
    
        public int Id { get; set; }
        public string UserId { get; set; }
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
        public bool IsActive { get; set; }
        public Nullable<int> GaragesTimeingSlotId { get; set; }
        public Nullable<System.DateTime> NextServiceDate { get; set; }
        public Nullable<System.DateTime> LastServiceDate { get; set; }
        public int SubscriptionTypeId { get; set; }
        public string PaymentMethodName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdhocUserPackage> AdhocUserPackages { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual CarUser CarUser { get; set; }
        public virtual Package Package { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaypalAutoPayment> PaypalAutoPayments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayPalLog> PayPalLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnAssignedCar> UnAssignedCars { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserPackagesAddon> UserPackagesAddons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTransaction> UserTransactions { get; set; }
    }
}
