using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Data.Metadata
{
    [MetadataType(typeof(CarUserMeta))]
    public partial class CarUser
    {
        public class CarUserMeta
        {
            public int CarId { get; set; }
            [Required(ErrorMessage = "Display Name is required.")]
            [Display(Name = "Display Name")]
            public string DisplayName { get; set; }
            [Required(ErrorMessage = "Make is required.")]
            [Display(Name = "Make")]
            public string Make { get; set; }
            [Required(ErrorMessage = "License Number is required.")]
            [Display(Name = "License Number")]
            public string LicenseNumber { get; set; }
            public string Color { get; set; }
            [Required(ErrorMessage = "Type of Car is required.")]
            [Display(Name = "Type of Car")]
            public int Type { get; set; }
            [Required(ErrorMessage = "Purchase Year is required.")]
            [Display(Name = "Purchase Year")]
            public int PurchaseYear { get; set; }
            public string UserId { get; set; }
            public bool AutoRenewal { get; set; }
            public int GarageId { get; set; }

            public virtual AspNetUser AspNetUser { get; set; }
            public virtual CarType CarType { get; set; }
        }
    }
}
