using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenPro.AdminInterface.Models
{
    public class GarageViewModel
    {
        public GarageViewModel()
        {
            AvailableServiceDays = new List<SelectListItem>();
            AvailableCitys = new List<SelectListItem>();
            AvailableStates = new List<SelectListItem>();
        }


        public virtual IList<SelectListItem> AvailableServiceDays { get; set; }
        public virtual IList<SelectListItem> AvailableCitys { get; set; }
        public virtual IList<SelectListItem> AvailableStates { get; set; }
        

        public int GarageId { get; set; }
        public string Garage_Name { get; set; }
        public string Garage_Address { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public double Latitute { get; set; }
        public double Longitude { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public string CreatedBy { get; set; }
        public string Contact_Person { get; set; }
        public string Phone_Number { get; set; }
        public string Email { get; set; }
        public Nullable<System.TimeSpan> OpenTime { get; set; }
        public System.TimeSpan CloseTime { get; set; }
        public string[] ServiceDays { get; set; }

    }
}