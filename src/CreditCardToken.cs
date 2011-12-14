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

		void ICreditCard.Validate()
		{
			Require.Argument("card", Token);
		}

		void ICreditCard.AddParametersToRequest(RestRequest request)
		{
			request.AddParameter("card", Token);
		}
	}
}
