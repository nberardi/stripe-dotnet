using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
	public class PlanTests
	{
		private StripeClient _client;

		public PlanTests()
		{
			_client = new StripeClient(Constants.ApiKey);
		}

		[Fact]
		public void CreatePlan_Test()
		{
			var id = Guid.NewGuid().ToString();

			dynamic response = _client.CreatePlan(id, 400M, "usd", PlanFrequency.Month, id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.NotNull(response.Id);
		}

		[Fact]
		public void RetrievePlan_Test()
		{
			var id = Guid.NewGuid().ToString();

			dynamic plan = _client.CreatePlan(id, 400M, "usd", PlanFrequency.Month, id);
			dynamic response = _client.RetreivePlan(plan.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.Equal(plan.Id, response.Id);
		}

		[Fact]
		public void DeletePlan_Test()
		{
			var id = Guid.NewGuid().ToString();

			dynamic plan = _client.CreatePlan(id, 400M, "usd", PlanFrequency.Month, id);
			dynamic response = _client.DeletePlan(plan.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.True(response.Deleted);
			Assert.Equal(plan.Id, response.Id);
		}

		[Fact]
		public void ListPlans_Test()
		{
			StripeArray response = _client.ListPlans();

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.True(response.Any());
		}

        [Fact]
        public void UpdatePlan_Test()
        {
            var id = Guid.NewGuid().ToString();

            string newName = "Testing123";

            dynamic plan = _client.CreatePlan(id, 400M, "usd", PlanFrequency.Month, id);
            dynamic response = _client.UpdatePlan(id, newName);

            Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.Equal(plan.Id, response.Id);
            Assert.Equal(response.name, newName);
        }
	}
}
