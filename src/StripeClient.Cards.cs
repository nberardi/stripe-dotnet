using System;
using System.Linq;
using RestSharp;
using RestSharp.Validation;

namespace Stripe
{
    public partial class StripeClient
    {
        public StripeObject CreateCard(string customerOrRecipientId, ICreditCard card, bool isRecipient = false)
        {
            Require.Argument("customerOrRecipientId", customerOrRecipientId);
            Require.Argument("card", card);

            if (card != null)
            {
                card.Validate();
            }

            var request = new RestRequest();

            request.Method = Method.POST;
            request.Resource = string.Format("{0}/{{customerOrRecipientId}}/cards", isRecipient ? "recipients" : "customers");

            request.AddUrlSegment("customerOrRecipientId", customerOrRecipientId);

            card.AddParametersToRequest_Source(request);

            return ExecuteObject(request);
        }

        public StripeObject RetrieveCard(string customerOrRecipientId, string cardId, bool isRecipient = false)
        {
            Require.Argument("customerOrRecipientId", customerOrRecipientId);
            Require.Argument("cardId", cardId);

            var request = new RestRequest();
            request.Resource = string.Format("{0}/{{customerOrRecipientId}}/cards/{{cardId}}", isRecipient ? "recipients" : "customers");

            request.AddUrlSegment("customerOrRecipientId", customerOrRecipientId);
            request.AddUrlSegment("cardId", cardId);

            return ExecuteObject(request);
        }

        public StripeObject UpdateCard(string customerOrRecipientId, string cardId, ICreditCard card, bool isRecipient = false)
        {
            Require.Argument("customerOrRecipientId", customerOrRecipientId);
            Require.Argument("cardId", cardId);
            Require.Argument("card", card);

            if (card != null)
            {
                card.Validate();
            }

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = string.Format("{0}/{{customerOrRecipientId}}/{1}/{{cardId}}", isRecipient ? "recipients" : "customers", isRecipient ? "cards" : "sources");

            request.AddUrlSegment("customerOrRecipientId", customerOrRecipientId);
            request.AddUrlSegment("cardId", cardId);

            card.AddParametersToRequest_Update(request);

            return ExecuteObject(request);
        }

        public StripeObject DeleteCard(string customerOrRecipientId, string cardId, bool isRecipient = false)
        {
            Require.Argument("customerOrRecipientId", customerOrRecipientId);
            Require.Argument("cardId", cardId);

            var request = new RestRequest();
            request.Method = Method.DELETE;
            request.Resource = string.Format("{0}/{{customerOrRecipientId}}/cards/{{cardId}}", isRecipient ? "recipients" : "customers");

            request.AddUrlSegment("customerOrRecipientId", customerOrRecipientId);
            request.AddUrlSegment("cardId", cardId);

            return ExecuteObject(request);
        }

        public StripeArray ListCards(string customerOrRecipientId, int? count = null, int? offset = null, bool isRecipient = false)
        {
            Require.Argument("customerOrRecipientId", customerOrRecipientId);

            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = string.Format("{0}/{{customerOrRecipientId}}/{1}", isRecipient ? "recipients" : "customers", isRecipient ? "cards" : "sources");

            request.AddUrlSegment("customerOrRecipientId", customerOrRecipientId);
            request.AddQueryParameter("object", "card");

            if (count.HasValue) request.AddParameter("count", count.Value);
            if (offset.HasValue) request.AddParameter("offset", offset.Value);

            return ExecuteArray(request);
        }
    }
}