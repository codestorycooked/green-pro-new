using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
