using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace GreenPro.Data
{
    [MetadataType(typeof(AspNetUsersMetadata))]
    public partial class AspNetUser
    {
        class AspNetUsersMetadata
        {

            public String Id { get; set; }

            [DisplayName("Email")]
            [Required]
            [EmailAddress]
            [StringLength(256)]
            public String Email { get; set; }



            [DisplayName("Password Hash")]
            public String PasswordHash { get; set; }



            [DisplayName("Phone Number")]

            public String PhoneNumber { get; set; }


            [DisplayName("User Name")]

            [EmailAddress]
            [StringLength(256)]
            public String UserName { get; set; }

            [DisplayName("First Name")]

            [StringLength(100)]
            public string FirstName { get; set; }
            [DisplayName("Last Name")]

            [StringLength(100)]
            public string LastName { get; set; }

            //[DisplayName("Date of Birth")]
            //[Required(ErrorMessage="Date of Birth is Required.")]            
            //[DataType(DataType.Date)]
            //public System.DateTime DateofBirth { get; set; }

            //[DisplayName("Address")]
            //[Required(ErrorMessage="Address is Required.")]            
            //[StringLength(100)]
            //public string Address { get; set; }

            //[Required(ErrorMessage="State Required.")]                        
            //public int State { get; set; }

            //[Required(ErrorMessage="City is Required.")]                        
            //public int City { get; set; }

            [Display(Name = "Zip Code")]


            public string Pincode { get; set; }
        }
    }
}
