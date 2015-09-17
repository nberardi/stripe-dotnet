using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
	public class AccountTests
	{
		private readonly StripeClient _client;
        private readonly dynamic _account;
        private readonly dynamic _managedAccount;
        private readonly string _email;
        private readonly string _managedEmail;
        private readonly string _newEmail;

		public AccountTests()
		{
           _email = Guid.NewGuid().ToString() + "@test.com";
           _managedEmail = Guid.NewGuid().ToString() + "@test.com";
           _newEmail = Guid.NewGuid().ToString() + "@test.com";

			_client = new StripeClient(Constants.ApiKey);
            _account = _client.CreateAccount(false, "US", _email);
            _managedAccount = _client.CreateAccount(true, "US", _managedEmail);
		}

		[Fact]
		public void RetrieveAccount_Test()
		{
            dynamic response = _client.RetrieveAccount(_account.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
		}

        [Fact]
        public void CreateAccount_Test()
        {
            dynamic response = _client.CreateAccount(false, "US", _newEmail);

            Assert.NotNull(response);
            Assert.False(response.IsError);
        } 

        [Fact]
        public void UpdateAccount_Test()
        {
            string businessName = "Test Business Name 123";

            dynamic response = _client.UpdateAccount(_managedAccount.Id, businessName: businessName);

            Assert.NotNull(response);
            Assert.False(response.IsError);
            Assert.Equal(businessName, response.businessName);
        }

        [Fact]
        public void DeleteAccount_Test()
        {
            dynamic response = _client.DeleteAccount(_managedAccount.Id);

            Assert.NotNull(response);
            Assert.False(response.IsError);
            Assert.Equal(_managedAccount.Id, response.Id);
        }

        [Fact]
        public void ListAccounts_Test()
        {
            dynamic response = _client.ListAccounts();

            Assert.NotNull(response);
            Assert.False(response.IsError);
        }
	}
}
