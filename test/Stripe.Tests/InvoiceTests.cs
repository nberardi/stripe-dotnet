using System;
using System.Linq;
using NUnit.Framework;
using Stripe.Models;

namespace Stripe.Tests
{
	[TestFixture]
	public class InvoiceTests
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
		public void CreateInvoiceItem_Test()
		{
			var response = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Id);
		}

		[Test]
		public void RetrieveInvoiceItem_Test()
		{
			var invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			var response = _client.RetreiveInvoiceItem(invoiceItem.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(invoiceItem.Id, response.Id);
		}

		[Test]
		public void UpdateInvoiceItem_Test()
		{
			var invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			var response = _client.UpdateInvoiceItem(invoiceItem.Id, 200M, "usd");

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(invoiceItem.Id, response.Id);
			Assert.AreNotEqual(invoiceItem.Amount, response.Amount);
		}

		[Test]
		public void DeleteInvoiceItem_Test()
		{
			var invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			var response = _client.DeleteInvoiceItem(invoiceItem.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Deleted);
			Assert.AreEqual(invoiceItem.Id, response.Id);
		}

		[Test]
		public void ListInvoiceItems_Test()
		{
			var response = _client.ListInvoiceItems();

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Data);
			Assert.IsTrue(response.Data.Count > 0);
		}
	}
}
