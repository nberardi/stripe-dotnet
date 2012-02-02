using System;
using System.Linq;
using NUnit.Framework;


namespace Stripe.Tests
{
	[TestFixture]
	public class CustomerTests
	{
		private StripeClient _client;

		private CreditCard _card;
		private string _email = "test@hoppio.com";

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
		public void CreateCustomer_Test()
		{
			dynamic response = _client.CreateCustomer(_card, email: _email);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Id);
		}

		[Test]
		public void CreateCustomer_WithPlan_Test()
		{
			var id = Guid.NewGuid().ToString();
			_client.CreatePlan(id, 400M, "usd", PlanFrequency.Month, id);

			dynamic response = _client.CreateCustomer(_card, email: _email, plan: id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Id);
			Assert.IsNotNull(response.Subscription);
		}

		[Test]
		public void RetrieveCustomer_Test()
		{
			dynamic customer = _client.CreateCustomer(_card, email: _email);
			dynamic response = _client.RetrieveCustomer(customer.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(customer.Id, response.Id);
		}

		[Test]
		public void UpdateCustomer_Test()
		{
			var newCard = new CreditCard {
				Number = "378734493671000",
				ExpMonth = 12,
				ExpYear = 2016
			};

			dynamic customer = _client.CreateCustomer(_card, email: _email);
			dynamic response = _client.UpdateCustomer(customer.Id, newCard);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(customer.Id, response.Id);
			Assert.AreNotEqual(customer.ActiveCard.Last4, response.ActiveCard.Last4);
		}

		[Test]
		public void DeleteCustomer_Test()
		{
			dynamic customer = _client.CreateCustomer(_card, email: _email);
			dynamic response = _client.DeleteCustomer(customer.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Deleted);
			Assert.AreEqual(customer.Id, response.Id);
		}

		[Test]
		public void ListCharges_Test()
		{
			dynamic response = _client.ListCustomers();

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Count > 0);
		}
	}
}
