using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
	public class EventTest
	{
		private StripeClient _client;

		public EventTest()
		{
			_client = new StripeClient(Constants.ApiKey);
		}

		[Fact]
		public void ListEvents_Test()
		{
			dynamic response = _client.ListEvents();

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.True(response.Count > 0);
		}
	}
}
