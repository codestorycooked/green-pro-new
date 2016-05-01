using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Data.Metadata
{
    [MetadataType(typeof(GarageWeekdayMeta))]
    public partial class GarrageWeekday
    {
        public class GarageWeekdayMeta
        {
            public int Id { get; set; }
            [Required]
            [DisplayName("Garage")]
            public int GarrageId { get; set; }
            [Required]
            [DisplayName("Weekday")]
            public int WeekdayId { get; set; }
        }
    }
}
