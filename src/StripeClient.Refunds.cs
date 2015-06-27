using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Validation;

namespace Stripe
{
    public partial class StripeClient
    {
        public StripeObject CreateRefund(string chargeId, decimal? amount = null, bool refundApplicationFee = false,
            bool reverseTransfer = false, string reason = null, Dictionary<string, string> metaData = null)
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
            if (metaData != null) request.AddParameter("metadata", metaData);

            return ExecuteObject(request);
        }

        public StripeObject RetrieveRefund(string refundId, string chargeId)
        {
            Require.Argument("refundId", refundId);
            Require.Argument("chargeId", chargeId);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "charges/{chargeId}/refunds/{refundId}";

            request.AddUrlSegment("chargeId", chargeId);
            request.AddUrlSegment("refundId", refundId);

            return ExecuteObject(request);
        }

        public StripeObject UpdateRefund(string chargeId, string refundId, Dictionary<string, string> metaData = null)
        {
            Require.Argument("refundId", refundId);
            Require.Argument("chargeId", chargeId);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "charges/{chargeId}/refunds/{refundId}";

            request.AddUrlSegment("chargeId", chargeId);
            request.AddUrlSegment("refundId", refundId);

            if (metaData != null) request.AddParameter("metadata", metaData);

            return ExecuteObject(request);
        }

        public StripeArray ListRefunds(string chargeId, string endingBefore = null, int? limit = null, string startingAfter = null)
        {
            Require.Argument("chargeId", chargeId);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "charges/{chargeId}/refunds";

            request.AddUrlSegment("chargeId", chargeId);

            if (endingBefore.HasValue()) request.AddParameter("ending_before", endingBefore);
            if (limit.HasValue) request.AddParameter("limit", limit);
            if (startingAfter.HasValue()) request.AddParameter("starting_after", startingAfter);

            return ExecuteArray(request);
        }
    }
}
