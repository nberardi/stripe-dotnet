using System;
using System.Linq;
using RestSharp;
using RestSharp.Validation;


namespace Stripe
{
	public partial class StripeClient
	{
		public StripeObject CreateCustomer(ICreditCard card = null, string coupon = null, string email = null, string description = null, string plan = null, DateTimeOffset? trialEnd = null)
		{
			if (card != null) card.Validate();

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "customers";

			if (card != null) card.AddParametersToRequest(request);
			if (coupon.HasValue()) request.AddParameter("coupon", coupon);
			if (email.HasValue()) request.AddParameter("email", email);
			if (description.HasValue()) request.AddParameter("description", description);
			if (plan.HasValue()) request.AddParameter("plan", plan);
			if (trialEnd.HasValue) request.AddParameter("trial_end", trialEnd.Value.ToUnixEpoch());

			return ExecuteObject(request);
		}

		public StripeObject RetrieveCustomer(string customerId)
		{
			Require.Argument("customerId", customerId);

			var request = new RestRequest();
			request.Resource = "customers/{customerId}";

			request.AddUrlSegment("customerId", customerId);

			return ExecuteObject(request);
		}

		public StripeObject UpdateCustomer(string customerId, ICreditCard card = null, string coupon = null, string email = null, string description = null, int? accountBalance = null)
		{
			if (card != null) card.Validate();

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "customers/{customerId}";

			request.AddUrlSegment("customerId", customerId);

			if (card != null) card.AddParametersToRequest(request);
			if (coupon.HasValue()) request.AddParameter("coupon", coupon);
			if (email.HasValue()) request.AddParameter("email", email);
			if (description.HasValue()) request.AddParameter("description", description);
            if (accountBalance.HasValue) request.AddParameter("account_balance", accountBalance.Value);

			return ExecuteObject(request);
		}

		public StripeObject DeleteCustomer(string customerId)
		{
			Require.Argument("customerId", customerId);

			var request = new RestRequest();
			request.Method = Method.DELETE;
			request.Resource = "customers/{customerId}";

			request.AddUrlSegment("customerId", customerId);

			return ExecuteObject(request);
		}

		public StripeArray ListCustomers(int? count = null, int? offset = null)
		{
			var request = new RestRequest();
			request.Resource = "customers";

			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);

			return ExecuteArray(request);
		}
	}
}