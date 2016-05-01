using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Data.Metadata
{
    [MetadataType(typeof(GarageUserMetadata))]
    public partial class GarageUser
    {
        public class GarageUserMetadata
        {
            public int Id { get; set; }
            [Required]
            [DisplayName("Garage")]
            public int GarageId { get; set; }
            [Required]
            [DisplayName("User")]
            public string UserId { get; set; }
        }
    }
}
