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
		public CustomerSubscriptionResponse UpdateCustomersSubscription(string customerId, string planId, string coupon = null, bool? prorate = null, DateTimeOffset? trialEnd = null, CreditCardRequest card = null)
		{
			Require.Argument("customerId", customerId);
			Require.Argument("planId", planId);

			if (card != null)
			{
				Require.Argument("card[number]", card.Number);
				Require.Argument("card[exp_month]", card.ExpMonth);
				Require.Argument("card[exp_year]", card.ExpYear);
			}

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "customers/{customerId}/subscription";

			request.AddUrlSegment("customerId", customerId);

			request.AddParameter("plan", planId);
			if (coupon.HasValue()) request.AddParameter("coupon", coupon);
			if (prorate.HasValue) request.AddParameter("prorate", prorate.Value);
			if (trialEnd.HasValue) request.AddParameter("trial_end", trialEnd.Value.ToUnixEpoch());
			if (card != null)
			{
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
			}

			return Execute<CustomerSubscriptionResponse>(request);
		}

		public CustomerSubscriptionResponse CancelCustomersSubscription(string customerId, bool? atPeriodEnd = null)
		{
			Require.Argument("customerId", customerId);

			var request = new RestRequest();
			request.Method = Method.DELETE;
			request.Resource = "customers/{customerId}/subscription";

			request.AddUrlSegment("customerId", customerId);

			if (atPeriodEnd.HasValue) request.AddParameter("at_period_end", atPeriodEnd.Value);

			return Execute<CustomerSubscriptionResponse>(request);
		}
	}
}
