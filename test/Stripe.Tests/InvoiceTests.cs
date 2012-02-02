using System;
using System.Linq;
using NUnit.Framework;


namespace Stripe.Tests
{
	[TestFixture]
	public class InvoiceTests
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
		public void CreateInvoiceItem_Test()
		{
			dynamic response = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Id);
		}

		[Test]
		public void RetrieveInvoiceItem_Test()
		{
			dynamic invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			dynamic response = _client.RetreiveInvoiceItem(invoiceItem.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(invoiceItem.Id, response.Id);
		}

		[Test]
		public void UpdateInvoiceItem_Test()
		{
			dynamic invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			dynamic response = _client.UpdateInvoiceItem(invoiceItem.Id, 200M, "usd");

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(invoiceItem.Id, response.Id);
			Assert.AreNotEqual(invoiceItem.Amount, response.Amount);
		}

		[Test]
		public void DeleteInvoiceItem_Test()
		{
			dynamic invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			dynamic response = _client.DeleteInvoiceItem(invoiceItem.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Deleted);
			Assert.AreEqual(invoiceItem.Id, response.Id);
		}

		[Test]
		public void ListInvoiceItems_Test()
		{
			dynamic response = _client.ListInvoiceItems();

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Count > 0);
		}

		[Test, Ignore]
		public void RetrieveInvoice_Test()
		{
			dynamic invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			dynamic response = _client.RetreiveInvoice(invoiceItem.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(invoiceItem.Id, response.Id);
		}

		[Test]
		public void RetrieveCustomersUpcomingInvoice_Test()
		{
			dynamic invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			var response = _client.RetreiveCustomersUpcomingInvoice(_customer.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
		}

		[Test, Ignore]
		public void ListInvoices_Test()
		{
			dynamic response = _client.ListInvoices();

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Data);
			Assert.IsTrue(response.Data.Count > 0);
		}
	}
}
