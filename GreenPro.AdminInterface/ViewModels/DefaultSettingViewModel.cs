using GreenPro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GreenPro.AdminInterface.ViewModels
{
    public partial class DefaultSettingViewModel
    {
        public DefaultSettingViewModel()
        {
            AvailableTeams = new List<SelectListItem>();
            AvailableLeaders = new List<SelectListItem>();
            AvailableServiceDays = new List<SelectListItem>();
            AvailableMembers = new List<SelectListItem>();
            AvailableCars = new List<SelectListItem>();

            CarServicesList = new List<CarServices>();
            CarPayments = new List<CarServicesPayment>();

            SelectedLeaders = new Dictionary<string, IDictionary<int, bool>>();
            SelectedMembers = new Dictionary<string, IDictionary<int, bool>>();
            SelectedCars = new Dictionary<int, IDictionary<int, bool>>();
        }



        public int GarageId { get; set; }

        public string GarageName { get; set; }
        public string ServiceDay { get; set; }
        public DateTime ServiceDate { get; set; }
        public string[] SelectedLeaderIds { get; set; }

        public bool PrepareModelData { get; set; }

        //[Leader id] / [Team id] / [selected]
        public IDictionary<string, IDictionary<int, bool>> SelectedLeaders { get; set; }

        //[Member id] / [Team id] / [selected]
        public IDictionary<string, IDictionary<int, bool>> SelectedMembers { get; set; }

        //[Car id] / [Team id] / [selected]
        public IDictionary<int, IDictionary<int, bool>> SelectedCars { get; set; }

        public IList<SelectListItem> AvailableTeams { get; set; }
        public IList<SelectListItem> AvailableLeaders { get; set; }
        public IList<SelectListItem> AvailableMembers { get; set; }
        public IList<SelectListItem> AvailableCars { get; set; }
        public IList<SelectListItem> AvailableServiceDays { get; set; }

        public IList<CarServices> CarServicesList { get; set; }

        public IList<CarServicesPayment> CarPayments { get; set; }

    }

    public partial class CarServices
    {
        public CarServices()
        {
            SelectServices = new List<SelectService>();
        }
        public int ServiceDayId { get; set; }
        public string CarDisplayName { get; set; }
        public int CarId { get; set; }
        public int TeamId { get; set; }

        public string DisplayName { get; set; }
        public string Make { get; set; }
        public string LicenseNumber { get; set; }
        public string Color { get; set; }
        public int Type { get; set; }
        public string CarType { get; set; }
        public int PurchaseYear { get; set; }
        public string UserId { get; set; }
        public bool AutoRenewal { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> GarageId { get; set; }
        public string Garage { get; set; }
        public int ServiceStatusId { get; set; }

        public ServiceStatus serviceStatus { get; set; }


        public IList<SelectService> SelectServices { get; set; }

       


        public string Comment { get; set; }

        public class SelectService
        {
            public string ServiceName { get; set; }
        }
    }


    public partial class CarServicesPayment
    {
        public int ServiceDayId { get; set; }       
        public int CarId { get; set; }
        public string DisplayName { get; set; }
        public string Make { get; set; }
        public string LicenseNumber { get; set; }
        public string Color { get; set; }
        public bool IsPaid { get; set; }
    }
}