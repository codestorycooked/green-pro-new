using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace GreenPro.AdminInterface.Helper
{
    public class ListHelper
    {
        public static IList<SelectListItem> GetDayNameList()
        {
            
            IList<SelectListItem> ServiceDays=new  List<SelectListItem>();            
            foreach (var item in DateTimeFormatInfo.CurrentInfo.DayNames)
            {
                SelectListItem li = new SelectListItem();
                li.Text = item;
                li.Value = item;
                ServiceDays.Add(li);

            }
            return ServiceDays;

        }

        public static IList<SelectListItem> GetServiceStatusList()
        {
            IList<SelectListItem> ServiceStatus = new List<SelectListItem>();

            ServiceStatus.Add(new SelectListItem() { Text = "Pending", Value = "10" });
            ServiceStatus.Add(new SelectListItem() { Text = "In Process", Value = "20" });
            ServiceStatus.Add(new SelectListItem() { Text = "Complate", Value = "30" });
            return ServiceStatus;

        }


        public static IList<SelectListItem> GetSubscriptionTypeList()
        {
            IList<SelectListItem> ServiceStatus = new List<SelectListItem>();

            ServiceStatus.Add(new SelectListItem() { Text = "One Time", Value = "4" });

            ServiceStatus.Add(new SelectListItem() { Text = "Weekly", Value = "1" });
            ServiceStatus.Add(new SelectListItem() { Text = "Bi-Weekly", Value = "2" });
            ServiceStatus.Add(new SelectListItem() { Text = "Monthly", Value = "3" });
            return ServiceStatus;

        }
    }
}