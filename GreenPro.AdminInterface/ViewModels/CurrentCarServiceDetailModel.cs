using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenPro.AdminInterface.ViewModels
{
    public class CurrentCarServiceDetailModel
    {
        public CurrentCarServiceDetailModel()
        {
            AvailableServiceStatus = new List<SelectListItem>();
        }
       public int CurrentCarServiceId { get; set; }
       public string Comment { get; set; }
       public DateTime? StartDateTime { get; set; }
       public DateTime? EndDateTime { get; set; }
       public int ServiceStatusId { get; set; }

       public CarServices CarService { get; set; }
       public IList<SelectListItem> AvailableServiceStatus { get; set; }
    }
}