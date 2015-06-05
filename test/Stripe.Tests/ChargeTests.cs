using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
	public class ChargeTests
	{
		private StripeClient _client;

		private dynamic _customer;
		private CreditCard _card;

		public ChargeTests()
		{
			_card = new CreditCard {
				Number = "4242 4242 4242 4242",
				ExpMonth = 3,
				ExpYear = 2015
			};

			_client = new StripeClient(Constants.ApiKey);
			_customer = _client.CreateCustomer(_card);
		}
        [Fact]
        public void CreateCharge_Using_Token()
        {
            dynamic token = _client.CreateCardToken(_card);
            dynamic response = _client.CreateChargeWithToken(100M,token.Id);
            Assert.NotNull(response);
            Assert.False(response.IsError);
            Assert.True(response.Paid);
        }
		[Fact]
		public void CreateCharge_Card_Test()
		{
			dynamic response = _client.CreateCharge(100M, "usd", _card);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.True(response.Paid);
		}

		[Fact]
		public void CreateCharge_Customer_Test()
		{
			dynamic response = _client.CreateCharge(100M, "usd", _customer.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.True(response.Paid);
		}

		[Fact]
		public void RetrieveCharge_Test()
		{
			dynamic charge = _client.CreateCharge(100M, "usd", _customer.Id);
			dynamic response = _client.RetrieveCharge(charge.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.Equal(charge.Id, response.Id);
		}

		[Fact]
		public void RefundCharge_Test()
		{
			dynamic charge = _client.CreateCharge(100M, "usd", _customer.Id);
			dynamic response = _client.RefundCharge(charge.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.Equal(charge.Id, response.Id);
			Assert.True(response.Refunded);
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
