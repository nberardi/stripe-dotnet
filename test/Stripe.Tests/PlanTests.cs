using System;
using System.Linq;
using NUnit.Framework;
using Stripe.Models;

namespace Stripe.Tests
{
	[TestFixture]
	public class PlanTests
	{
		private StripeClient _client;

		[SetUp]
		public void Setup()
		{
			_client = new StripeClient(Constants.ApiKey);
		}

		[Test]
		public void CreatePlan_Test()
		{
			var response = _client.CreatePlan("gold", 400M, "usd", PlanFrequency.Month, "Gold");

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Id);
		}

		[Test]
		public void RetrievePlan_Test()
		{
			var id = Guid.NewGuid().ToString();

			var plan = _client.CreatePlan(id, 400M, "usd", PlanFrequency.Month, id);
			var response = _client.RetreivePlan(plan.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(plan.Id, response.Id);
		}

		[Test]
		public void DeleteCustomer_Test()
		{
			var id = Guid.NewGuid().ToString();

			var plan = _client.CreatePlan(id, 400M, "usd", PlanFrequency.Month, id);
			var response = _client.DeletePlan(plan.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Deleted);
			Assert.AreEqual(plan.Id, response.Id);
		}

		[Test]
		public void ListCharges_Test()
		{
			var response = _client.ListPlans();

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Data);
			Assert.IsTrue(response.Data.Count > 0);
		}
	}
}
