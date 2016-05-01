using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.AdminInterface.ViewModels
{
    public class CarListViewModel
    {
        public  CarListViewModel()
        {
            Cars = new List<CarViewModel>();
        }
        public IList<CarViewModel> Cars { get; set; }
    }
}