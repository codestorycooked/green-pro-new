using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.Api.ViewModels
{
    public class CarUserViewModel
    {
        public int CarId { get; set; }
        public string DisplayName { get; set; }
        public string Make { get; set; }
        public string LicenseNumber { get; set; }
        public string Color { get; set; }
        public int PurchaseYear { get; set; }
        public string UserId { get; set; }
        public bool AutoRenewal { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> GarageId { get; set; }
    }
}