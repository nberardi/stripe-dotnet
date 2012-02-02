using System;
using System.Linq;
using NUnit.Framework;


namespace Stripe.Tests
{
	[TestFixture]
	public class ChargeTests
	{
		private StripeClient _client;

		private dynamic _customer;
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
			_customer = _client.CreateCustomer(_card);
		}

		[Test]
		public void CreateCharge_Card_Test()
		{
			dynamic response = _client.CreateCharge(100M, "usd", _card);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Paid);
		}

		[Test]
		public void CreateCharge_Customer_Test()
		{
			dynamic response = _client.CreateCharge(100M, "usd", _customer.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Paid);
		}

		[Test]
		public void RetrieveCharge_Test()
		{
			dynamic charge = _client.CreateCharge(100M, "usd", _customer.Id);
			dynamic response = _client.RetrieveCharge(charge.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(charge.Id, response.Id);
		}

		[Test]
		public void RefundCharge_Test()
		{
			dynamic charge = _client.CreateCharge(100M, "usd", _customer.Id);
			dynamic response = _client.RefundCharge(charge.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(charge.Id, response.Id);
			Assert.IsTrue(response.Refunded);
		}

		[Test]
		public void ListCharges_Test()
		{
			dynamic response = _client.ListCharges();

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Count > 0);
		}
	}
}
