using System;
using System.Linq;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;


namespace Stripe
{
	public partial class StripeClient
	{
		public StripeObject UpdateCustomersSubscription(string customerId, string planId, string coupon = null, bool? prorate = null, DateTimeOffset? trialEnd = null, ICreditCard card = null)
		{
			Require.Argument("customerId", customerId);
			Require.Argument("planId", planId);

			if (card != null) card.Validate();

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "customers/{customerId}/subscription";

			request.AddUrlSegment("customerId", customerId);

			request.AddParameter("plan", planId);
			if (coupon.HasValue()) request.AddParameter("coupon", coupon);
			if (prorate.HasValue) request.AddParameter("prorate", prorate.Value);
			if (trialEnd.HasValue) request.AddParameter("trial_end", trialEnd.Value.ToUnixEpoch());
			if (card != null) card.AddParametersToRequest(request);

			return ExecuteObject(request);
		}

		public StripeObject CancelCustomersSubscription(string customerId, bool? atPeriodEnd = null)
		{
			Require.Argument("customerId", customerId);

			var request = new RestRequest();
			request.Method = Method.DELETE;
			request.Resource = "customers/{customerId}/subscription";

			request.AddUrlSegment("customerId", customerId);

			if (atPeriodEnd.HasValue) request.AddParameter("at_period_end", atPeriodEnd.Value);

			return ExecuteObject(request);
		}
	}
}
