using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Validation;


namespace Stripe
{
    public partial class StripeClient
    {
        /// <summary>
        /// Creates a charge using a token retrieved via the browser
        /// </summary>
        /// <param name="amount">Amount to be charged</param>
        /// <param name="token">Stripe.js token for a credit card</param>
        /// <param name="currency">3-letter ISO currency code</param>
        /// <param name="description">Description displayed on the Stripe web interface</param>
        /// <param name="metadata"></param>
        /// <param name="capture">Whether or not to immediately capture the charge. When false, the charge issues an authorization (or pre-authorization), and will need to be captured later.</param>
        /// <param name="statementDescriptor">An arbitrary string to be displayed on your customer's credit card statement</param>
        /// <param name="receiptEmail">The email address to send this charge's receipt to. The receipt will not be sent until the charge is paid.</param>
        /// <param name="destination">An account to make the charge on behalf of</param>
        /// <param name="applicationFee">Fixed amount to charge for our service to the receiver. Comes out of total amount charged</param>
        /// <param name="shipping">Shipping information for the charge. Helps prevent fraud on charges for physical goods.</param>
        /// <returns>Stripe Charge Object</returns>
		public StripeObject CreateChargeWithToken(decimal amount, string token, string currency = "usd",
            string description = null, IDictionary<object, object> metadata = null, bool capture = true,
            string statementDescriptor = null, string receiptEmail = null, string destination = null,
            decimal? applicationFee = null, IDictionary<object, object> shipping = null)
        {
            Require.Argument("amount", amount);
            Require.Argument("currency", currency);
            Require.Argument("token", token);

            var request = GetCreateChargeRequest(amount, currency, null, description, metadata, capture, 
                statementDescriptor, receiptEmail, destination, applicationFee, shipping);

            request.AddParameter("source", token);

            return ExecuteObject(request);
        }

        /// <summary>
        /// Creates a charge using a customer id and an optional source (Credit Card) object
        /// </summary>
        /// <param name="amount">Amount to be charged</param>
        /// <param name="currency">3-letter ISO currency code</param>
        /// <param name="customerId">The ID of an existing customer that will be charged in this request</param>
        /// <param name="cardId">A payment source to be charged, such as a credit card</param>
        /// <param name="description">Description displayed on the Stripe web interface</param>
        /// <param name="metadata"></param>
        /// <param name="capture">Whether or not to immediately capture the charge. When false, the charge issues an authorization (or pre-authorization), and will need to be captured later.</param>
        /// <param name="statementDescriptor">An arbitrary string to be displayed on your customer's credit card statement</param>
        /// <param name="receiptEmail">The email address to send this charge's receipt to. The receipt will not be sent until the charge is paid.</param>
        /// <param name="destination">An account to make the charge on behalf of</param>
        /// <param name="applicationFee">Fixed amount to charge for our service to the receiver. Comes out of total amount charged</param>
        /// <param name="shipping">Shipping information for the charge. Helps prevent fraud on charges for physical goods.</param>
        /// <returns>Stripe Charge Object</returns>
        public StripeObject CreateCharge(decimal amount, string currency, string customerId,
            string cardId = null, string description = null, IDictionary<object, object> metadata = null,
            bool capture = true, string statementDescriptor = null, string receiptEmail = null,
            string destination = null, decimal? applicationFee = null,
            IDictionary<object, object> shipping = null)
        {
            Require.Argument("amount", amount);
            Require.Argument("currency", currency);
            Require.Argument("customerId", customerId);

            var request = GetCreateChargeRequest(amount, currency, customerId, description, metadata,
                capture, statementDescriptor, receiptEmail, destination, applicationFee, shipping);

            request.AddParameter("source", cardId);

            return ExecuteObject(request);
        }

        /// <summary>
        /// Creates a charge using a customer id and an optional source (Credit Card) object
        /// </summary>
        /// <param name="amount">Amount to be charged</param>
        /// <param name="currency">3-letter ISO currency code</param>
        /// <param name="source"></param>
        /// <param name="description">Description displayed on the Stripe web interface</param>
        /// <param name="metadata"></param>
        /// <param name="capture">Whether or not to immediately capture the charge. When false, the charge issues an authorization (or pre-authorization), and will need to be captured later.</param>
        /// <param name="statementDescriptor">An arbitrary string to be displayed on your customer's credit card statement</param>
        /// <param name="receiptEmail">The email address to send this charge's receipt to. The receipt will not be sent until the charge is paid.</param>
        /// <param name="destination">An account to make the charge on behalf of</param>
        /// <param name="applicationFee">Fixed amount to charge for our service to the receiver. Comes out of total amount charged</param>
        /// <param name="shipping">Shipping information for the charge. Helps prevent fraud on charges for physical goods.</param>
        /// <returns>Stripe Charge Object</returns>
        public StripeObject CreateCharge(decimal amount, string currency, ICreditCard source,
            string description = null, IDictionary<object, object> metadata = null, bool capture = true,
            string statementDescriptor = null, string receiptEmail = null, string destination = null,
            decimal? applicationFee = null, IDictionary<object, object> shipping = null)
        {
            Require.Argument("amount", amount);
            Require.Argument("currency", currency);
            Require.Argument("card", source);

            var request = GetCreateChargeRequest(amount, currency, null, description, metadata, capture,
                statementDescriptor, receiptEmail, destination, applicationFee, shipping);

            source.Validate();
            source.AddParametersToRequest_Source(request);

            return ExecuteObject(request);
        }

        private RestRequest GetCreateChargeRequest(decimal amount, string currency, string customerId,
            string description, IDictionary<object, object> metadata, bool capture, string statementDescriptor, 
            string receiptEmail, string destination, decimal? applicationFee, IDictionary<object, object> shipping)
        {
            if (amount < 0.5M)
                throw new ArgumentOutOfRangeException("amount", amount, "Amount must be at least 50 cents");

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "charges";

            request.AddParameter("amount", Convert.ToInt32(amount * 100M));
            request.AddParameter("currency", currency);
            request.AddParameter("capture", capture);
            
            if (customerId.HasValue()) request.AddParameter("customer", customerId);
            if (description.HasValue()) request.AddParameter("description", description);
            if (statementDescriptor.HasValue()) request.AddParameter("statement_descriptor", statementDescriptor);
            if (receiptEmail.HasValue()) request.AddParameter("receipt_email", receiptEmail);
            if (destination.HasValue()) request.AddParameter("destination", destination);
            if (applicationFee.HasValue) request.AddParameter("application_fee", Convert.ToInt32(applicationFee.Value * 100M));
            if (metadata != null) AddDictionaryParameter(metadata, "metadata", request);
            if (shipping != null) AddDictionaryParameter(shipping, "shipping", request);

            return request;
        }

        /// <summary>
        /// Retrieves the details of a charge that has previously been created
        /// </summary>
        /// <param name="chargeId">The identifier of the charge to be retrieved</param>
        /// <returns>Stripe Charge Object</returns>
        public StripeObject RetrieveCharge(string chargeId)
        {
            Require.Argument("chargeId", chargeId);

            var request = new RestRequest();
            request.Resource = "charges/{chargeId}";

            request.AddUrlSegment("chargeId", chargeId);

            return ExecuteObject(request);
        }

        /// <summary>
        /// Updates the specified charge by setting the values of the parameters passed
        /// </summary>
        /// <param name="chargeId">The identifier of the charge to be updated</param>
        /// <param name="description">An arbitrary string which you can attach to a charge object. It is displayed when in the web interface alongside the charge</param>
        /// <param name="metadata">A set of key/value pairs that you can attach to a charge object. It can be useful for storing additional information about the charge in a structured format</param>
        /// <param name="receiptEmail">This is the email address that the receipt for this charge will be sent to. If this field is updated, then a new email receipt will be sent to the updated address.</param>
        /// <param name="fraudDetails">A set of key/value pairs you can attach to a charge giving information about its riskiness</param>
        /// <param name="shipping">Shipping information for the charge. Helps prevent fraud on charges for physical goods. </param>
        /// <returns>A Stripe Charge Object</returns>
        public StripeObject UpdateCharge(string chargeId, string description = null, IDictionary<object, object> metadata = null,
            string receiptEmail = null, IDictionary<object, object> fraudDetails = null, IDictionary<object, object> shipping = null)
        {
            Require.Argument("chargeId", chargeId);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "charges/{chargeId}";

            request.AddUrlSegment("chargeId", chargeId);

            if (description.HasValue()) request.AddParameter("description", description);
            if (receiptEmail.HasValue()) request.AddParameter("receipt_email", receiptEmail);
            if (fraudDetails != null) AddDictionaryParameter(fraudDetails, "fraud_details", request);
            if (metadata != null) AddDictionaryParameter(metadata, "metadata", request);
            if (shipping != null) AddDictionaryParameter(shipping, "shipping", request);

            return ExecuteObject(request);
        }

        /// <summary>
        /// Capture the payment of an existing, uncaptured, charge.
        /// </summary>
        /// <param name="chargeId">The identifier of the charge to be updated</param>
        /// <param name="amount">The amount to capture, which must be less than or equal to the original amount</param>
        /// <param name="applicationFee">An application fee to add on to this charge. Can only be used with Stripe Connect</param>
        /// <param name="receiptEmail">The email address to send this charge’s receipt to. This will override the previously-specified email address for this charge, if one was set. Receipts will not be sent in test mode. </param>
        /// <param name="statementDescriptor">An arbitrary string to be displayed on your customer’s credit card statement</param>
        /// <returns>A Stripe Charge Object</returns>
        public StripeObject CaptureCharge(string chargeId, decimal? amount = null, decimal? applicationFee = null,
            string receiptEmail = null, string statementDescriptor = null)
        {
            Require.Argument("chargeId", chargeId);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "charges/{chargeId}/capture";

            request.AddUrlSegment("chargeId", chargeId);

            if (amount != null) request.AddParameter("amount", Convert.ToInt32(amount * 100M));
            if (applicationFee.HasValue) request.AddParameter("application_fee", Convert.ToInt32(applicationFee.Value * 100M));
            if (receiptEmail.HasValue()) request.AddParameter("receipt_email", receiptEmail);
            if (statementDescriptor.HasValue()) request.AddParameter("statement_descriptor", statementDescriptor);

            return ExecuteObject(request);
        }

        /* TO DO: Expand the Refund Feature */
        public StripeObject RefundCharge(string chargeId, decimal? amount = null)
        {
            Require.Argument("chargeId", chargeId);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "charges/{chargeId}/refund";

            request.AddUrlSegment("chargeId", chargeId);
            if (amount.HasValue)
            {
                if (amount.Value < 0.5M)
                    throw new ArgumentOutOfRangeException("amount", amount, "Amount must be at least 50 cents");

                request.AddParameter("amount", Convert.ToInt32(amount * 100M));
            }

            return ExecuteObject(request);
        }

        /// <summary>
        /// Returns a list of charges you've previously created. The charges are returned in sorted order, with the most recent charges appearing first. 
        /// </summary>
        /// <param name="customerId">Only return charges for the customer specified by this customer ID.</param>
        /// <param name="limit">A limit on the number of objects to be returned. Limit can range between 1 and 100 items.</param>
        /// <param name="endingBefore">A cursor for use in pagination. ending_before is an object ID that defines your place in the list.</param>
        /// <param name="startingAfter">A cursor for use in pagination. starting_after is an object ID that defines your place in the list</param>
        /// <returns>A List Of Stripe Charges</returns>
        public StripeArray ListCharges(string customerId = null, int? limit = null, string endingBefore = null,
            string startingAfter = null)
        {
            var request = new RestRequest();
            request.Resource = "charges";

            if (limit.HasValue) request.AddParameter("limit", limit.Value);
            if (endingBefore.HasValue()) request.AddParameter("ending_before", endingBefore);
            if (customerId.HasValue()) request.AddParameter("customer", customerId);
            if (startingAfter.HasValue()) request.AddParameter("starting_after", startingAfter);

            return ExecuteArray(request);
        }
    }
}
