using System;
using System.Linq;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using Stripe.Models;

namespace Stripe
{
	public partial class StripeClient
	{
		public TokenResponse CreateCardToken(CreditCardRequest card, decimal? amount = null, string currency = null)
		{
			Require.Argument("card", card);
			Require.Argument("card[number]", card.Number);
			Require.Argument("card[exp_month]", card.ExpirationMonth);
			Require.Argument("card[exp_year]", card.ExpirationYear);

			if (amount.HasValue || currency.HasValue())
			{
				Require.Argument("amount", amount);
				Require.Argument("currency", currency);

				if (amount < 0.5M)
					throw new ArgumentOutOfRangeException("amount", amount, "Amount must be at least 50 cents");
			}

			Validate.IsBetween(card.ExpirationMonth, 1, 12);
			Validate.IsBetween(card.ExpirationYear, DateTime.Now.Year, 2050);

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "tokens";

			if (amount.HasValue) request.AddParameter("amount", Convert.ToInt32(amount * 100M));
			if (currency.HasValue()) request.AddParameter("currency", currency);
			request.AddParameter("card[number]", card.Number);
			request.AddParameter("card[exp_month]", card.ExpirationMonth);
			request.AddParameter("card[exp_year]", card.ExpirationYear);
			if (card.Cvc.HasValue()) request.AddParameter("card[cvc]", card.ExpirationYear);
			if (card.Name.HasValue()) request.AddParameter("card[name]", card.ExpirationYear);
			if (card.AddressLine1.HasValue()) request.AddParameter("card[address_line1]", card.ExpirationYear);
			if (card.AddressLine2.HasValue()) request.AddParameter("card[address_line2]", card.ExpirationYear);
			if (card.AddressZip.HasValue()) request.AddParameter("card[address_zip]", card.ExpirationYear);
			if (card.AddressState.HasValue()) request.AddParameter("card[address_state]", card.ExpirationYear);
			if (card.AddressCountry.HasValue()) request.AddParameter("card[address_country]", card.ExpirationYear);

			return Execute<TokenResponse>(request);
		}

		public TokenResponse RetrieveToken(string tokenId)
		{
			Require.Argument("tokenId", tokenId);

			var request = new RestRequest();
			request.Resource = "tokens/{tokenId}";

			request.AddUrlSegment("tokenId", tokenId);

			return Execute<TokenResponse>(request);
		}
	}
}
