using System;
using System.Linq;
using NUnit.Framework;


namespace Stripe.Tests
{
	[TestFixture]
	public class EventTest
	{
		private StripeClient _client;

		[TestFixtureSetUp]
		public void Setup()
		{
			_client = new StripeClient(Constants.ApiKey);
		}

		[Test]
		public void ListEvents_Test()
		{
			dynamic response = _client.ListEvents();

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Count > 0);
		}
	}
}
