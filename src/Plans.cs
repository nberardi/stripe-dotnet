using System;
using System.Linq;
using RestSharp;
using RestSharp.Validation;
using Stripe.Models;

namespace Stripe
{
	public partial class StripeClient
	{
		public PlanResponse CreatePlan(string planId, decimal amount, string currency, PlanFrequency interval, string name, int? trialPeriodDays = null)
		{
			Require.Argument("planId", planId);
			Require.Argument("amount", amount);
			Require.Argument("currency", currency);
			Require.Argument("interval", interval);
			Require.Argument("name", interval);

			if (trialPeriodDays.HasValue)
				Validate.IsBetween(trialPeriodDays.Value, 0, Int32.MaxValue);

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "plans";

			int inCents = Convert.ToInt32(amount * 100M);

			request.AddParameter("id", planId);
			request.AddParameter("amount", inCents);
			request.AddParameter("currency", currency);
			request.AddParameter("interval", interval.ToString().ToLowerInvariant());
			request.AddParameter("name", name);
			if (trialPeriodDays.HasValue) request.AddParameter("trial_period_days", trialPeriodDays);

			return Execute<PlanResponse>(request);
		}

		public PlanResponse RetreivePlan(string planId)
		{
			Require.Argument("planId", planId);

			var request = new RestRequest();
			request.Resource = "plans/{planId}";

			request.AddUrlSegment("planId", planId);

			return Execute<PlanResponse>(request);
		}

		public DeletedPlanResponse DeletePlan(string planId)
		{
			Require.Argument("planId", planId);

			var request = new RestRequest();
			request.Method = Method.DELETE;
			request.Resource = "plans/{planId}";

			request.AddUrlSegment("planId", planId);

			return Execute<DeletedPlanResponse>(request);
		}

		public ListResponse<PlanResponse> ListPlans(int? count = null, int? offset = null)
		{
			var request = new RestRequest();
			request.Resource = "plans";

			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);

			return Execute<ListResponse<PlanResponse>>(request);
		}
	}
}
