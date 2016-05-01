using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.AdminInterface.ViewModels
{
    public class LeaderServiceDayViewModel
    {
        public LeaderServiceDayViewModel()
        {
            Members = new List<TeamMember>();
            CarServicesList = new List<CarServices>();
        }
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public int GarageId { get; set; }
        public string GarageName { get; set; }

        public string LeaderId { get; set; }
        public string LeaderName { get; set; }

        public string ServiceDay { get; set; }
        public bool ServiceDayRecordExists { get; set; }

        public IList<TeamMember> Members { get; set; }
        public IList<CarServices> CarServicesList { get; set; }
    }

    public partial class TeamMember
    {
        public string MemberId { get; set; }
        public string MemberName { get; set; }
    }
}