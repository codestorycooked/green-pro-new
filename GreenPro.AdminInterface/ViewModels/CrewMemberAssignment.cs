using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenPro.Data;

namespace GreenPro.AdminInterface.ViewModels
{
    public class CrewMemberAssignment
    {
        public List<LeaderMember> PickedMembers { get; set; }
        public List<WorkerGarage> AvailableMembers { get; set; }
        public bool IsSelected { get; set; }
    }
}