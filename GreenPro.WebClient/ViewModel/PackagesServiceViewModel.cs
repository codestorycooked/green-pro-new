using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenPro.Data;
namespace GreenPro.WebClient.ViewModel
{
    public class PackagesServiceViewModel
    {
        public int Sample { get; set; }
        public IEnumerable<CarType> CarTypes { get; set; }
        public IEnumerable<Package> Packages { get; set; }
        public IEnumerable<Package_Services> Services { get; set; }

    }
}