using System;
using System.Linq;
using NUnit.Framework;
using Stripe.Models;

namespace Stripe.Tests
{
	[TestFixture]
	public class ChargeTests
	{
		private StripeClient _client;

		private CustomerResponse _customer;
		private CreditCardRequest _card;

		[SetUp]
		public void Setup()
		{
			_card = new CreditCardRequest {
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
			var response = _client.CreateCharge(100M, "usd", _card);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Paid);
		}

		[Test]
		public void CreateCharge_Customer_Test()
		{
			var response = _client.CreateCharge(100M, "usd", _customer.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Paid);
		}

		[Test]
		public void RetrieveCharge_Test()
		{
			var charge = _client.CreateCharge(100M, "usd", _customer.Id);
			var response = _client.RetrieveCharge(charge.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(charge.Id, response.Id);
		}

		[Test]
		public void RefundCharge_Test()
		{
			var charge = _client.CreateCharge(100M, "usd", _customer.Id);
			var response = _client.RefundCharge(charge.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(charge.Id, response.Id);
			Assert.IsTrue(response.Refunded);
		}

		[Test]
		public void ListCharges_Test()
		{
			var response = _client.ListCharges();

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Data);
			Assert.IsTrue(response.Data.Count > 0);
		}
	}
}
