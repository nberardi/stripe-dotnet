using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
	public class CustomerTests
	{
		private StripeClient _client;

		private CreditCard _card;
		private string _email = "test@hoppio.com";

		public CustomerTests()
		{
			_card = new CreditCard {
                Number = "4242424242424242",
				ExpMonth = 3,
				ExpYear = 2015
			};

			_client = new StripeClient(Constants.ApiKey);
		}

		[Fact]
		public void CreateCustomer_Test()
		{
			dynamic response = _client.CreateCustomer(_card, email: _email);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.NotNull(response.Id);
		}

		[Fact]
		public void CreateCustomer_WithPlan_Test()
		{
			var id = Guid.NewGuid().ToString();
			_client.CreatePlan(id, 400M, "usd", PlanFrequency.Month, id);

			dynamic response = _client.CreateCustomer(_card, email: _email, plan: id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.NotNull(response.Id);
			Assert.NotNull(response.Subscription);
		}

		[Fact]
		public void RetrieveCustomer_Test()
		{
			dynamic customer = _client.CreateCustomer(_card, email: _email);
			dynamic response = _client.RetrieveCustomer(customer.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.Equal(customer.Id, response.Id);
		}

		[Fact]
		public void UpdateCustomer_Test()
		{
			var newCard = new CreditCard {
                Number = "4012888888881881",
				ExpMonth = 12,
				ExpYear = 2016
			};

			dynamic customer = _client.CreateCustomer(_card, email: _email);
			dynamic response = _client.UpdateCustomer(customer.Id, newCard);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.Equal(customer.Id, response.Id);
			Assert.NotEqual(customer.ActiveCard.Last4, response.ActiveCard.Last4);
		}

		[Fact]
		public void DeleteCustomer_Test()
		{
			dynamic customer = _client.CreateCustomer(_card, email: _email);
			dynamic response = _client.DeleteCustomer(customer.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.True(response.Deleted);
			Assert.Equal(customer.Id, response.Id);
		}

		[Fact]
		public void ListCharges_Test()
		{
			StripeArray response = _client.ListCharges();

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.True(response.Any());
		}
	}
}
