using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.PayPalSystem.Models
{
    public class PaypalResponse
    {
        public string ApiStatus { get; set; }
        public List<ErrorType> ResponseError { get; set; }
        public string ResponseRedirectURL { get; set; }
        public string ECToken { get; set; }
        public string BillingAgreementID { get; set; }
        public string Timestamp { get; set; }
        public string CorrelationID { get; set; }
        public string ACK { get; set; }

    }
}
