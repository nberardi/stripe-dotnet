using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
	public class AccountTests
	{
		private StripeClient _client;

		public AccountTests()
		{
			_client = new StripeClient(Constants.ApiKey);
		}

		[Fact]
		public void RetrieveAccount_Test()
		{
			dynamic response = _client.RetrieveAccount();

			Assert.NotNull(response);
			Assert.False(response.IsError);
		}
	}
}
