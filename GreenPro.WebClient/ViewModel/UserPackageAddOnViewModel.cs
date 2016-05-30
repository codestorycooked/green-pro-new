using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenPro.Data;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace GreenPro.WebClient.ViewModel
{
    public class UserPackageAddOnViewModel
    {
        public UserPackageAddOnViewModel()
        {
            AvailableServiceDays = new List<SelectListItem>();
            AvailableGaragesTimeingSlots = new List<SelectListItem>();
            AvailableSubscriptionTypes = new List<SelectListItem>();
        }
        public Package Packages { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public CarType Car { get; set; }

        public IEnumerable<CarUser> UserCars { get; set; }

        [Required(ErrorMessage = "You have to select a car.")]
        public string SelectedCar { get; set; }
        public int PackageID { get; set; }
        public int GarageId { get; set; }

        public int SubscriptionTypeId { get; set; }
        public int GaragesTimeingSlotId { get; set; }
        public string ServiceDay { get; set; }
        public bool AutoRenewalSubscription { get; set; }
        public IList<SelectListItem> AvailableServiceDays { get; set; }
        public IList<SelectListItem> AvailableGaragesTimeingSlots { get; set; }
        public IList<SelectListItem> AvailableSubscriptionTypes { get; set; }
    }
}