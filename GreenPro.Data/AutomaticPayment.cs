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
    
    public partial class AutomaticPayment
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public Nullable<int> UserPackageID { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string PaypalBillingID { get; set; }
        public string PayPalECToken { get; set; }
        public Nullable<int> AdhocUserPackageID { get; set; }
    }
}
