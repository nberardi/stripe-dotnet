using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Validation;

namespace Stripe
{
    public partial class StripeClient 
    {
        /// <summary>
        /// Create a transfer from your stripe account to a 3rd party
        /// </summary>
        /// <param name="amount">Amount</param>
        /// <param name="currency">3-letter ISO code for currency</param>
        /// <param name="destination">Card or bank account id</param>
        /// <param name="sourceTransaction">Charge (or other transaction) id to transfer funds from before being added to your available balance</param>
        /// <param name="description">Description of the transfer to be displayed in the web interface</param>
        /// <param name="statementDescriptor">Statement description to appear on the recipient's bank or card statement</param>
        /// <param name="metaData">Useful for storing additional information about the transfer in a structured format</param>
        /// <returns>Stripe Transfers Object</returns>
		public StripeObject CreateTransfer(string amount, string currency, string destination, 
			string sourceTransaction = null, string description = null, string statementDescriptor = null,
			Dictionary<string, string> metaData = null)
        {
            var request = new RestRequest() { Method = Method.POST, Resource = "transfers" };

            request.AddParameter("amount", amount);
            request.AddParameter("currency", currency);
            request.AddParameter("destination", destination);

            if (sourceTransaction.HasValue()) request.AddParameter("source_transaction", sourceTransaction);
            if (description.HasValue()) request.AddParameter("description", description);
            if (statementDescriptor.HasValue()) request.AddParameter("statement_descriptor", statementDescriptor);
            if (metaData != null) request.AddParameter("metadata", metaData);

            return ExecuteObject(request);
        }

        /// <summary>
        /// Retrieves the details of an existing transfer
        /// </summary>
        /// <param name="transferId">Id of the transfer to be retrieved</param>
        /// <returns>Stripe Transfers Object</returns>
        public StripeObject RetrieveTransfer(string transferId)
        {
            var request = new RestRequest() { Method = Method.GET, Resource = "transfers/{transferId}" };

            request.AddUrlSegment("transferId", transferId);

            return ExecuteObject(request);
        }

        /// <summary>
        /// Updates the specified transfer by setting the values of the parameters passed
        /// </summary>
        /// <param name="transferId">Id of the transfer to be updated</param>
        /// <param name="description">Description of the transfer to be displayed in the web interface</param>
        /// <param name="metaData">Useful for storing additional information about the transfer in a structured format</param>
        /// <returns>Stripe Transfers Object</returns>
		public StripeObject UpdateTransfer(string transferId, string description = null,
			Dictionary<string, string> metaData = null)
        {
            var request = new RestRequest() { Method = Method.POST, Resource = "transfers/{transferId}" };

            request.AddUrlSegment("transferId", transferId);

            if (description.HasValue()) request.AddParameter("description", description);
            if (metaData != null) request.AddParameter("metadata", metaData);

            return ExecuteObject(request);
        }
    }
}
