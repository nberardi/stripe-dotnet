using System;
using System.Linq;
using NUnit.Framework;
using Stripe.Models;

namespace Stripe.Tests
{
	[TestFixture]
	public class CustomerTests
	{
		private StripeClient _client;

		private CreditCard _card;
		private string _email = "test@hoppio.com";

		[SetUp]
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
			var response = _client.CreateCustomer(_card, email: _email);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Id);
		}

		[Test]
		public void RetrieveCustomer_Test()
		{
			var customer = _client.CreateCustomer(_card, email: _email);
			var response = _client.RetrieveCustomer(customer.Id);

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

			var customer = _client.CreateCustomer(_card, email: _email);
			var response = _client.UpdateCustomer(customer.Id, newCard);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(customer.Id, response.Id);
			Assert.AreNotEqual(customer.ActiveCard.Last4, response.ActiveCard.Last4);
		}

		[Test]
		public void DeleteCustomer_Test()
		{
			var customer = _client.CreateCustomer(_card, email: _email);
			var response = _client.DeleteCustomer(customer.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Deleted);
			Assert.AreEqual(customer.Id, response.Id);
		}

		[Test]
		public void ListCharges_Test()
		{
			var response = _client.ListCustomers();

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Data);
			Assert.IsTrue(response.Data.Count > 0);
		}
	}
}
