using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
    public class CardTokenTests
    {
        private StripeClient _client;

        private CreditCard _card;

        public CardTokenTests()
        {
            _card = new CreditCard
            {
                Number = "4242424242424242",
                ExpMonth = 3,
                ExpYear = (DateTime.Now.Year + 2)
            };

            _client = new StripeClient(Constants.ApiKey);
        }

        [Fact]
        public void CreateCardToken_Test()
        {
            dynamic response = _client.CreateCardToken(_card);

            Assert.NotNull(response);
            Assert.False(response.IsError);
        }

        [Fact]
        public void RetrieveCardToken_Test()
        {
            dynamic token = _client.CreateCardToken(_card);
            dynamic response = _client.RetrieveCardToken(token.Id);

            Assert.NotNull(response);
            Assert.False(response.IsError);
            Assert.Equal(token.Id, response.Id);
        }
    }
}
