using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;

namespace Stripe
{
    public partial class StripeClient
    {
        /// <summary>
        /// Creates a new subscription on an existing customer.
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="plan">The Identifier of the plan to subscribe the customer</param>
        /// <param name="coupon">The code of the coupon to apply to this subscription</param>
        /// <param name="trialEnd">Unix timestamp representing the end of the trial period the customer will get before being charged for the first time.</param>
        /// <param name="source">The source can either be a token, or a dictionary containing a user's credit card details.</param>
        /// <param name="quantity">The quantity you'd like to apply t the subscription you're creating.</param>
        /// <param name="applicationFeePercent">A positive decimal between 1 and 100. Represents the percentage of the subscription invoice subtotal that will be transferred to the application owner's Stripe account.</param>
        /// <param name="taxPercent">A positive decimal between 1 and 100. Represents the percentage of the subscription invoice subtotal that will be calculated and added as tax to the final amount each billing period.</param>
        /// <param name="metaData">A set of key/value pairs that you can attach to a subscription object.</param>
        /// <returns>The newly created subscription object if the call succeeded.</returns>
        public StripeObject CreateCustomersSubscription(string customerId, string plan,
            string coupon = null, DateTimeOffset? trialEnd = null,
            dynamic source = null, int quantity = 1, decimal? applicationFeePercent = null,
            decimal? taxPercent = null, IDictionary<object, object> metaData = null)
        {
            Require.Argument("customerId", customerId);
            Require.Argument("plan", plan);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "customers/{customerId}/subscriptions";

            request.AddUrlSegment("customerId", customerId);

            request.AddParameter("plan", plan);
            request.AddParameter("quantity", quantity);

            if (coupon.HasValue()) request.AddParameter("coupon", coupon);
            if (trialEnd.HasValue) request.AddParameter("trial_end", trialEnd);
            if (applicationFeePercent.HasValue) request.AddParameter("application_fee_percent", applicationFeePercent);
            if (taxPercent.HasValue) request.AddParameter("tax_percent", taxPercent);
            if (metaData != null) AddDictionaryParameter(metaData, "metadata", ref request);

            if (source != null)
            {
                if (source is ICreditCard)
                    source.AddParametersToRequest(request);
                else if (source is String)
                    request.AddParameter("source", source);
                else
                    throw new ArgumentException("source is of the wrong type");
            }

            return ExecuteObject(request);
        }

        /// <summary>
        /// By default, you can see the 10 most recent active subscriptions stored on a customer directly on the customer object, but you can also retrieve details about a specific active subscription for a customer.
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="subscriptionId">ID of the subscription to retrieve.</param>
        /// <returns>Returns the subscription object.</returns>
        public StripeObject RetrieveCustomersSubscription(string customerId, string subscriptionId)
        {
            Require.Argument("customerId", customerId);
            Require.Argument("subscriptionId", subscriptionId);

            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = "customers/{customerId}/subscriptions/{subscriptionId}";

            request.AddUrlSegment("customerId", customerId);
            request.AddUrlSegment("subscriptionId", subscriptionId);

            return ExecuteObject(request);
        }

        /// <summary>
        /// Updates an existing subscription on a customer to match the specified parameters.
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="subscriptionId">Subscription Id</param>
        /// <param name="plan">The identifier of the plan to update the subscription to. If omitted, the subscription will not change plans.</param>
        /// <param name="coupon">The code of the coupon to apply to the customer if you would like to apply it at the same time as updating the subscription.</param>
        /// <param name="prorate">Flag telling us whether to prorate switching plans during a billing cycle.</param>
        /// <param name="prorationDate">If set, the proration will be calculated as though the subscription was updated at the given time.</param>
        /// <param name="trialEnd">Unix timestamp representing the end of the trial period the customer will get before being charged for the first time.</param>
        /// <param name="source">The source can either be a token, or a dictionary containing a user's credit card details.</param>
        /// <param name="quantity">The quantity you'd like to apply to the subscription you're updating.</param>
        /// <param name="applicationFeePercent">A positive decimal between 1 and 100 that represents the percentage of the subscription invoice amount due each billing period that will be transferred to the application owner’s Stripe account.</param>
        /// <param name="taxPercent">Update the amount of tax applied to this subscription. </param>
        /// <param name="metaData">A set of key/value pairs that you can attach to a subscription object.</param>
        /// <returns>The newly updated subscription object if the call succeeded.</returns>
        public StripeObject UpdateCustomersSubscription(string customerId, string subscriptionId,
            string plan, string coupon = null, bool? prorate = null, DateTime? prorationDate = null,
            DateTimeOffset? trialEnd = null, dynamic source = null, int quantity = 1,
            decimal? applicationFeePercent = null, decimal? taxPercent = null, 
            IDictionary<object, object> metaData = null)
        {
            Require.Argument("customerId", customerId);
            Require.Argument("subscriptionId", subscriptionId);
            Require.Argument("plan", plan);

            if (source != null) source.Validate();

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "customers/{customerId}/subscriptions/{subscriptionId}";

            request.AddUrlSegment("customerId", customerId);
            request.AddUrlSegment("subscriptionId", subscriptionId);

            request.AddParameter("plan", plan);
            request.AddParameter("quantity", quantity);

            if (coupon.HasValue()) request.AddParameter("coupon", coupon);
            if (prorate.HasValue) request.AddParameter("prorate", prorate.Value);
            if (trialEnd.HasValue) request.AddParameter("trial_end", trialEnd.Value.ToUnixEpoch());
            if (prorationDate.HasValue) request.AddParameter("proration_date", prorationDate.Value);
            if (applicationFeePercent.HasValue) request.AddParameter("application_fee_percent", applicationFeePercent);
            if (metaData != null) AddDictionaryParameter(metaData, "metadata", ref request);

            if (taxPercent.HasValue)
            {
                if (taxPercent.Value < 0)
                    throw new ArgumentException("taxPercent must be a positive value");

                request.AddParameter("tax_percent", taxPercent);
            }

            if (source != null)
            {
                if (source is ICreditCard)
                    source.AddParametersToRequest(request);
                else if (source is String)
                    request.AddParameter("source", source);
                else
                    throw new ArgumentException("source is of the wrong type");
            }

            return ExecuteObject(request);
        }

        /// <summary>
        /// Cancels a customer’s subscription.
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="subscriptionId">Subscription Id</param>
        /// <param name="atPeriodEnd">A flag that if set to true will delay the cancellation of the subscription until the end of the current period.</param>
        /// <returns>The canceled subscription object.</returns>
        public StripeObject CancelCustomersSubscription(string customerId, string subscriptionId, bool? atPeriodEnd = null)
        {
            Require.Argument("customerId", customerId);
            Require.Argument("subscriptionId", subscriptionId);

            var request = new RestRequest();
            request.Method = Method.DELETE;
            request.Resource = "customers/{customerId}/subscriptions/{subscriptionId}";

            request.AddUrlSegment("customerId", customerId);
            request.AddUrlSegment("subscriptionId", subscriptionId);

            if (atPeriodEnd.HasValue) request.AddParameter("at_period_end", atPeriodEnd.Value);

            return ExecuteObject(request);
        }

        /// <summary>
        /// You can see a list of the customer's active subscriptions.
        /// </summary>
        /// <param name="customerId">The ID of the customer whose subscriptions will be retrieved</param>
        /// <param name="endingBefore">A cursor for use in pagination. ending_before is an object ID that defines your place in the list.</param>
        /// <param name="limit">A limit on the number of objects to be returned. Limit can range between 1 and 100 items.</param>
        /// <param name="startingAfter">A cursor for use in pagination. starting_after is an object ID that defines your place in the list.</param>
        /// <returns>Returns a list of the customer's active subscriptions. </returns>
        public StripeArray ListActiveCustomersSubscription(string customerId, string endingBefore = null, int limit = 10, string startingAfter = null)
        {
            Require.Argument("customerId", customerId);

            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = "customers/{customerId}/subscriptions";

            request.AddUrlSegment("customerId", customerId);

            request.AddQueryParameter("limit", limit.ToString());

            if (endingBefore.HasValue()) request.AddParameter("ending_before", endingBefore);
            if (startingAfter.HasValue()) request.AddParameter("starting_after", startingAfter);

            return ExecuteArray(request);
        }
    }
}
