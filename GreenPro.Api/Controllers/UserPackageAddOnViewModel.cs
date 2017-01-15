using GreenPro.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenPro.Api.Controllers
{
    public class UserPackageAddOnViewModel
    {
        public UserPackageAddOnViewModel()
        {
            AvailableServiceDays = new List<string>();
            AvailableGaragesTimeingSlots = new List<string>();
            AvailableSubscriptionTypes = new List<string>();
        }
        public Package Packages { get; set; }
        public IEnumerable<GreenPro.Data.Service> Services { get; set; }
        public CarType Car { get; set; }

        public IEnumerable<CarUser> UserCars { get; set; }

        [Required(ErrorMessage = "You have to select a car.")]
        public string SelectedCar { get; set; }
        [Required(ErrorMessage = "Package ID is required")]
        public int PackageID { get; set; }
        [Required(ErrorMessage = "Garage ID is required")]
        public int GarageId { get; set; }
        [Required(ErrorMessage = "Subscription ID is required")]
        public int SubscriptionTypeId { get; set; }
        public int GaragesTimeingSlotId { get; set; }
        public string ServiceDay { get; set; }
        public bool AutoRenewalSubscription { get; set; }
        public IList<string> AvailableServiceDays { get; set; }
        public IList<string> AvailableGaragesTimeingSlots { get; set; }
        public IList<string> AvailableSubscriptionTypes { get; set; }
        public string UserId { get; set; }
        
    }

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
}