using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace GreenPro.Data
{
    [MetadataType(typeof(ServicesMetadata))]
    public partial class Service
    {
        class ServicesMetadata
        {
          
            public Int32 ServiceID { get; set; }

            [DisplayName("Service Name")]
            [Required]
            [StringLength(100)]
            public String Service_Name { get; set; }

            [DisplayName("Service Description")]
            [Required]
            [StringLength(200)]
            public String Service_Description { get; set; }

            [DisplayName("Service Price")]
            [Required]
            [DataType(DataType.Currency)]
            public Decimal Service_Price { get; set; }

            [DisplayName("Create Date")]
            [Required]
            public DateTime CreateDt { get; set; }

          
        }
    }
}
