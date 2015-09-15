using System;
using System.Linq;
using RestSharp;
using System.Net;
using RestSharp.Authenticators;

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
			request.Credentials = new NetworkCredential(_apiKey, "");
		}
	}
}
