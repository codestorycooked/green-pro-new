using GreenPro.PayPalSystem.Models;
using Newtonsoft.Json;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.PayPalSystem
{
    public class SetExpressCheckOut
    {
        public PaypalResponse SetExpressCheckout(string userpackageID,string email, string orderDescription, string billingAgreementText, 
            string LogoURL, string brandName, double itemTotalAmount, string PlanName, double planAmount, string planDescription)
        {
            // Create request object
            SetExpressCheckoutRequestType request = new SetExpressCheckoutRequestType();
            PopulateRequestObject(request, email, orderDescription, billingAgreementText, LogoURL, brandName, itemTotalAmount, PlanName, planAmount, planDescription);

            // Invoke the API
            SetExpressCheckoutReq wrapper = new SetExpressCheckoutReq();
            wrapper.SetExpressCheckoutRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);


            string fileName = DateTime.Now.ToString("Request_"+userpackageID + "_" + "yyyy-MM-dd-HH-mm", CultureInfo.InvariantCulture) + ".txt";
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = "~/App_Data/";
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            path = path + fileName;

            string reqString = string.Empty;
            reqString = JsonConvert.SerializeObject(wrapper);
            string text = "Paypal request: " + DateTime.Now.ToString();
            text += Environment.NewLine + Environment.NewLine + "request string: " + reqString;
            System.IO.File.WriteAllText(Path.Combine(baseDirectory, path), text);

            // # API call 
            // Invoke the SetExpressCheckout method in service wrapper object  
            SetExpressCheckoutResponseType setECResponse = service.SetExpressCheckout(wrapper);

            string responseString = string.Empty;
            responseString = JsonConvert.SerializeObject(setECResponse);

            PaypalResponse responsePaypal = new PaypalResponse();
            responsePaypal = ProcessResponse(setECResponse);
            return responsePaypal;

        }

      

        public PaypalResponse CreateBillingAgreement(PaypalResponse response)
        {
            CreateBillingAgreementRequestType request = new CreateBillingAgreementRequestType();
            // (Required) The time-stamped token returned in the SetCustomerBillingAgreement response.
            // Note: The token expires after 3 hours.
            request.Token = response.ECToken;

            // Invoke the API
            CreateBillingAgreementReq wrapper = new CreateBillingAgreementReq();
            wrapper.CreateBillingAgreementRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the CreateBillingAgreement method in service wrapper object 
            CreateBillingAgreementResponseType billingAgreementResponse = service.CreateBillingAgreement(wrapper);
            PaypalResponse responsePaypal = new PaypalResponse();
            responsePaypal = GetBillingAgreementID(billingAgreementResponse);
            return responsePaypal;

        }

        private PaypalResponse GetBillingAgreementID(CreateBillingAgreementResponseType setECResponse)
        {
            PaypalResponse responseObject = new PaypalResponse();
            responseObject.ApiStatus = setECResponse.Ack.ToString();
            if (setECResponse.Ack.Equals(AckCodeType.FAILURE) || (setECResponse.Errors != null && setECResponse.Errors.Count > 0))
            {
                responseObject.ResponseError = setECResponse.Errors;
                responseObject.ResponseRedirectURL = null;
                responseObject.CorrelationID = setECResponse.CorrelationID;
                responseObject.Timestamp = setECResponse.Timestamp;
                responseObject.ACK = "FAILURE";
                //Todo Log Error to database


                return responseObject;
            }
            else
            {
                responseObject.CorrelationID = setECResponse.CorrelationID;
                responseObject.Timestamp = setECResponse.Timestamp;
                responseObject.ACK = "SUCCESS";
                responseObject.ResponseError = null;                
                responseObject.BillingAgreementID = setECResponse.BillingAgreementID;
                //TodoLog to Databse
                return responseObject;
            }
        }

      
        private PaypalResponse ProcessResponse(SetExpressCheckoutResponseType setECResponse)
        {
            PaypalResponse responseObject = new PaypalResponse();
            responseObject.ApiStatus = setECResponse.Ack.ToString();
            if (setECResponse.Ack.Equals(AckCodeType.FAILURE) || (setECResponse.Errors != null && setECResponse.Errors.Count > 0))
            {
                responseObject.ResponseError = setECResponse.Errors;
                responseObject.ResponseRedirectURL = null;
                responseObject.CorrelationID = setECResponse.CorrelationID;
                responseObject.Timestamp = setECResponse.Timestamp;
                responseObject.ACK = setECResponse.Ack.ToString(); 
                //Todo Log Error to database
                return responseObject;
            }
            else
            {
                responseObject.CorrelationID = setECResponse.CorrelationID;
                responseObject.Timestamp = setECResponse.Timestamp;
                responseObject.ACK = responseObject.ACK = setECResponse.Ack.ToString(); 
                responseObject.ResponseError = null;
                responseObject.ECToken = setECResponse.Token;
                responseObject.ResponseRedirectURL = System.Configuration.ConfigurationManager.AppSettings["RedirectURL"] + "_express-checkout&token=" + setECResponse.Token;
                //Log to Database
                return responseObject;
            }


        }

        private void PopulateRequestObject(SetExpressCheckoutRequestType request, string email, string orderDescription, string billingAgreementText, string LogoURL, string brandName, double itemTotalAmount, string PlanName, double planAmount, string planDescription)
        {
            SetExpressCheckoutRequestDetailsType ecDetails = new SetExpressCheckoutRequestDetailsType();
            ecDetails.ReturnURL = System.Configuration.ConfigurationManager.AppSettings["ReturnUrl"];
            ecDetails.CancelURL = System.Configuration.ConfigurationManager.AppSettings["CancelURL"];

            ecDetails.BuyerEmail = email;
            //Optional get from merchant account ecDetails.ReqConfirmShipping=value;
            //Optional get from paypal  ecDetails.AddressOverride
            //optional ecDetails.NoShipping
            ecDetails.SolutionType = (SolutionTypeType)Enum.Parse(typeof(SolutionTypeType), "SOLE");
            //ecDetails.LandingPage = (LandingPageType)Enum.Parse(typeof(LandingPageType), "BILLING");

            //Adding payment details
            PaymentDetailsType paymentDetails = new PaymentDetailsType();
            ecDetails.PaymentDetails.Add(paymentDetails);

            // (Required) Total cost of the transaction to the buyer. 
            //If shipping cost and tax charges are known, include them in this value. 
            //If not, this value should be the current sub-total of the order. If the transaction includes one or more one-time purchases, 
            //this field must be equal to the sum of the purchases. Set this field to 0 if the transaction does not include a one-time purchase 
            //such as when you set up a billing agreement for a recurring payment that is not immediately charged. When the field is set to 0, purchase-specific fields are ignored.
            
            double orderTotal = 0.0;
            // Sum of cost of all items in this order. For digital goods, this field is required.
            double itemTotal = 0.0;
            CurrencyCodeType currency = (CurrencyCodeType)
                Enum.Parse(typeof(CurrencyCodeType), "USD");
            // How you want to obtain payment. When implementing parallel payments, 
            // this field is required and must be set to Order.
            // When implementing digital goods, this field is required and must be set to Sale.
            // If the transaction does not include a one-time purchase, this field is ignored. 
            // It is one of the following values:
            //   Sale – This is a final sale for which you are requesting payment (default).
            //   Authorization – This payment is a basic authorization subject to settlement with PayPal Authorization and Capture.
            //   Order – This payment is an order authorization subject to settlement with PayPal Authorization and Capture.
            paymentDetails.PaymentAction = (PaymentActionCodeType)
                Enum.Parse(typeof(PaymentActionCodeType), "SALE");

            // Each payment can include requestDetails about multiple items
            // This example shows just one payment item
            var itemName = PlanName;
            var itemQuantity = 1;
            if (itemName != null && planAmount != null && itemQuantity != null)
            {
                PaymentDetailsItemType itemDetails = new PaymentDetailsItemType();
                itemDetails.Name = itemName;
                //itemDetails.Amount = new BasicAmountType(currency, planAmount.ToString()); // Comment by nitendra
                itemDetails.Amount = new BasicAmountType(currency, String.Format("{0:0.00}", planAmount)); // Added by nitendra
                // planAmount
                itemDetails.Quantity = Convert.ToInt32(itemQuantity);
                // Indicates whether an item is digital or physical. For digital goods, this field is required and must be set to Digital. It is one of the following values:
                //   1.Digital
                //   2.Physical
                //  This field is available since version 65.1. 
                itemDetails.ItemCategory = (ItemCategoryType)
                    Enum.Parse(typeof(ItemCategoryType), "DIGITAL");
                itemTotal += Convert.ToDouble(itemDetails.Amount.value) * itemDetails.Quantity.Value;
                //(Optional) Item sales tax.
                //    Note: You must set the currencyID attribute to one of 
                //    the 3-character currency codes for any of the supported PayPal currencies.
                //    Character length and limitations: Value is a positive number which cannot exceed $10,000 USD in any currency.
                //    It includes no currency symbol. It must have 2 decimal places, the decimal separator must be a period (.), 
                //    and the optional thousands separator must be a comma (,).
                //if (salesTax.Value != string.Empty)
                //{
                //    itemDetails.Tax = new BasicAmountType(currency, salesTax.Value);
                //    orderTotal += Convert.ToDouble(salesTax.Value);
                //}
                //(Optional) Item description.
                // Character length and limitations: 127 single-byte characters
                // This field is introduced in version 53.0. 
                if (planDescription != string.Empty)
                {
                    itemDetails.Description = planDescription;
                }
                paymentDetails.PaymentDetailsItem.Add(itemDetails);
            }

            // Comment by nitendra
            //orderTotal += itemTotal;
            //paymentDetails.ItemTotal = new BasicAmountType(currency, itemTotal.ToString());
            //paymentDetails.OrderTotal = new BasicAmountType(currency, orderTotal.ToString());

            // Added by nitendra
            orderTotal += itemTotal;
            paymentDetails.ItemTotal = new BasicAmountType(currency, String.Format("{0:0.00}", itemTotal));
            paymentDetails.OrderTotal = new BasicAmountType(currency, String.Format("{0:0.00}", orderTotal));


            if (billingAgreementText != string.Empty)
            {
                //(Required) Type of billing agreement. For recurring payments,
                //this field must be set to RecurringPayments. 
                //In this case, you can specify up to ten billing agreements. 
                //Other defined values are not valid.
                //Type of billing agreement for reference transactions. 
                //You must have permission from PayPal to use this field. 
                //This field must be set to one of the following values:
                //   1. MerchantInitiatedBilling - PayPal creates a billing agreement 
                //      for each transaction associated with buyer.You must specify 
                //      version 54.0 or higher to use this option.
                //   2. MerchantInitiatedBillingSingleAgreement - PayPal creates a 
                //      single billing agreement for all transactions associated with buyer.
                //      Use this value unless you need per-transaction billing agreements. 
                //      You must specify version 58.0 or higher to use this option.
                BillingCodeType billingCodeType = (BillingCodeType)
                    Enum.Parse(typeof(BillingCodeType), "MERCHANTINITIATEDBILLING");
                BillingAgreementDetailsType baType = new BillingAgreementDetailsType(billingCodeType);
                baType.BillingAgreementDescription = billingAgreementText;
                ecDetails.BillingAgreementDetails.Add(baType);
            }

            //Logo for Website
            // (Optional) URL for the image you want to appear at the top left of the payment page. The image has a maximum size of 750 pixels wide by 90 pixels high. PayPal recommends that you provide an image that is stored on a secure (https) server. If you do not specify an image, the business name displays.
            if (LogoURL != string.Empty)
            {
                ecDetails.cppHeaderImage = LogoURL;
            }

            // (Optional) A label that overrides the business name in the PayPal account on the PayPal hosted checkout pages.
            if (brandName != string.Empty)
            {
                ecDetails.BrandName = brandName;
            }
            request.SetExpressCheckoutRequestDetails = ecDetails;
        }

    }
}
