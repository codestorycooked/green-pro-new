using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.WebClient.ViewModel
{
    public class PackageViewModel
    {
        public PackageViewModel()
        {
            Services = new List<ServiceViewModel>();
        }
        public int PackageId { get; set; }
        public string Package_Name { get; set; }
        public decimal Package_Price { get; set; }
        public IList<ServiceViewModel> Services { get; set; }
    }


    public class ServiceViewModel
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
    }
}