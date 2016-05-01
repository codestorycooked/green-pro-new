using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Data.Metadata
{
    [MetadataType(typeof(CityMetadata))]
    public partial class City
    {
        class CityMetadata
        {
            public int CarId { get; set; }
            [DisplayName("Display Name")]
            [Required]
            public string DisplayName { get; set; }
            [Required]
            public string Make { get; set; }
            [DisplayName("License Number")]
            [Required]
            public string LicenseNumber { get; set; }
            [Required]
            public string Color { get; set; }
            [Required]
            public int Type { get; set; }
            [DisplayName("Purchase Year")]
            [Required]
            public int PurchaseYear { get; set; }
            [DisplayName("User")]
            [Required]
            public string UserId { get; set; }
            [Required]
            public bool Default { get; set; }
            [DisplayName("Deleted")]
            [Required]
            public Nullable<bool> IsDeleted { get; set; }
        }
    }
}
