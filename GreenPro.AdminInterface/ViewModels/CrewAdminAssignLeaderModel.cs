using GreenPro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.AdminInterface.ViewModels
{
    public class CrewAdminAssignLeaderModel : AspNetUser
    {
        public bool IsLeader { get; set; }
        public int GarageId { get; set; }

        public int WorkerId { get; set; }
    }
}