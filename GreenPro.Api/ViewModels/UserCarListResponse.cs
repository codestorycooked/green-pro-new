using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.Api.ViewModels
{
    public class UserCarListResponse
    {
        public UserCarListResponse()
        {
            cars = new List<CarUserViewModel>();
        }

        public IList<CarUserViewModel> cars { get; set; }
    }
}