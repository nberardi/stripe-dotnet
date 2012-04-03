using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
	public class SubscriptionTests
	{
		private dynamic _plan;
		private dynamic _customer;

		private StripeClient _client;

		public SubscriptionTests()
		{
			_client = new StripeClient(Constants.ApiKey);

			var id = Guid.NewGuid().ToString();
			var card = new CreditCard {
				Number = "4111111111111111",
				ExpMonth = 3,
				ExpYear = 2015
			};

			_plan = _client.CreatePlan(id, 400M, "usd", PlanFrequency.Month, id);
			_customer = _client.CreateCustomer(card);
		}

		[Fact]
		public void UpdateCustomersSubscription_Test()
		{
			dynamic response = _client.UpdateCustomersSubscription(_customer.Id, _plan.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
		}

		[Fact]
		public void CancelCustomersSubscription_Test()
		{
			_client.UpdateCustomersSubscription(_customer.Id, _plan.Id);
			dynamic response = _client.CancelCustomersSubscription(_customer.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.NotNull(response.CanceledAt);
			Assert.NotNull(response.EndedAt);
		}
	}
}
