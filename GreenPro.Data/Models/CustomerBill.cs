using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Data.Models
{
    public class CustomerBillDetails
    {
        public AspNetUser Customer { get; set; }
        public CarUser CarDetails { get; set; }
        public decimal Amount { get; set; }
        public Package PackageName { get; set; }
        public List<Service> ServiceList { get; set; }
    }
}
