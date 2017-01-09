using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Stripe.Tests
{
    public class RefundTests
    {
        private readonly StripeClient _client;
        private readonly CreditCard _card;

        public RefundTests()
        {
            _client = new StripeClient(Constants.ApiKey);

            _card = new CreditCard
            {
                Number = "4242 4242 4242 4242",
                ExpMonth = 3,
                ExpYear = (DateTime.Now.Year + 2)
            };

            
        }
        [Fact]
        public void Create_Refund_Card_Charge_Test()
        {
            dynamic charge = _client.CreateCharge(100M, "usd", _card);

            dynamic response = _client.CreateRefund(charge.Id);

            Assert.NotNull(response);
            Assert.False(response.IsError);
            Assert.Equal(charge.Id, response.charge);
            Assert.NotNull(response.balance_transaction);
        }
        [Fact]
        public void Retrieve_Refund_Card_Charge_Test()
        {
            dynamic charge = _client.CreateCharge(100M, "usd", _card);

            dynamic refund = _client.CreateRefund(charge.Id);

            dynamic response = _client.RetrieveRefund(refund.Id);

            Assert.NotNull(response);
            Assert.False(response.IsError);
        }
        [Fact]
        public void Update_Refund_Card_Charge_Test()
        {
            dynamic charge = _client.CreateCharge(100M, "usd", _card);

            var metaData = new Dictionary<object, object>() {
                { "Shipping", "USPS" }
            };

            dynamic refund = _client.CreateRefund(charge.Id, metaData: metaData);

            metaData["Shipping"] = "FedEx";

            dynamic response = _client.UpdateRefund(refund.Id, metaData: metaData);

            Assert.NotNull(response);
            Assert.False(response.IsError);
            Assert.NotEqual(refund.metadata["Shipping"], response.metadata["Shipping"]);
        }
        [Fact]
        public void List_Refund_Card_Charge_Test()
        {
            dynamic charge = _client.CreateCharge(100M, "usd", _card);

            dynamic refund = _client.CreateRefund(charge.Id, amount: 10M);
            dynamic secondRefund = _client.CreateRefund(charge.Id, amount: 15M);

            StripeArray response = _client.ListRefunds(charge.Id, limit: 2);

            Assert.NotNull(response);
            Assert.False(response.IsError);
            Assert.True(response.Any());
        }
    }
}
