using System;
using System.Linq;
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
			Require.Argument("card", Token);
		}

		void IObjectValidation.AddParametersToRequest(RestRequest request)
		{
			request.AddParameter("card", Token);
		}
	}
}
