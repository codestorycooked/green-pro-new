using GreenPro.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenPro.WebClient.ViewModel
{
    public class AdhocUserPackageAddOnViewModel
    {
        public Package Packages { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public CarType Car { get; set; }

        public IEnumerable<CarUser> UserCars { get; set; }

        [Required(ErrorMessage = "You have to select a car.")]
        public string SelectedCar { get; set; }
        public int PackageID { get; set; }
        public bool AutoRenewalSubscription { get; set; }

        public int LinkCarId { get; set; }
    }
}