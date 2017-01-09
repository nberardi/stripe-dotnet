using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Validation;

namespace Stripe
{
    public partial class StripeClient
    {
        /// <summary>
        /// Creating a new refund will refund a charge that has previously been created but not yet refunded. Funds will be refunded to the credit or debit card that was originally charged. The fees you were originally charged are also refunded.
        /// </summary>
        /// <param name="chargeId">The identifier of the charge to refund.</param>
        /// <param name="amount">A positive integer in cents representing how much of this charge to refund.</param>
        /// <param name="refundApplicationFee">Boolean indicating whether the application fee should be refunded when refunding this charge.</param>
        /// <param name="reverseTransfer">Boolean indicating whether the transfer should be reversed when refunding this charge.</param>
        /// <param name="reason">String indicating the reason for the refund. If set, possible values are duplicate, fraudulent, and requested_by_customer.</param>
        /// <param name="metaData">A set of key/value pairs that you can attach to a refund object.</param>
        /// <returns>Returns the refund object if the refund succeeded.</returns>
        public StripeObject CreateRefund(string chargeId, decimal? amount = null, bool refundApplicationFee = false,
            bool reverseTransfer = false, string reason = null, Dictionary<object, object> metaData = null)
        {
            Require.Argument("chargeId", chargeId);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "charges/{chargeId}/refunds";

            request.AddUrlSegment("chargeId", chargeId);

            request.AddParameter("refund_application_fee", refundApplicationFee);
            request.AddParameter("reverse_transfer", reverseTransfer);

            if (amount.HasValue) request.AddParameter("amount", Convert.ToInt32(amount * 100M));
            if (reason.HasValue()) request.AddParameter("reason", reason);
            if (metaData != null) AddDictionaryParameter(metaData, "metadata", request);

            return ExecuteObject(request);
        }

        /// <summary>
        /// Retrieves the details of an existing refund.
        /// </summary>
        /// <param name="refundId">ID of refund to retrieve.</param>
        /// <param name="chargeId"></param>
        /// <returns>Returns a refund if a valid ID was provided.</returns>
        public StripeObject RetrieveRefund(string refundId)
        {
            Require.Argument("refundId", refundId);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "refunds/{refundId}";

            request.AddUrlSegment("refundId", refundId);

            return ExecuteObject(request);
        }

        /// <summary>
        /// Updates the specified refund by setting the values of the parameters passed. Any parameters not provided will be left unchanged.
        /// </summary>
        /// <param name="refundId">ID of refund to retrieve.</param>
        /// <param name="metaData">A set of key/value pairs that you can attach to a refund object.</param>
        /// <returns>Returns the refund object if the update succeeded. </returns>
        public StripeObject UpdateRefund(string refundId, Dictionary<object, object> metaData = null)
        {
            Require.Argument("refundId", refundId);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "refunds/{refundId}";

            request.AddUrlSegment("refundId", refundId);

            if (metaData != null) AddDictionaryParameter(metaData, "metadata", request);

            return ExecuteObject(request);
        }

        /// <summary>
        /// Returns a list of all refunds you’ve previously created. The refunds are returned in sorted order, with the most recent refunds appearing first. For convenience, the 10 most recent refunds are always available by default on the charge object.
        /// </summary>
        /// <param name="chargeId">Only return refunds for the charge specified by this charge ID.</param>
        /// <param name="endingBefore">A cursor for use in pagination. ending_before is an object ID that defines your place in the list.</param>
        /// <param name="limit">A limit on the number of objects to be returned. Limit can range between 1 and 100 items.</param>
        /// <param name="startingAfter">A cursor for use in pagination. starting_after is an object ID that defines your place in the list.</param>
        /// <returns>A dictionary with a data property that contains an array of up to limit refunds, starting after refund starting_after. Each entry in the array is a separate refund object.</returns>
        public StripeArray ListRefunds(string chargeId = null, string endingBefore = null, int limit = 10, string startingAfter = null)
        {
            Require.Argument("chargeId", chargeId);

            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = "refunds";

            request.AddUrlSegment("chargeId", chargeId);
            request.AddParameter("limit", limit, ParameterType.QueryString);

            if (endingBefore.HasValue()) request.AddParameter("ending_before", endingBefore);
            if (startingAfter.HasValue()) request.AddParameter("starting_after", startingAfter);

            return ExecuteArray(request);
        }
    }
}
