using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.AdminInterface.ViewModels
{
    public class PackageViewModel
    {

        public int PackageId { get; set; }
        public string Package_Name { get; set; }
        public string Package_Description { get; set; }
        public int CarTypeId { get; set; }
        public decimal Package_Price { get; set; }
        public System.DateTime CreateDt { get; set; }
        public string CreatedBy { get; set; }
    }
}