using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenPro.Data;

namespace GreenPro.WebClient.ViewModel
{
    public class GarageSearchViewModel
    {
        public IEnumerable<State> StateList { get; set; }
        public IEnumerable<City> CityList { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public IEnumerable<Garage> Garages { get; set; }
    }
}