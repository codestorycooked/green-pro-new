using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.AdminInterface.Models
{
    public class PackagesViewModel:GreenPro.Data.Package
    {
        public List<GreenPro.Data.Service> Services { get; set; }
    }
}