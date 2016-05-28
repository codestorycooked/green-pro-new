using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace GreenPro.WebClient.Models
{
    public class CarUserModel
    {
        public int CarId { get; set; }
        public string DisplayName { get; set; }
        public string Make { get; set; }
        public bool AutoRenewal { get; set; }
        public int GarageId { get; set; }
        public string GarageName { get; set; }
        public bool SubscriptionBought { get; set; }
        public string SubscriptionName { get; set; }
        public string ServiceDay { get; set; }
        public int UserPackageId { get; set; }
    }
}