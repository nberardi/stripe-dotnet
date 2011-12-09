﻿using System;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using Stripe.Models;

namespace Stripe
{
	public partial class StripeClient
	{
		public ChargeResponse CreateCharge(decimal amount, string currency, string customerId, string description = null)
		{
			Require.Argument("amount", amount);
			Require.Argument("currency", currency);
			Require.Argument("customerId", customerId);

			if (amount < 0.5M)
				throw new ArgumentOutOfRangeException("amount", amount, "Amount must be at least 50 cents");

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "charges";

			int inCents = Convert.ToInt32(amount * 100M);

			request.AddParameter("amount", inCents);
			request.AddParameter("currency", currency);
			request.AddParameter("customer", customerId);
			if (description.HasValue()) request.AddParameter("description", description);

			return Execute<ChargeResponse>(request);
		}

		public ChargeResponse CreateCharge(decimal amount, string currency, CreditCardRequest card, string description = null)
		{
			Require.Argument("amount", amount);
			Require.Argument("currency", currency);
			Require.Argument("card", card);
			Require.Argument("card[number]", card.Number);
			Require.Argument("card[exp_month]", card.ExpirationMonth);
			Require.Argument("card[exp_year]", card.ExpirationYear);

			if (amount < 0.5M)
				throw new ArgumentOutOfRangeException("amount", amount, "Amount must be at least 50 cents");

			Validate.IsBetween(card.ExpirationMonth, 1, 12);
			Validate.IsBetween(card.ExpirationYear, DateTime.Now.Year, 2050);

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "charges";

			int inCents = Convert.ToInt32(amount * 100M);

			request.AddParameter("amount", inCents);
			request.AddParameter("currency", currency);
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
			if (description.HasValue()) request.AddParameter("description", description);

			return Execute<ChargeResponse>(request);
		}

		public ChargeResponse RetrieveCharge(string chargeId)
		{
			Require.Argument("chargeId", chargeId);

			var request = new RestRequest();
			request.Resource = "charges/{chargeId}";

			request.AddUrlSegment("chargeId", chargeId);

			return Execute<ChargeResponse>(request);
		}

		public ChargeResponse RefundCharge(string chargeId, decimal? amount = null)
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

				int inCents = Convert.ToInt32(amount.Value * 100M);

				request.AddParameter("amount", inCents);
			}

			return Execute<ChargeResponse>(request);
		}

		public ChargeListResponse ListCharges(string customerId = null, int? count = null, int? offset = null)
		{
			var request = new RestRequest();
			request.Resource = "charges";
			
			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);
			if (customerId.HasValue()) request.AddParameter("customer", customerId);

			return Execute<ChargeListResponse>(request);
		}
	}
}
