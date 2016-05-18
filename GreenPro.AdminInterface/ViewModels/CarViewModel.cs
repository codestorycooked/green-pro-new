using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.AdminInterface.ViewModels
{
    public class CarViewModel
    {
        public CarViewModel()
        {
            UserPackages = new List<UserPackageViewModel>();
        }

        public int CarId { get; set; }
        public string DisplayName { get; set; }
        public string Make { get; set; }
        public string LicenseNumber { get; set; }
        public string Color { get; set; }
        public int Type { get; set; }
        public string CarType { get; set; }
        public int PurchaseYear { get; set; }
        public string UserId { get; set; }
        public string CustomerName { get; set; }
        public bool AutoRenewal { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> GarageId { get; set; }
        public string Garage { get; set; }

        public IList<UserPackageViewModel> UserPackages { get; set; }




    }

    
   
}