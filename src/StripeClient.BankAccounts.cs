using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Validation;

namespace Stripe
{
    public partial class StripeClient
    {
        /// <summary>
        /// When you create a new bank account, you must specify a managed account to create it on. 
        /// </summary>
        /// <param name="accountId">Managed Account Id</param>
        /// <param name="externalAccount">This can either be a token, like the ones returned by our Stripe.js, or a dictionary containing a user’s bank account details</param>
        /// <param name="defaultForCurrency">If you set this to true (or if this is the first external account being added in this currency) this bank account will become the default external account for its currency.</param>
        /// <param name="metaData">A set of key/value pairs that you can attach to an external account object.</param>
        /// <returns>Returns the bank account object.</returns>
        public StripeObject CreateBankAccount(string accountId, dynamic externalAccount, bool? defaultForCurrency = null, IDictionary<object, object> metaData = null)
        {
            Require.Argument("accountId", accountId);
            Require.Argument("externalAccount", externalAccount);

            var request = new RestRequest()
            {
                Method = Method.POST,
                Resource = "accounts/{accountId}/external_accounts"
            };

            request.AddUrlSegment("accountId", accountId);

            if (externalAccount is string)
                request.AddParameter("external_account", externalAccount);
            else if (externalAccount is IBankAccount)
                (externalAccount as IBankAccount).AddParametersToRequest(request);
            else
                throw new ArgumentException("externalAccount must be of type string or IBankAccount");

            if (defaultForCurrency.HasValue) request.AddParameter("default_for_currency", defaultForCurrency);
            if (metaData != null) AddDictionaryParameter(metaData, "metadata", request);

            return ExecuteObject(request);
        }

        /// <summary>
        /// By default, you can see the 10 most recent bank accounts stored on a managed account directly on the Stripe account object, but you can also retrieve details about a specific bank account stored on the Stripe account.
        /// </summary>
        /// <param name="accountId">Managed Account Id</param>
        /// <param name="bankAccountId">Bank Account Id</param>
        /// <returns>Returns the bank account object.</returns>
        public StripeObject RetrieveBankAccount(string accountId, string bankAccountId)
        {
            Require.Argument("accountId", accountId);
            Require.Argument("bankAccountId", bankAccountId);

            var request = new RestRequest()
            {
                Method = Method.GET,
                Resource = "accounts/{accountId}/external_accounts/{bankAccountId}"
            };

            request.AddUrlSegment("accountId", accountId);
            request.AddUrlSegment("bankAccountId", bankAccountId);

            return ExecuteObject(request);
        }

        /// <summary>
        /// Updates the metadata of a bank account (belonging to a managed account) and optionally sets it as the default for its currency. Other bank account details are, by design, not editable.
        /// </summary>
        /// <param name="accountId">Managed Account Id</param>
        /// <param name="bankAccountId">The ID of the bank account to be updated.</param>
        /// <param name="defaultForCurrency">If set to true, this bank account will become the default external account for its currency.</param>
        /// <param name="metaData">A set of key/value pairs that you can attach to an external account object.</param>
        /// <returns>Returns the bank account object.</returns>
        public StripeObject UpdateBankAccount(string accountId, string bankAccountId, bool? defaultForCurrency = null, IDictionary<object, object> metaData = null)
        {
            Require.Argument("accountId", accountId);
            Require.Argument("bankAccountId", bankAccountId);

            var request = new RestRequest()
            {
                Method = Method.POST,
                Resource = "accounts/{accountId}/external_accounts/{bankAccountId}"
            };

            request.AddUrlSegment("accountId", accountId);
            request.AddUrlSegment("bankAccountId", bankAccountId);

            if (defaultForCurrency.HasValue) request.AddParameter("default_for_currency", defaultForCurrency);
            if (metaData != null) AddDictionaryParameter(metaData, "metadata", request);

            return ExecuteObject(request);
        }

        /// <summary>
        /// You can delete bank accounts from a managed account. If a bank account is the default external account for its currency, it can only be deleted if it is the only external account for that currency, and the currency is not the Stripe account's default currency. Otherwise, you must set another external account to be the default for the currency before deleting it.
        /// </summary>
        /// <param name="accountId">Managed Account Id</param>
        /// <param name="bankAccountId">Bank Account Id to be updated</param>
        /// <returns>Returns the deleted bank account object.</returns>
        public StripeObject DeleteBankAccount(string accountId, string bankAccountId)
        {
            Require.Argument("accountId", accountId);
            Require.Argument("bankAccountId", bankAccountId);

            var request = new RestRequest()
            {
                Method = Method.DELETE,
                Resource = "accounts/{accountId}/external_accounts/{bankAccountId}"
            };

            request.AddUrlSegment("accountId", accountId);
            request.AddUrlSegment("bankAccountId", bankAccountId);

            return ExecuteObject(request);
        }

        /// <summary>
        /// You can see a list of the bank accounts belonging to a managed account. Note that the 10 most recent external accounts are always available by default on the Stripe account object. If you need more than those 10, you can use this API method and the limit and starting_after parameters to page through additional bank accounts.
        /// </summary>
        /// <param name="accountId">Managed Account Id</param>
        /// <param name="endingBefore">A cursor for use in pagination. ending_before is an object ID that defines your place in the list.</param>
        /// <param name="limit">A limit on the number of objects to be returned. Limit can range between 1 and 100 items.</param>
        /// <param name="startingAfter">A cursor for use in pagination. starting_after is an object ID that defines your place in the list.</param>
        /// <returns> Returns a list of the bank accounts stored on the customer or recipient. You can optionally request that the response include the total count of all bank accounts for the customer or recipient.To do so, specify include[]= total_count in your request.</returns>
        public StripeArray ListBankAccounts(string accountId, string endingBefore = null, int limit = 10, string startingAfter = null)
        {
            Require.Argument("accountId", accountId);
            Validate.IsBetween(limit, 1, 100);

            var request = new RestRequest()
            {
                Method = Method.GET,
                Resource = "accounts/{accountId}/external_accounts"
            };

            request.AddUrlSegment("accountId", accountId);
            request.AddParameter("limit", limit, ParameterType.QueryString);

            return ExecuteArray(request);
        }
    }
}
