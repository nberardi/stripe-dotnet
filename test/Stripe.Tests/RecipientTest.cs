using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
    public class RecipientTest
    {
        private StripeClient _client;
        private CreditCard _card;
        private BankAccount _bankAccount;
        private string _name;
        private string _email;
        private string _type;

        public RecipientTest()
        {
            _name = "sir unit testing";
            _email = "sir.unit@testing.com";
            _type = "individual";

            _card = new CreditCard
            {
                Number = "4000056655665556",
                ExpMonth = 3,
                ExpYear = 2020
            };

            _bankAccount = new BankAccount
            {
                AccountNumber = "000123456789",
                Country = "US",
                Currency = "USD",
                RoutingNumber = "110000000"
            };

            _client = new StripeClient(Constants.ApiKey);
        }

        [Fact]
        public void CreateRecipient_WithBankAccount_Test()
        {
            dynamic response = _client.CreateRecipient(_name, _type, email: _email, bankAccount: _bankAccount);

            Assert.NotNull(response);
            Assert.False(response.IsError);
        }

        [Fact]
        public void CreateRecipient_WithDebitCard_Test()
        {
            dynamic response = _client.CreateRecipient(_name, _type, email: _email, card: _card);

            Assert.NotNull(response);
            Assert.False(response.IsError);
        }

        [Fact]
        public void RetrieveRecipient_Test()
        {
            dynamic recipient = _client.CreateRecipient(_name, _type, email: _email, bankAccount: _bankAccount);
            dynamic response = _client.RetrieveRecipient(recipient.Id);

            Assert.NotNull(response);
            Assert.False(response.IsError);
            Assert.Equal(recipient.Id, response.Id);
        }

        [Fact]
        public void UpdateRecipient_Test() {
            var newCard = new CreditCard
            {
                Number = "5200828282828210",
                ExpMonth = 5,
                ExpYear = 2025
            };

            dynamic recipient = _client.CreateRecipient(_name, _type, email: _email, card: _card);
            dynamic response = _client.UpdateRecipient(recipient.Id, card: newCard);

            var recipient4 = recipient.Cards.Data[0].Last4;
            var response4 = response.Cards.Data[0].Last4;

            Assert.NotNull(response);
            Assert.False(response.IsError);
            Assert.Equal(response.Id, recipient.Id);
            Assert.NotEqual(recipient.Cards.Data[0].Last4, response.Cards.Data[0].Last4);
        }

        [Fact]
        public void DeleteRecipient_Test() {
            dynamic recipient = _client.CreateRecipient(_name, _type, email: _email, card: _card);
            dynamic response = _client.DeleteRecipient(recipient.Id);

            Assert.NotNull(response);
            Assert.False(response.IsError);
            Assert.True(response.Deleted);
            Assert.Equal(recipient.Id, response.Id);
        }

        [Fact]
        public void ListRecipients_Test()
        {
            dynamic response = _client.ListRecipients();

            Assert.NotNull(response);
            Assert.False(response.IsError);
        }
    }
}
