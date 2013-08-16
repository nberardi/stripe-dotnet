﻿using System;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;


namespace Stripe
{
	public partial class StripeClient
	{
        /// <summary>
        /// Creates a charge using a token retrieved via the browser
        /// </summary>
		/// <param name="application_fee">Fixed amount to charge for our service to the receiver. Comes out of total amount charged</param>
        /// <returns></returns>
		public StripeObject CreateChargeWithToken(decimal amount, string token, string currency = "usd", string description = null, decimal? application_fee = null)
        {
            Require.Argument("amount", amount);
            Require.Argument("currency", currency);
            Require.Argument("token", token);

            if (amount < 0.5M)
                throw new ArgumentOutOfRangeException("amount", amount, "Amount must be at least 50 cents");

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "charges";

            request.AddParameter("amount", Convert.ToInt32(amount * 100M));
            request.AddParameter("currency", currency);
            request.AddParameter("card", token);
            if (description.HasValue()) 
				request.AddParameter("description", description);

			if (application_fee.HasValue)
			{
				request.AddParameter("application_fee",  Convert.ToInt32(application_fee.Value * 100M));
			}

            return ExecuteObject(request);
        }
        
        public StripeObject CreateCharge(decimal amount, string currency, string customerId, string description = null)
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

			return ExecuteObject(request);
		}

		public StripeObject CreateCharge(decimal amount, string currency, ICreditCard card, string description = null)
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

			return ExecuteObject(request);
		}

		public StripeObject RetrieveCharge(string chargeId)
		{
			Require.Argument("chargeId", chargeId);

			var request = new RestRequest();
			request.Resource = "charges/{chargeId}";

			request.AddUrlSegment("chargeId", chargeId);

			return ExecuteObject(request);
		}

		public StripeObject RefundCharge(string chargeId, decimal? amount = null)
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

			return ExecuteObject(request);
		}

		public StripeArray ListCharges(string customerId = null, int? count = null, int? offset = null)
		{
			var request = new RestRequest();
			request.Resource = "charges";
			
			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);
			if (customerId.HasValue()) request.AddParameter("customer", customerId);

			return ExecuteArray(request);
		}
	}
}
