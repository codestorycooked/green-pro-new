using GreenPro.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenPro.WebClient.ViewModel
{
    public class UserPackageAddOn
    {
        public UserPackage UserPackge { get; set; }
        public IEnumerable<UserPackagesAddon> Addons { get; set; }

        [Required(ErrorMessage = "Please accept our Terms & Conditions")]
        public string AcceptAgreement { get; set; }
        public string UserPackageID { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal FinalPrice
        {
            get
            {
                if (UserPackge == null)
                    return 0;
                return (UserPackge.TotalPrice + UserPackge.TaxAmount + UserPackge.TipAmount);
            }            
        }
    }
    public class UserPackageAddOnInvoice
    {
        public UserPackage UserPackge { get; set; }
        public IEnumerable<UserPackagesAddon> Addons { get; set; }

        public AspNetUser UserDetails { get; set; }
        public string UserPackageID { get; set; }
    }
}