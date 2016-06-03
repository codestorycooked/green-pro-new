using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenPro.AdminInterface.Models
{
    public class MasterPackageViewModel
    {
        public MasterPackageViewModel()
        {
            AvailableSubscriptionTypes = new List<SelectListItem>();
        }
    
        public int PackageId { get; set; }
        public string Package_Name { get; set; }
        public string Package_Description { get; set; }
        public decimal Package_Price { get; set; }        
        public string CreatedBy { get; set; }
        public string[] SubscriptionTypes { get; set; }

        public virtual IList<SelectListItem> AvailableSubscriptionTypes { get; set; }
    }
}