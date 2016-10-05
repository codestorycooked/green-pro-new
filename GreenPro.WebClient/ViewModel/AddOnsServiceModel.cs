using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.WebClient.ViewModel
{
    public class AddOnsServiceModel
    {
        public int ServiceID { get; set; }
        public string Service_Name { get; set; }
        public string Service_Description { get; set; }
        public decimal Service_Price { get; set; }
    }
}