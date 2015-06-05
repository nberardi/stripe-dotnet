using System;
using System.Linq;
using RestSharp;
using RestSharp.Validation;

namespace Stripe
{
	public partial class StripeClient
	{
		public StripeObject CreateCard(string customerOrRecipientId, CreditCard card, bool isRecipient = false)
		{
            Require.Argument("customerOrRecipientId", customerOrRecipientId);
			Require.Argument("card", card);

			if (card != null)
            {
                ((ICreditCard)card).Validate();
            }

			var request = new RestRequest();

			request.Method = Method.POST;
            request.Resource = string.Format("{0}/{customerOrRecipientId}/cards", isRecipient ? "recipients" : "customers");

            request.AddUrlSegment("customerOrRecipientId", customerOrRecipientId);

			((ICreditCard)card).AddParametersToRequest(request);

			return ExecuteObject(request);
		}

		public StripeObject RetrieveCard(string customerOrRecipientId, string cardId, bool isRecipient = false)
		{
            Require.Argument("customerOrRecipientId", customerOrRecipientId);
			Require.Argument("cardId", cardId);

			var request = new RestRequest();
            request.Resource = string.Format("{0}/{customerOrRecipientId}/cards/{cardId}", isRecipient ? "recipients" : "customers");

            request.AddUrlSegment("customerOrRecipientId", customerOrRecipientId);
			request.AddUrlSegment("cardId", cardId);

			return ExecuteObject(request);
		}

		public StripeObject UpdateCard(string customerOrRecipientId, string cardId, CreditCard card, bool isRecipient = false)
		{
            Require.Argument("customerOrRecipientId", customerOrRecipientId);
			Require.Argument("cardId", cardId);
			Require.Argument("card", card);

            if (card != null)
            {
                ((ICreditCard)card).Validate();
            }

			var request = new RestRequest();
			request.Method = Method.POST;
            request.Resource = string.Format("{0}/{customerOrRecipientId}/cards/{cardId}", isRecipient ? "recipients" : "customers");

            request.AddUrlSegment("customerOrRecipientId", customerOrRecipientId);
			request.AddUrlSegment("cardId", cardId);

			((ICreditCard)card).AddParametersToRequest(request);

			return ExecuteObject(request);
		}

		public StripeObject DeleteCard(string customerOrRecipientId, string cardId, bool isRecipient = false)
		{
            Require.Argument("customerOrRecipientId", customerOrRecipientId);
			Require.Argument("cardId", cardId);

			var request = new RestRequest();
			request.Method = Method.DELETE;
            request.Resource = string.Format("{0}/{customerOrRecipientId}/cards/{cardId}", isRecipient ? "recipients" : "customers");

            request.AddUrlSegment("customerOrRecipientId", customerOrRecipientId);
			request.AddUrlSegment("cardId", cardId);

			return ExecuteObject(request);
		}

		public StripeArray ListCards(string customerOrRecipientId, int? count = null, int? offset = null, bool isRecipient = false)
		{
            Require.Argument("customerOrRecipientId", customerOrRecipientId);

			var request = new RestRequest();
			request.Method = Method.POST;
            request.Resource = string.Format("{0}/{customerOrRecipientId}/cards", isRecipient ? "recipients" : "customers");

            request.AddUrlSegment("customerOrRecipientId", customerOrRecipientId);

			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);

			return ExecuteArray(request);
		}
	}
}