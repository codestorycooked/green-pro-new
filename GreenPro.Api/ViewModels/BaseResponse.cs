using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.Api.ViewModels
{
    public abstract class BaseResponse
    {
        public bool Result { get; set; }
        public string ResponseMessage { get; set; }

    }
}