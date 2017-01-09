using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Stripe.Tests
{
    public class BankAccounTests
    {
        private StripeClient _client;

        private BankAccount _bankAccount;
        private dynamic _account;

        public BankAccounTests()
        {
            _bankAccount = new BankAccount()
            {
                AccountNumber = "000123456789",
                Country = "US",
                Currency = "USD",
                RoutingNumber = "110000000"
            };

            _client = new StripeClient(Constants.ApiKey);
            _account = _client.CreateAccount(managed: true, country: "us", email: "sir.unit@test.com");
        }

        [Fact]
        public void CreateBankAccount_Test()
        {
            dynamic response = _client.CreateBankAccount(_account.Id, externalAccount: _bankAccount);

            Assert.NotNull(response);
            Assert.False(response.IsError);
        }

        [Fact]
        public void RetrieveBankAccount_Test() {
            dynamic bankAccount = _client.CreateBankAccount(_account.Id, externalAccount: _bankAccount);
            dynamic response = _client.RetrieveBankAccount(_account.Id, bankAccount.Id);

            Assert.NotNull(response);
            Assert.False(response.IsError);
        }

        [Fact]
        public void UpdateBankAccount_Test()
        {
            var newMetaData = new Dictionary<object, object> { { "Test", "123" }, { "Testing", "456" } };

            dynamic bankAccount = _client.CreateBankAccount(_account.Id, externalAccount: _bankAccount);
            dynamic response = _client.UpdateBankAccount(_account.Id, bankAccount.Id, metaData: newMetaData);

            Assert.NotNull(response);
            Assert.False(response.IsError);
            Assert.Equal(newMetaData.First().Value, response.metaData.Test);
            Assert.Equal(newMetaData.Last().Value, response.metaData.Testing);
        }

        [Fact]
        public void DeleteBankAccount_Test()
        {
            dynamic bankAccountOne = _client.CreateBankAccount(_account.Id, externalAccount: _bankAccount);
            dynamic bankAccountTwo = _client.CreateBankAccount(_account.Id, externalAccount: _bankAccount);
            dynamic response = _client.DeleteBankAccount(_account.Id, bankAccountTwo.Id);

            Assert.NotNull(response);
            Assert.False(response.IsError);
            Assert.Equal(bankAccountTwo.Id, response.Id);
        }

        [Fact]
        public void ListBankAccounts_Test()
        {
            dynamic response = _client.ListBankAccounts(_account.Id);

            Assert.NotNull(response);
            Assert.False(response.IsError);
        }
    }
}
