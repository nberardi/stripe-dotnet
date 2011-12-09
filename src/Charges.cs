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

			request.AddParameter("amount", Convert.ToInt32(amount * 100M));
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
			Require.Argument("card[exp_month]", card.ExpMonth);
			Require.Argument("card[exp_year]", card.ExpYear);

			if (amount < 0.5M)
				throw new ArgumentOutOfRangeException("amount", amount, "Amount must be at least 50 cents");

			Validate.IsBetween(card.ExpMonth, 1, 12);
			Validate.IsBetween(card.ExpYear, DateTime.Now.Year, 2050);

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "charges";

			request.AddParameter("amount", Convert.ToInt32(amount * 100M));
			request.AddParameter("currency", currency);
			request.AddParameter("card[number]", card.Number);
			request.AddParameter("card[exp_month]", card.ExpMonth);
			request.AddParameter("card[exp_year]", card.ExpYear);
			if (card.Cvc.HasValue()) request.AddParameter("card[cvc]", card.ExpYear);
			if (card.Name.HasValue()) request.AddParameter("card[name]", card.ExpYear);
			if (card.AddressLine1.HasValue()) request.AddParameter("card[address_line1]", card.ExpYear);
			if (card.AddressLine2.HasValue()) request.AddParameter("card[address_line2]", card.ExpYear);
			if (card.AddressZip.HasValue()) request.AddParameter("card[address_zip]", card.ExpYear);
			if (card.AddressState.HasValue()) request.AddParameter("card[address_state]", card.ExpYear);
			if (card.AddressCountry.HasValue()) request.AddParameter("card[address_country]", card.ExpYear);
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

				request.AddParameter("amount", Convert.ToInt32(amount * 100M));
			}

			return Execute<ChargeResponse>(request);
		}

		public ListResponse<ChargeResponse> ListCharges(string customerId = null, int? count = null, int? offset = null)
		{
			var request = new RestRequest();
			request.Resource = "charges";
			
			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);
			if (customerId.HasValue()) request.AddParameter("customer", customerId);

			return Execute<ListResponse<ChargeResponse>>(request);
		}
	}
}
