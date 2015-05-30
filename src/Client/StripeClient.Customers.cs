using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using RestSharp.Validation;


namespace Stripe
{
	public partial class StripeClient
	{
        public StripeObject CreateCustomer(ICreditCard card = null, string coupon = null,
            string email = null, string description = null, string plan = null,
            DateTimeOffset? trialEnd = null, int? accountBalance = default(int),
            IDictionary<string, string> metaData = null, int? quantity = null,
            decimal? taxPercent = null)
		{
			if (card != null) card.Validate();

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "customers";

			if (coupon.HasValue()) request.AddParameter("coupon", coupon);
			if (email.HasValue()) request.AddParameter("email", email);
			if (description.HasValue()) request.AddParameter("description", description);
			if (plan.HasValue()) request.AddParameter("plan", plan);
			if (trialEnd.HasValue) request.AddParameter("trial_end", trialEnd.Value.ToUnixEpoch());
            if (metaData != null) request.AddParameter("metadata", metaData);
            if (accountBalance.HasValue) request.AddParameter("account_balance", accountBalance);
            if (quantity > 1) request.AddParameter("quantity", quantity);
            if (taxPercent.HasValue) request.AddParameter("tax_percent", taxPercent);
            //if (card != null) request.AddBody(card);
            if (card != null) card.AddParametersToRequest(request);

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

		public StripeObject UpdateCustomer(string customerId, ICreditCard card = null, 
            string coupon = null, string email = null, string description = null, 
            int? accountBalance = null, string defaultSource = null,
            IDictionary<string, string> metaData = null)
		{
            Require.Argument("customerId", customerId);

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
            if (defaultSource.HasValue()) request.AddParameter("default_source", defaultSource);
            if (metaData != null) request.AddParameter("metadata", metaData);

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