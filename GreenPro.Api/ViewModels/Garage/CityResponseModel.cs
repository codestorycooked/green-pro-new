using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.Api.ViewModels.Garage
{
    public class CityResponseModel : BaseResponse
    {
        public CityResponseModel()
        {
            Citys = new List<CityModel>();
        }
        public IList<CityModel> Citys { get; set; }
    }
}