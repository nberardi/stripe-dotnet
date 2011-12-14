﻿using System;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using Stripe.Models;

namespace Stripe
{
	public partial class StripeClient
	{
		public StripeCharge CreateCharge(decimal amount, string currency, string customerId, string description = null)
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

			return Execute<StripeCharge>(request);
		}

		public StripeCharge CreateCharge(decimal amount, string currency, ICreditCard card, string description = null)
		{
			Require.Argument("amount", amount);
			Require.Argument("currency", currency);
			Require.Argument("card", card);
			card.Validate();

			if (amount < 0.5M)
				throw new ArgumentOutOfRangeException("amount", amount, "Amount must be at least 50 cents");

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "charges";

			request.AddParameter("amount", Convert.ToInt32(amount * 100M));
			request.AddParameter("currency", currency);
			card.AddParametersToRequest(request);
			if (description.HasValue()) request.AddParameter("description", description);

			return Execute<StripeCharge>(request);
		}

		public StripeCharge RetrieveCharge(string chargeId)
		{
			Require.Argument("chargeId", chargeId);

			var request = new RestRequest();
			request.Resource = "charges/{chargeId}";

			request.AddUrlSegment("chargeId", chargeId);

			return Execute<StripeCharge>(request);
		}

		public StripeCharge RefundCharge(string chargeId, decimal? amount = null)
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

			return Execute<StripeCharge>(request);
		}

		public StripeList<StripeCharge> ListCharges(string customerId = null, int? count = null, int? offset = null)
		{
			var request = new RestRequest();
			request.Resource = "charges";
			
			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);
			if (customerId.HasValue()) request.AddParameter("customer", customerId);

			return Execute<StripeList<StripeCharge>>(request);
		}
	}
}
