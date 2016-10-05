using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GreenPro.Data
{
    public enum SubscriptionType : int
    {
        Weekly=1,
    }

    public static class SubscriptionTypeInfo
    {
        public static string GetSubscriptionTypeInfo(int SubscriptionTypeId)
        {
            string subscriptionType = string.Empty;
            if (SubscriptionTypeId == 1)
                subscriptionType = "Weekly";

            if (SubscriptionTypeId == 2)
                subscriptionType = "Bi-Weekly";

            if (SubscriptionTypeId == 3)
                subscriptionType = "Monthly";

            return subscriptionType;
        }

        public static IList<SelectListItem> GetSubscriptionTypeList()
        {
            IList<SelectListItem> ServiceStatus = new List<SelectListItem>();

            ServiceStatus.Add(new SelectListItem() { Text = "Weekly", Value = "1" });
            ServiceStatus.Add(new SelectListItem() { Text = "Bi-Weekly", Value = "2" });
            ServiceStatus.Add(new SelectListItem() { Text = "Monthly", Value = "3" });
            return ServiceStatus;

        }
    }
}
