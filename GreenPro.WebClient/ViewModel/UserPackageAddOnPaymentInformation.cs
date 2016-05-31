using GreenPro.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenPro.WebClient.ViewModel
{
    public class UserPackageAddOnPaymentInformation
    {
        public UserPackageAddOnPaymentInformation()
        {
            CreditCardTypes = new List<SelectListItem>();
            ExpireMonths = new List<SelectListItem>();
            ExpireYears = new List<SelectListItem>();
            Warnings = new List<string>();
        }

        public UserPackage UserPackge { get; set; }
        public IEnumerable<UserPackagesAddon> Addons { get; set; }

        
        public int UserPackageID { get; set; }
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


        [DisplayName("Card Type")]
        [Required]
        public string CreditCardType { get; set; }

        [DisplayName("Name On Card")]
        [Required]
        public string NameOnCard { get; set; }

        [DisplayName("Card Number")]
        [Required]
        public string CardNumber { get; set; }

        [DisplayName("Expiry Month")]
        [Required]
        public int CardExpiryMonth { get; set; }

        [DisplayName("Expiry Year")]
        [Required]
        public int CardExpiryYear { get; set; }

        [DisplayName("Card Security Code")]
        [Required]
        public string CardSecurityCode { get; set; }

        public IList<SelectListItem> ExpireMonths { get; set; }
        public IList<SelectListItem> ExpireYears { get; set; }
        public IList<SelectListItem> CreditCardTypes { get; set; }

        public IList<string> Warnings { get; set; }

    }
}