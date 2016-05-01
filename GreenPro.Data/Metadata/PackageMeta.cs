using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Data
{
    [MetadataType(typeof(PackageMeta))]
    public partial class Package
    {
        class PackageMeta
        {
            public int PackageId { get; set; }
            [Required(ErrorMessage="Package Name is required.")]
            [Display(Name="Package Name")]
            public string Package_Name { get; set; }
            [Required(ErrorMessage = "Package Description is required.")]
            [Display(Name = "Package Description")]
            public string Package_Description { get; set; }
            [Required(ErrorMessage = "Package Price is required.")]
            [Display(Name = "Package Price")]
            [DataType(DataType.Currency)]
            public decimal Package_Price { get; set; }
            [Required(ErrorMessage = "Date is required.")]
            [Display(Name = "Created Date")]
            [DataType(DataType.Date)]
            public System.DateTime CreateDt { get; set; }
            [Required(ErrorMessage = "Created by is required.")]
            [Display(Name = "Created by")]
            
            public string CreatedBy { get; set; }
        }
    }
}
