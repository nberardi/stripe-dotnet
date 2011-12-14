using System;
using System.Linq;
using RestSharp;
using RestSharp.Validation;
using Stripe.Models;

namespace Stripe
{
	public partial class StripeClient
	{
		public StripeCustomer CreateCustomer(ICreditCard card = null, string coupon = null, string email = null, string description = null, string plan = null, DateTimeOffset? trialEnd = null)
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
			if (trialEnd.HasValue) request.AddParameter("trialEnd", trialEnd.Value.ToUnixEpoch());

			return Execute<StripeCustomer>(request);
		}

		public StripeCustomer RetrieveCustomer(string customerId)
		{
			Require.Argument("customerId", customerId);

			var request = new RestRequest();
			request.Resource = "customers/{customerId}";

			request.AddUrlSegment("customerId", customerId);

			return Execute<StripeCustomer>(request);
		}

		public StripeCustomer UpdateCustomer(string customerId, ICreditCard card = null, string coupon = null, string email = null, string description = null)
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

			return Execute<StripeCustomer>(request);
		}

		public StripeCustomer DeleteCustomer(string customerId)
		{
			Require.Argument("customerId", customerId);

			var request = new RestRequest();
			request.Method = Method.DELETE;
			request.Resource = "customers/{customerId}";

			request.AddUrlSegment("customerId", customerId);

			return Execute<StripeCustomer>(request);
		}

		public StripeList<StripeCustomer> ListCustomers(int? count = null, int? offset = null)
		{
			var request = new RestRequest();
			request.Resource = "customers";

			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);

			return Execute<StripeList<StripeCustomer>>(request);
		}
	}
}