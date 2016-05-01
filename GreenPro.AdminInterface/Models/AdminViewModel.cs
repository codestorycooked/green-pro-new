using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IdentitySample.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel : GreenPro.Data.AspNetUser
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        public string DateofBirthStr { get; set; }

        [Display(Name = "User name (Email)")]
        [EmailAddress(ErrorMessage = "Please Enter your email ID")]
        public string UserName { get; set; }

        [Display(Name = "State")]
        [Required]
        public int StateId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> StateList { get; set; }
        [Display(Name = "City")]
        [Required]
        //public string City { get; set; }
        public int CityId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> CityList { get; set; }


        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}