using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace GreenPro.Data
{
    [MetadataType(typeof(GaragesMetadata))]
    public partial class Garage
    {
        public string CarID { get; set; }
        class GaragesMetadata
        {
            
            public Int32 GarageId { get; set; }

            [DisplayName("Garage Name")]
            [Required]

            public String Garage_Name { get; set; }

            [DisplayName("Postal Code")]
            [Required]
            [DataType(DataType.PostalCode)]
            public String Pincode { get; set; }

            [DisplayName("Garage Address")]
            [Required]

            public String Garage_Address { get; set; }

            [DisplayName("State")]
            [Required]

            public String State { get; set; }

            [DisplayName("City")]
            [Required]

            public String City { get; set; }

            [DisplayName("Country")]
            [Required]

            public String Country { get; set; }





            [DisplayName("Is Active")]
            public Boolean? IsActive { get; set; }

            [DisplayName("Created Date")]
            [Required]
            public DateTime CreatedDt { get; set; }



            [DisplayName("Contact Person")]
            [Required]

            public String Contact_Person { get; set; }

            [DisplayName("Phone Number")]
            [DataType(DataType.PhoneNumber)]
            [Required]

            public String Phone_Number { get; set; }

            [DisplayName("Email")]
            [DataType(DataType.EmailAddress)]

            [Required]
            public String Email { get; set; }

            [Display(Name = "Open Time")]
            [DataType(DataType.Time)]
            [Required]
            public Nullable<System.TimeSpan> OpenTime { get; set; }
            [Display(Name = "Close Time")]
            [DataType(DataType.Time)]
            [Required]
            public System.TimeSpan CloseTime { get; set; }
            [Display(Name = "Service Days")]
            [Required]
            public string ServiceDays { get; set; }
        }
    }
}
