using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.Api.ViewModels.Garage
{
    public class GaragesListRsponseModel : BaseResponse
    {
        public GaragesListRsponseModel()
        {
            Garages = new List<GarageModel>();
        }
        public IList<GarageModel> Garages { get; set; }
    }

    public class GarageModel
    {
        public int GarageId { get; set; }
        public string Garage_Name { get; set; }
        public string Garage_Address { get; set; }
        public string ServiceDays { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public Nullable<System.TimeSpan> OpenTime { get; set; }
        public string OpenTimeStr { get; set; }
        public string CloseTimeStr { get; set; }
        public System.TimeSpan CloseTime { get; set; }
        public string Pincode { get; set; }
        public double Latitute { get; set; }
        public double Longitude { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
    }

}