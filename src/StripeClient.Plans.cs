using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using RestSharp.Validation;

namespace Stripe
{
	public partial class StripeClient
	{
        /// <summary>
        /// You can create plans easily via the plan management page of the Stripe dashboard. Plan creation is also accessible via the API if you need to create plans on the fly.
        /// </summary>
        /// <param name="planId">Unique string of your choice that will be used to identify this plan when subscribing a customer.</param>
        /// <param name="amount">A positive integer in cents (or 0 for a free plan) representing how much to charge (on a recurring basis).</param>
        /// <param name="currency">3-letter ISO code for currency.</param>
        /// <param name="interval">Specifies billing frequency. Either day, week, month or year.</param>
        /// <param name="name">Name of the plan, to be displayed on invoices and in the web interface.</param>
        /// <param name="trialPeriodDays">Specifies a trial period in (an integer number of) days. </param>
        /// <param name="intervalCount">The number of intervals between each subscription billing. For example, interval=month and interval_count=3 bills every 3 months.</param>
        /// <param name="statementDescriptor">An arbitrary string to be displayed on your customer’s credit card statement.</param>
        /// <param name="metaData">A set of key/value pairs that you can attach to a plan object.</param>
        /// <returns>Returns the plan object.</returns>
		public StripeObject CreatePlan(string planId, decimal amount, string currency, PlanFrequency interval, 
            string name, int? trialPeriodDays = null, int? intervalCount = 1, string statementDescriptor = null,
            IDictionary<object, object> metaData = null)
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

            if (intervalCount.HasValue) request.AddParameter("interval_count", intervalCount);
            if (trialPeriodDays.HasValue) request.AddParameter("trial_period_days", trialPeriodDays);
            if (statementDescriptor.HasValue()) request.AddParameter("statement_descriptor", statementDescriptor);
            if (metaData != null) AddDictionaryParameter(metaData, "metadata", request);

			return ExecuteObject(request);
		}

        /// <summary>
        /// Retrieves the plan with the given ID.
        /// </summary>
        /// <param name="planId">The ID of the desired plan. </param>
        /// <returns>Returns a plan if a valid plan ID was provided. Returns an error otherwise.</returns>
		public StripeObject RetreivePlan(string planId)
		{
			Require.Argument("planId", planId);

			var request = new RestRequest();
			request.Resource = "plans/{planId}";

			request.AddUrlSegment("planId", planId);

			return ExecuteObject(request);
		}

        /// <summary>
        /// You can delete plans via the plan management page of the Stripe dashboard. However, deleting a plan does not affect any current subscribers to the plan; it merely means that new subscribers can’t be added to that plan. You can also delete plans via the API.
        /// </summary>
        /// <param name="planId">The identifier of the plan to be deleted.</param>
        /// <returns>An object with the deleted plan’s ID and a deleted flag upon success. Otherwise, this call returns an error, such as if the plan has already been deleted.</returns>
		public StripeObject DeletePlan(string planId)
		{
			Require.Argument("planId", planId);

			var request = new RestRequest();
			request.Method = Method.DELETE;
			request.Resource = "plans/{planId}";

			request.AddUrlSegment("planId", planId);

			return ExecuteObject(request);
		}

        /// <summary>
        /// Returns a list of your plans.
        /// </summary>
        /// <param name="created">A filter on the list based on the object created field.</param>
        /// <param name="endingBefore">A cursor for use in pagination. ending_before is an object ID that defines your place in the list.</param>
        /// <param name="limit">A limit on the number of objects to be returned. Limit can range between 1 and 100 items.</param>
        /// <param name="startingAfter">A cursor for use in pagination. starting_after is an object ID that defines your place in the list.</param>
        /// <returns>A dictionary with a data property that contains an array of up to limit plans, starting after plan starting_after. Each entry in the array is a separate plan object. If no more plans are available, the resulting array will be empty.</returns>
		public StripeArray ListPlans(IDictionary<object, object> created = null, string endingBefore = null, int? limit = 10, string startingAfter = null)
		{
			var request = new RestRequest();
			request.Resource = "plans";
            request.Method = Method.GET;

            if (limit.HasValue) request.AddQueryParameter("limit", limit.ToString());
            if (created != null) AddDictionaryParameter(created, "created", request);
            if (endingBefore.HasValue()) request.AddParameter("ending_before", endingBefore);
            if (startingAfter.HasValue()) request.AddParameter("starting_after", startingAfter);

			return ExecuteArray(request);
		}

        /// <summary>
        /// Updates the name of a plan. Other plan details (price, interval, etc.) are, by design, not editable.
        /// </summary>
        /// <param name="planId">The identifier of the plan to be updated.</param>
        /// <param name="name">Name of the plan, to be displayed on invoices and in the web interface.</param>
        /// <param name="statementDescriptor">An arbitrary string to be displayed on your customer’s credit card statement. This may be up to 22 characters.</param>
        /// <param name="metadata">A set of key/value pairs that you can attach to a plan object.</param>
        /// <returns>The updated plan object is returned upon success.</returns>
        public StripeObject UpdatePlan(string planId, string name = null, string statementDescriptor = null, IDictionary<object, object> metadata = null)
        {
            Require.Argument("planId", planId);

            var request = new RestRequest();
            request.Resource = "plans/{planId}";
            request.Method = Method.POST;

            request.AddUrlSegment("planId", planId);

            if (name.HasValue()) request.AddParameter("name", name);
            if (statementDescriptor.HasValue()) request.AddParameter("statement_descriptor", statementDescriptor);
            if (metadata != null) AddDictionaryParameter(metadata, "metadata", request);

            return ExecuteObject(request);
        }
	}
}
