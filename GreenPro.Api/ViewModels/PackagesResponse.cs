﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.Api.ViewModels
{
    public class PackagesResponse : BaseResponse
    {
        public PackagesResponse()
        {
            Packages = new List<PackageViewModel>();
        }

        public IList<PackageViewModel> Packages { get; set; }
    }

    public class ServiceAddOnsResponse : BaseResponse
    {
        public ServiceAddOnsResponse()
        {
            data = new List<Data.Service>();
        }
        public IList<Data.Service> data { get; set; }

    }
}


