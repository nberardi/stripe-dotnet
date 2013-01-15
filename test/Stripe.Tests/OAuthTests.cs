using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
	public class OAuthTests
	{
		private StripeClient _client;

		public OAuthTests()
		{
			_client = new StripeClient(Constants.ApiKey);
		}

		[Fact]
		public void CreateOAuthAuthorizationUrl_Test()
		{
			string expected = "https://connect.stripe.com/oauth/authorize?response_type=code&client_id=clientId&scope=scope&stripe_landing=landing&state=state";
			string actual = _client.CreateOAuthAuthorizationUrl("clientId", "scope", "landing", "state");

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void RetrieveOAuthToken_Test()
		{
			//todo: not sure how to test this
		}
	}
}
