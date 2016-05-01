using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.AdminInterface.ViewModels
{
    public class UserPackageListViewModel
    {
        public UserPackageListViewModel()
        {
            UserPackages = new List<UserPackageViewModel>();
        }
        public IList<UserPackageViewModel> UserPackages { get; set; }
    }
}