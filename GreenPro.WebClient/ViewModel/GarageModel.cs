using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.WebClient.ViewModel
{
    public class GarageModel
    {
        public int GarageId { get; set; }
        public string Garage_Name { get; set; }
        public string Garage_Address { get; set; }
        public string ServiceDays { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public Nullable<System.TimeSpan> OpenTime { get; set; }
        public System.TimeSpan CloseTime { get; set; }
        public string Pincode { get; set; }
        public double Latitute { get; set; }
        public double Longitude { get; set; }
    }
}