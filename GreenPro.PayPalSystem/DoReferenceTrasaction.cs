
using GreenPro.PayPalSystem.Models;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GreenPro.PayPalSystem
{
    public class DoReferenceTrasaction
    {
        public PayPalTrasactions DoTransaction(string billingAgreementID, string orderDescription, string itemName, double itemAmount, string serviceDescription, out string PaypalResponse)
        {
            string response = string.Empty;

            // Create request object
            DoReferenceTransactionRequestType request = new DoReferenceTransactionRequestType();
            populateRequestObject(request, billingAgreementID, orderDescription, itemName, itemAmount, serviceDescription);
            // Invoke the API
            DoReferenceTransactionReq wrapper = new DoReferenceTransactionReq();
            wrapper.DoReferenceTransactionRequest = request;
            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            string requestString = "  Paypal DoTransaction Request billingAgreementID: " + billingAgreementID + ":  " ;
            requestString = requestString + JsonConvert.SerializeObject(wrapper);

            // # API call 
            // Invoke the DoReferenceTransaction method in service wrapper object  
            DoReferenceTransactionResponseType doReferenceTxnResponse = service.DoReferenceTransaction(wrapper);
            PayPalTrasactions responsePaypal = new PayPalTrasactions();
            responsePaypal = ProcessTrasactionResponse(doReferenceTxnResponse);
            response = JsonConvert.SerializeObject(doReferenceTxnResponse);
            PaypalResponse = requestString +"  Paypal DoTransaction Response billingAgreementID: " + billingAgreementID + " : "+ response;
            return responsePaypal;
        }

        private void populateRequestObject(DoReferenceTransactionRequestType request, string billingAgreementID, string orderDescription, string itemName, double itemAmount, string serviceDescription)
        {
            DoReferenceTransactionRequestDetailsType referenceTransactionDetails = new DoReferenceTransactionRequestDetailsType();
            request.DoReferenceTransactionRequestDetails = referenceTransactionDetails;
            // (Required) A transaction ID from a previous purchase, such as a credit card charge using the DoDirectPayment API, or a billing agreement ID.
            referenceTransactionDetails.ReferenceID = billingAgreementID;

            // (Optional) How you want to obtain payment. It is one of the following values:
            // * Authorization – This payment is a basic authorization subject to settlement with PayPal Authorization and Capture.
            // * Sale – This is a final sale for which you are requesting payment.
            referenceTransactionDetails.PaymentAction = (PaymentActionCodeType)
                Enum.Parse(typeof(PaymentActionCodeType), "SALE");
            // Populate payment requestDetails. 
            PaymentDetailsType paymentDetails = new PaymentDetailsType();
            referenceTransactionDetails.PaymentDetails = paymentDetails;
            // (Required) The total cost of the transaction to the buyer. If shipping cost and tax charges are known, include them in this value. If not, this value should be the current subtotal of the order. If the transaction includes one or more one-time purchases, this field must be equal to the sum of the purchases. Set this field to 0 if the transaction does not include a one-time purchase such as when you set up a billing agreement for a recurring payment that is not immediately charged. When the field is set to 0, purchase-specific fields are ignored.
            // Note: You must set the currencyID attribute to one of the 3-character currency codes for any of the supported PayPal currencies.
            double orderTotal = 0.0;
            // (Optional) Sum of cost of all items in this order.
            // Note: You must set the currencyID attribute to one of the 3-character currency codes for any of the supported PayPal currencies.
            double itemTotal = 0.0;
            CurrencyCodeType currency = (CurrencyCodeType)
                Enum.Parse(typeof(CurrencyCodeType), "USD");

            // (Optional) Sum of tax for all items in this order.
            // Note: You must set the currencyID attribute to one of the 3-character currency codes for any of the supported PayPal currencies.
            //if (taxTotal.Value != string.Empty)
            //{
            //    paymentDetails.TaxTotal = new BasicAmountType(currency, taxTotal.Value);
            //    orderTotal += Convert.ToDouble(taxTotal.Value);
            //}

            if (orderDescription != string.Empty)
            {
                paymentDetails.OrderDescription = orderDescription;
            }
            // Each payment can include requestDetails about multiple payment items
            // This example shows just one payment item
            var itemQuantity = 1;
            if (itemName != null && itemAmount != null && itemQuantity != null)
            {
                PaymentDetailsItemType itemDetails = new PaymentDetailsItemType();
                // Item name. This field is required when you pass a value for ItemCategory.
                itemDetails.Name = itemName;
                // Cost of item. This field is required when you pass a value for ItemCategory. 
                
                //itemDetails.Amount = new BasicAmountType(currency, itemAmount.ToString());
                itemDetails.Amount = new BasicAmountType(currency, String.Format("{0:0.00}", itemAmount));

                // Item quantity. This field is required when you pass a value forItemCategory.
                itemDetails.Quantity = Convert.ToInt32(itemQuantity);
                // Indicates whether the item is digital or physical. For digital goods, this field is required and you must set it to Digital to get the best rates. It is one of the following values:
                // * Digital
                // * Physical
                // This field is introduced in version 69.0.
                itemDetails.ItemCategory = (ItemCategoryType)
                    Enum.Parse(typeof(ItemCategoryType), "DIGITAL");
                itemTotal += Convert.ToDouble(itemDetails.Amount.value) * itemDetails.Quantity.Value;
                //if (salesTax.Value != string.Empty)
                //{
                //    itemDetails.Tax = new BasicAmountType(currency, salesTax.Value);
                //    orderTotal += Convert.ToDouble(salesTax.Value);
                //}
                // (Optional) Item description.
                // This field is available since version 53.0.
                if (serviceDescription != string.Empty)
                {
                    itemDetails.Description = serviceDescription;
                }
                paymentDetails.PaymentDetailsItem.Add(itemDetails);
            }
            orderTotal += itemTotal;
            paymentDetails.ItemTotal = new BasicAmountType(currency, itemTotal.ToString());
            paymentDetails.OrderTotal = new BasicAmountType(currency, orderTotal.ToString());




        }
    

        private PayPalTrasactions ProcessTrasactionResponse(DoReferenceTransactionResponseType setECResponse)
        {
            PayPalTrasactions responseObject = new PayPalTrasactions();
            responseObject.ApiStatus = setECResponse.Ack.ToString();
            if (setECResponse.Ack.Equals(AckCodeType.FAILURE) ||
               (setECResponse.Errors != null && setECResponse.Errors.Count > 0))
            {
                responseObject.ResponseError = setECResponse.Errors;

            }
            else
            {
                responseObject.ResponseError = null;
                DoReferenceTransactionResponseDetailsType transactionDetails = setECResponse.DoReferenceTransactionResponseDetails;
                responseObject.TransactionID = transactionDetails.TransactionID;
                responseObject.BillingAgreementID = transactionDetails.BillingAgreementID;

                if (transactionDetails.PaymentInfo != null)
                {
                    if (transactionDetails.PaymentInfo.PaymentStatus != null)
                        responseObject.PaymentStatus = transactionDetails.PaymentInfo.PaymentStatus.Value.ToString();
                    if (transactionDetails.PaymentInfo.PendingReason != null)
                    {
                        responseObject.PendingReason = transactionDetails.PaymentInfo.PendingReason.Value.ToString();                        
                    }
                    responseObject.PaymentDate = transactionDetails.PaymentInfo.PaymentDate.ToString();
                    responseObject.GrossAmount = transactionDetails.PaymentInfo.GrossAmount.value;
                    responseObject.TransactionID = transactionDetails.PaymentInfo.TransactionID;

                   
                    responseObject.ItemTotal = transactionDetails.PaymentInfo.GrossAmount.value;
                    
                    //responseObject.ResponseError = transactionDetails.PaymentInfo.PaymentError;
                    
                    // comment by nitendra 16 March 2016
                    //responseObject.OrderTotal = transactionDetails.Amount.value;

                    responseObject.OrderTotal = transactionDetails.PaymentInfo.GrossAmount.value;


                }


            }
            return responseObject;
        }

        
    
    
    }
}
