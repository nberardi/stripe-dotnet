using System;
using System.Linq;
using RestSharp;
using RestSharp.Validation;


namespace Stripe
{
	public partial class StripeClient
	{
		public StripeObject CreateCardToken(CreditCard card, decimal? amount = null, string currency = null)
		{
			Require.Argument("card", card);
			((ICreditCard)card).Validate();

			if (amount.HasValue || currency.HasValue())
			{
				Require.Argument("amount", amount);
				Require.Argument("currency", currency);

				if (amount < 0.5M)
					throw new ArgumentOutOfRangeException("amount", amount, "Amount must be at least 50 cents");
			}

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "tokens";

			if (amount.HasValue) request.AddParameter("amount", Convert.ToInt32(amount * 100M));
			if (currency.HasValue()) request.AddParameter("currency", currency);
			((ICreditCard)card).AddParametersToRequest(request);

			return ExecuteObject(request);
		}

		public StripeObject RetrieveCardToken(string tokenId)
		{
			Require.Argument("tokenId", tokenId);

			var request = new RestRequest();
			request.Resource = "tokens/{tokenId}";

			request.AddUrlSegment("tokenId", tokenId);

			return ExecuteObject(request);
		}
	}
}
