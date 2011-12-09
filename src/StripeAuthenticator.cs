using System;
using System.Linq;
using RestSharp;

namespace Stripe
{
	public class StripeAuthenticator : IAuthenticator
	{
		private readonly string _apiKey;

		public StripeAuthenticator(string apiKey)
		{
			_apiKey = apiKey;
		}

		public void Authenticate(IRestClient client, IRestRequest request)
		{
			// only add the Authorization parameter if it hasn't been added by a previous Execute
			if (!request.Parameters.Any(p => p.Name.Equals("Authorization", StringComparison.InvariantCultureIgnoreCase)))
			{
				var authHeader = string.Format("Basic {0}", _apiKey);
				request.AddParameter("Authorization", authHeader, ParameterType.HttpHeader);
			}
		}
	}
}
