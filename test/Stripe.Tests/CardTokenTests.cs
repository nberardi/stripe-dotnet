using System;
using System.Linq;
using NUnit.Framework;

namespace Stripe.Tests
{
	[TestFixture]
	public class CardTokenTests
	{
		private StripeClient _client;

		private CreditCard _card;

		[TestFixtureSetUp]
		public void Setup()
		{
			_card = new CreditCard {
				Number = "4111111111111111",
				ExpMonth = 3,
				ExpYear = 2015
			};

			_client = new StripeClient(Constants.ApiKey);
		}

		[Test]
		public void CreateCardToken_Test()
		{
			dynamic response = _client.CreateCardToken(_card);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
		}

		[Test]
		public void RetrieveCardToken_Test()
		{
			dynamic token = _client.CreateCardToken(_card);
			dynamic response = _client.RetrieveCardToken(token.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(token.Id, response.Id);
		}
	}
}
