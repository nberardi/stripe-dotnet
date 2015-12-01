using System;
using System.Linq;
using RestSharp;
using RestSharp.Validation;


namespace Stripe
{
    public partial class StripeClient
    {
        public StripeObject CreateCustomerToken(string customerId)
        {
            Require.Argument("customerId", customerId);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "tokens";

            request.AddParameter("customer", customerId);

            return ExecuteObject(request);
        }

        public StripeObject CreateBankAccountToken(BankAccount bankAccount)
        {
            Require.Argument("bankAccount", bankAccount);
            ((IObjectValidation)bankAccount).Validate();

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "tokens";

            ((IObjectValidation)bankAccount).AddParametersToRequest(request);

            return ExecuteObject(request);
        }

        public StripeObject CreateCardToken(ICreditCard card)
        {
            Require.Argument("card", card);
            card.Validate();

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "tokens";

            card.AddParametersToRequest_Card(request);

            return ExecuteObject(request);
        }

        [Obsolete("Please use RetreiveToken instead of RetreiveCardToken.", error: false)]
        public StripeObject RetrieveCardToken(string tokenId)
        {
            return RetreiveToken(tokenId);
        }

        public StripeObject RetreiveToken(string tokenId)
        {
            Require.Argument("tokenId", tokenId);

            var request = new RestRequest();
            request.Resource = "tokens/{tokenId}";

            request.AddUrlSegment("tokenId", tokenId);

            return ExecuteObject(request);
        }
    }
}
