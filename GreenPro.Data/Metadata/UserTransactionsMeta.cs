using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Data.Metadata
{
    [MetadataType(typeof(UserTransactionsMeta))]
    public partial class UserTransaction

    {
        public class UserTransactionsMeta
        {
            [DataType(DataType.DateTime)]

            [Display(Name="Transaction Date")]
            public System.DateTime TransactionDate { get; set; }
            [DataType(DataType.Currency)]
            public decimal Amount { get; set; }
            public Nullable<int> PackageId { get; set; }
            [Display(Name="Paypal Refrence ID")]
            public string PaypalId { get; set; }
            public string Details { get; set; }
            public string Userid { get; set; }
        }
    }
}
