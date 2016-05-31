using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GreenPro.PayPalSystem.Payments
{
    public class PayPalDirectPaymentSettings 
    {

        public PayPalDirectPaymentSettings()
        {
            UseSandbox = Convert.ToString(ConfigurationManager.AppSettings["mode"]).ToLower() == "sandbox"?true:false;
            ApiAccountName = ConfigurationManager.AppSettings["apiUsername"].ToString();
            ApiAccountPassword = ConfigurationManager.AppSettings["apiPassword"].ToString();
            Signature = ConfigurationManager.AppSettings["apiSignature"].ToString();
        }
        public TransactMode TransactMode { get; set; }
        public bool UseSandbox { get; set; }
        public string ApiAccountName { get; set; }
        public string ApiAccountPassword { get; set; }
        public string Signature { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to "additional fee" is specified as percentage. true - percentage, false - fixed value.
        /// </summary>
        public bool AdditionalFeePercentage { get; set; }
        /// <summary>
        /// Additional fee
        /// </summary>
        public decimal AdditionalFee { get; set; }
    }

    public enum TransactMode
    {
        /// <summary>
        /// Authorize
        /// </summary>
        Authorize = 0,
        /// <summary>
        /// Authorize and capture
        /// </summary>
        AuthorizeAndCapture = 2
    }
}
