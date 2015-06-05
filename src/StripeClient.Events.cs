using System;
using System.Linq;
using RestSharp;
using RestSharp.Validation;

namespace Stripe
{
	public partial class StripeClient
	{
		public StripeObject RetreiveEvent(string eventId)
		{
			Require.Argument("eventId", eventId);

			var request = new RestRequest();
			request.Resource = "events/{eventId}";

			request.AddUrlSegment("eventId", eventId);

			return ExecuteObject(request);
		}

		public StripeArray ListEvents(string type = null, DateTimeOffset? created = null,DateTimeOffset? gt = null, DateTimeOffset? gte = null, DateTimeOffset? lt = null, DateTimeOffset? lte = null, int? count = null, int? offset = null)
		{
			if (created.HasValue && (gt.HasValue || gte.HasValue || lt.HasValue || lte.HasValue))
				throw new ArgumentException("'created' cannot be defined with 'gt', 'gte', 'lt', or 'lte'");

			var request = new RestRequest();
			request.Resource = "events";

			if (type.HasValue()) request.AddParameter("type", type);
			if (created.HasValue) request.AddParameter("created", created.Value.ToUnixEpoch());
			if (gt.HasValue) request.AddParameter("created[gt]", gt.Value.ToUnixEpoch());
			if (gte.HasValue) request.AddParameter("created[gte]", gte.Value.ToUnixEpoch());
			if (lt.HasValue) request.AddParameter("created[lt]", lt.Value.ToUnixEpoch());
			if (lte.HasValue) request.AddParameter("created[lte]", lte.Value.ToUnixEpoch());
			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);

			return ExecuteArray(request);
		}
	}
}
