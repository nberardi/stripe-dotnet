using System;
using System.Linq;
using RestSharp;
using RestSharp.Validation;


namespace Stripe
{
	public partial class StripeClient
	{
		public StripeObject CreateCard(string customerId, CreditCard card)
		{
			Require.Argument("customerId", customerId);
			Require.Argument("card", card);

			if (card != null) ((ICreditCard)card).Validate();

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "customers/{customerId}/cards";

			request.AddUrlSegment("customerId", customerId);

			((ICreditCard)card).AddParametersToRequest(request);

			return ExecuteObject(request);
		}

		public StripeObject RetrieveCard(string customerId, string cardId)
		{
			Require.Argument("customerId", customerId);
			Require.Argument("cardId", cardId);

			var request = new RestRequest();
			request.Resource = "customers/{customerId}/cards/{cardId}";

			request.AddUrlSegment("customerId", customerId);
			request.AddUrlSegment("cardId", cardId);

			return ExecuteObject(request);
		}

		public StripeObject UpdateCard(string customerId, string cardId, CreditCard card)
		{
			Require.Argument("customerId", customerId);
			Require.Argument("cardId", cardId);
			Require.Argument("card", card);

			if (card != null) ((ICreditCard)card).Validate();

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "customers/{customerId}/cards/{cardId}";

			request.AddUrlSegment("customerId", customerId);
			request.AddUrlSegment("cardId", cardId);

			((ICreditCard)card).AddParametersToRequest(request);

			return ExecuteObject(request);
		}

		public StripeObject DeleteCustomer(string customerId, string cardId)
		{
			Require.Argument("customerId", customerId);
			Require.Argument("cardId", cardId);

			var request = new RestRequest();
			request.Method = Method.DELETE;
			request.Resource = "customers/{customerId}/cards/{cardId}";

			request.AddUrlSegment("customerId", customerId);
			request.AddUrlSegment("cardId", cardId);

			return ExecuteObject(request);
		}

		public StripeArray ListCustomers(string customerId, int? count = null, int? offset = null)
		{
			Require.Argument("customerId", customerId);

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "customers/{customerId}/cards";

			request.AddUrlSegment("customerId", customerId);

			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);

			return ExecuteArray(request);
		}
	}
}