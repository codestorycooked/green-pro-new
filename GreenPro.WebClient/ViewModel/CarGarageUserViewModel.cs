using GreenPro.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.WebClient.ViewModel
{
    public class CarGarageUserViewModel
    {
        public IEnumerable<CarUserModel> CarUser { get; set; }
        public IEnumerable<AdhocCarViewModel> AdhocCarUser { get; set; }
        public IEnumerable<GreenPro.Data.Garage> Garages{get;set;}
        public int StateId { get; set; }
        public int CityId { get; set; }
        public bool CarsWithSubscription { get; set; }
    }
    public class AdhocCarViewModel
    {
        public int CarId { get; set; }
        public string DisplayName { get; set; }
        public string Make { get; set; }
        public string LinkedCarDisplayName { get; set; }
    }
}