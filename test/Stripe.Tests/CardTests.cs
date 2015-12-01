using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
    public class CardTests
    {
        private StripeClient _client;
        private dynamic _customer;
        private CreditCard _card;

        public CardTests()
        {
            _client = new StripeClient(Constants.ApiKey);
            _card = new CreditCard
            {
                Number = "4242424242424242",
                ExpMonth = 3,
                ExpYear = (DateTime.Now.Year + 2)
            };
            _customer = _client.CreateCustomer(_card);
        }

        [Fact]
        public void CreateCard_Test()
        {
            CreditCard card = new CreditCard
            {
                Number = "4242424242424242",
                ExpMonth = 3,
                ExpYear = (DateTime.Now.Year + 2)
            };
            dynamic response = _client.CreateCard(_customer.Id, card);

            Assert.NotNull(response);
            Assert.False(response.IsError);
        }

        [Fact]
        public void RetrieveCard_Test()
        {
            dynamic response = _client.RetrieveCard(_customer.Id, _customer.Sources.Data[0].Id);

            Assert.NotNull(response);
            Assert.False(response.IsError);
        }

        [Fact]
        public void UpdateCard_Test()
        {
            CreditCard updatedCard = new CreditCard()
            {
                Name = "updated name",
                Number = _card.Number,
                ExpMonth = _card.ExpMonth + 1,
                ExpYear = _card.ExpYear + 1
            };

            dynamic response = _client.UpdateCard(_customer.Id, _customer.Sources.Data[0].Id, updatedCard);

            Assert.NotNull(response);
            Assert.False(response.IsError);
            Assert.Equal(updatedCard.Name, response.Name);
            Assert.Equal(updatedCard.ExpMonth, response.ExpMonth);
            Assert.Equal(updatedCard.ExpYear, response.ExpYear);
        }

        [Fact]
        public void DeleteCard_Test()
        {
            dynamic response = _client.DeleteCard(_customer.Id, _customer.Sources.Data[0].Id);

            Assert.NotNull(response);
            Assert.False(response.IsError);
        }

        [Fact]
        public void ListCards_Test()
        {
            dynamic response = _client.ListCards(_customer.Id);

            Assert.NotNull(response);
            Assert.False(response.IsError);
        }
    }
}
