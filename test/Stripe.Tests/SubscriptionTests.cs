using System;
using System.Linq;
using NUnit.Framework;
using Stripe.Models;

namespace Stripe.Tests
{
	[TestFixture]
	public class SubscriptionTests
	{
		private StripePlan _plan;
		private StripeCustomer _customer;

		private StripeClient _client;

		[SetUp]
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
			var response = _client.UpdateCustomersSubscription(_customer.Id, _plan.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
		}

		[Test]
		public void CancelCustomersSubscription_Test()
		{
			_client.UpdateCustomersSubscription(_customer.Id, _plan.Id);
			var response = _client.CancelCustomersSubscription(_customer.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.CanceledAt);
			Assert.IsNotNull(response.EndedAt);
		}
	}
}
