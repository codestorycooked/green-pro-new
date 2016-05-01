using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Data.Metadata
{
    [MetadataType(typeof(StateMetadata))]
    public partial class State
    {
        public class StateMetadata
        {
            public int Id { get; set; }
            [Required]
            [DisplayName("State Name")]
            public string StateName { get; set; }
        }
    }
}
