using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenPro.AdminInterface.ViewModels
{
    public partial class GarageTeamModel
    {
        public GarageTeamModel()
        {
            AvailableGarages = new List<SelectListItem>();
        }

        public IList<SelectListItem> AvailableGarages { get; set; }


        public int Id { get; set; }
        public int GarageId { get; set; }
        public string GarageName { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}