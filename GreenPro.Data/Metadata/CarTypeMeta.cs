﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Data.Metadata
{
    [MetadataType(typeof(CarTypeMetadata))]
    public partial class CarType
    {
        public class CarTypeMetadata
        {
            public int Id { get; set; }
            [Required]
            public string Description { get; set; }
        }
    }
}
