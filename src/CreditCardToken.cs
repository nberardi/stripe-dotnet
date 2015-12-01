using System;
using RestSharp;
using RestSharp.Validation;

namespace Stripe
{
    public class CreditCardToken : ICreditCard
    {
        public CreditCardToken(string token)
        {
            this.Token = token;
        }

        public string Token { get; set; }

        void IObjectValidation.Validate()
        {
            Require.Argument("source", Token);
        }

        public void Validate_Old()
        {
            Require.Argument("card", Token);
        }

        void IObjectValidation.AddParametersToRequest(RestRequest request)
        {
            request.AddParameter("external_account", Token);
        }

        public void AddParametersToRequest_Card(RestRequest request)
        {
            request.AddParameter("card", Token);
        }

        public void AddParametersToRequest_Source(RestRequest request)
        {
            request.AddParameter("source", Token);
        }

        public void AddParametersToRequest_Update(RestRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
