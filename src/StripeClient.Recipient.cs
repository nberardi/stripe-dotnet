using System;
using System.Linq;
using RestSharp;
using RestSharp.Validation;

namespace Stripe
{
    public partial class StripeClient
    {
        public StripeObject CreateRecipient(string name, string type,
            ICreditCard card = null, IBankAccount bankAccount = null, 
            string taxId = null, string email = null, string description = null)
        {
            Require.Argument("name", name);
            Require.Argument("type", type);

            if (card != null) card.Validate();
            if (bankAccount != null) bankAccount.Validate();

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "recipients";

            request.AddParameter("name", name);
            request.AddParameter("type", type);

            if (taxId.HasValue()) request.AddParameter("tax_id", taxId);
            if (email.HasValue()) request.AddParameter("email", email);
            if (description.HasValue()) request.AddParameter("description", description);
            if (card != null) card.AddParametersToRequest_Old(request);
            if (bankAccount != null) bankAccount.AddParametersToRequest(request);

            return ExecuteObject(request);
        }

        public StripeObject RetrieveRecipient(string recipientId)
        {
            Require.Argument("recipientId", recipientId);

            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = "recipients/{recipientId}";

            request.AddUrlSegment("recipientId", recipientId);

            return ExecuteObject(request);
        }

        public StripeObject UpdateRecipient(string recipientId, string name = null,
            string taxId = null, IBankAccount bankAccount = null, ICreditCard card = null,
            string defaultCardId = null, string email = null, string description = null)
        {
            Require.Argument("recipientId", recipientId);

            if (card != null) card.Validate();
            if (bankAccount != null) bankAccount.Validate();

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "recipients/{recipientId}";

            request.AddUrlSegment("recipientId", recipientId);

            if (name.HasValue()) request.AddParameter("name", name);
            if (taxId.HasValue()) request.AddParameter("tax_id", taxId);
            if (defaultCardId.HasValue()) request.AddParameter("default_card", defaultCardId);
            if (email.HasValue()) request.AddParameter("email", email);
            if (description.HasValue()) request.AddParameter("description", description);
            if (bankAccount != null) bankAccount.AddParametersToRequest(request);
            if (card != null) card.AddParametersToRequest_Old(request);

            return ExecuteObject(request);
        }

        public StripeObject DeleteRecipient(string recipientId)
        {
            Require.Argument("recipientId", recipientId);

            var request = new RestRequest();
            request.Method = Method.DELETE;
            request.Resource = "recipients/{recipientId}";

            request.AddUrlSegment("recipientId", recipientId);

            return ExecuteObject(request);
        }

        public StripeObject ListRecipients(int limit = 10, string endingBefore = null,
            string startingAfter = null, string type = null, string verified = null)
        {
            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = "recipients";

            if (limit != 10) request.AddParameter("limit", limit);
            if (endingBefore.HasValue()) request.AddParameter("ending_before", endingBefore);
            if (startingAfter.HasValue()) request.AddParameter("starting_after", startingAfter);
            if (type.HasValue()) request.AddParameter("type", type);
            if (verified.HasValue()) request.AddParameter("verified", verified);

            return ExecuteObject(request);
        }
    }
}
