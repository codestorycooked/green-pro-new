using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.WebClient.Models
{
    public partial class CustomerNavigationModel
    {
        public bool HideInfo { get; set; }
        public bool HideAddresses { get; set; }
        public bool HideOrders { get; set; }
        public bool HideChangePassword { get; set; }

        public CustomerNavigationEnum SelectedTab { get; set; }
    }


    public enum CustomerNavigationEnum
    {
        Info = 0,
        Cars = 10,
        Orders = 20,
        YourSubscriptions = 30,
        YourTransactions = 40,
        DownloadableProducts = 50,
        RewardPoints = 60,
        ChangePassword = 70,
        Avatar = 80,
        ForumSubscriptions = 90
    }
}