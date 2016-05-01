using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.PayPalSystem.Models
{
    public class PayPalTrasactions
    {
        //Request parameter
        public string ReferenceID { get; set; }
        public string PaymentAction { get; set; }
        public string IPAddress { get; set; }
        public string OrderTotal { get; set; }
        public string ItemTotal { get; set; }
        public string InvoiceID { get; set; }

        public string ApiStatus { get; set; }
        public List<ErrorType> ResponseError { get; set; }

        public string BillingAgreementID { get; set; }
        public string Timestamp { get; set; }



        //PaymentInfo  Response
        //https://developer.paypal.com/docs/classic/api/merchant/DoReferenceTransaction_API_Operation_SOAP/
        public string TransactionID { get; set; }
        public string PaymentStatus { get; set; }
        public string PendingReason { get; set; }
        public string PaymentError { get; set; }
        public string PaymentDate { get; set; }
        public string GrossAmount { get; set; }
        
        

    }
}
