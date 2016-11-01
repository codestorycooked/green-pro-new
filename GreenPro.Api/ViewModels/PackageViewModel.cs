using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.Api.ViewModels
{
    public class PackageViewModel
    {
        public PackageViewModel()
        {
            Services = new List<string>();
        }

        public int PackageId { get; set; }
        public string Package_Name { get; set; }
        public string Package_Description { get; set; }
        public decimal Package_Price { get; set; }        
        public string SubscriptionTypes { get; set; }

        public IList<string> Services {get;set;}
    }
}