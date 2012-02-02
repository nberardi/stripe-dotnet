using System;
using System.Linq;
using NUnit.Framework;


namespace Stripe.Tests
{
	[TestFixture]
	public class SubscriptionTests
	{
		private dynamic _plan;
		private dynamic _customer;

		private StripeClient _client;

		[TestFixtureSetUp]
		public void Setup()
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

		[Test]
		public void UpdateCustomersSubscription_Test()
		{
			dynamic response = _client.UpdateCustomersSubscription(_customer.Id, _plan.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
		}

		[Test]
		public void CancelCustomersSubscription_Test()
		{
			_client.UpdateCustomersSubscription(_customer.Id, _plan.Id);
			dynamic response = _client.CancelCustomersSubscription(_customer.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.CanceledAt);
			Assert.IsNotNull(response.EndedAt);
		}
	}
}
