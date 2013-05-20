using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
	public class InvoiceTests
	{
		private StripeClient _client;

		private dynamic _customer;
		private CreditCard _card;

		public InvoiceTests()
		{
			_card = new CreditCard {
				Number = "4111111111111111",
				ExpMonth = 3,
				ExpYear = 2015
			};

			_client = new StripeClient(Constants.ApiKey);
			_customer = _client.CreateCustomer(_card);
		}

		[Fact]
		public void CreateInvoiceItem_Test()
		{
			dynamic response = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.NotNull(response.Id);
		}

		[Fact]
		public void CreateInvoice_Test()
		{
			dynamic invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			dynamic response = _client.CreateInvoice(_customer.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.False(response.closed);
		}

		[Fact]
		public void RetrieveInvoiceItem_Test()
		{
			dynamic invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			dynamic response = _client.RetreiveInvoiceItem(invoiceItem.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.Equal(invoiceItem.Id, response.Id);
		}

		[Fact]
		public void UpdateInvoiceItem_Test()
		{
			dynamic invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			dynamic response = _client.UpdateInvoiceItem(invoiceItem.Id, 200M, "usd");

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.Equal(invoiceItem.Id, response.Id);
			Assert.NotEqual(invoiceItem.Amount, response.Amount);
		}

		[Fact]
		public void UpdateInvoice_Test()
		{
			dynamic invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			dynamic invoice = _client.CreateInvoice(_customer.Id);
			dynamic response = _client.UpdateInvoice(invoice.Id, true);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.Equal(invoice.Id, response.Id);
			Assert.True(response.Closed);
		}

		[Fact]
		public void DeleteInvoiceItem_Test()
		{
			dynamic invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			dynamic response = _client.DeleteInvoiceItem(invoiceItem.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.True(response.Deleted);
			Assert.Equal(invoiceItem.Id, response.Id);
		}

		[Fact]
		public void ListInvoiceItems_Test()
		{
			dynamic response = _client.ListInvoiceItems();

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.True(response.Count > 0);
		}

		[Fact]
		public void RetrieveInvoice_Test()
		{
			dynamic invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			dynamic response = _client.RetreiveInvoice(invoiceItem.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.Equal(invoiceItem.Id, response.Id);
		}

		[Fact]
		public void RetrieveCustomersUpcomingInvoice_Test()
		{
			dynamic invoiceItem = _client.CreateInvoiceItem(_customer.Id, 100M, "usd");
			var response = _client.RetreiveCustomersUpcomingInvoice(_customer.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
		}

		[Fact]
		public void ListInvoices_Test()
		{
			dynamic response = _client.ListInvoices();

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.NotNull(response.Data);
			Assert.True(response.Data.Count > 0);
		}
	}
}
