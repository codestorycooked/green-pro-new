using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Data.Metadata
{
    [MetadataType(typeof(UserPackageMeta))]
    public partial class UserPackage
    {
        public class UserPackageMeta
        {

            [DataType(DataType.Currency)]
            public decimal ActualPrice { get; set; }
            [DataType(DataType.Currency)]
            public decimal TotalPrice { get; set; }
            [DataType(DataType.Currency)]
            public decimal PriceWithAddOns { get; set; }
            [DataType(DataType.Currency)]
            public decimal DiscountPrice { get; set; }
        }
    }
}
