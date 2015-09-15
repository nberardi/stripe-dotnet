using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using RestSharp.Validation;


namespace Stripe
{
	public partial class StripeClient
	{
        /// <summary>
        /// Retrieves the details of the account.
        /// </summary>
        /// <param name="accountId">The identifier of the account to be retrieved. If none is provided, will default to the account of the API key.</param>
        /// <returns>Returns an account object.</returns>
		public StripeObject RetrieveAccount(string accountId = null)
		{
            var request = new RestRequest()
            {
                Method = Method.GET
            };

            if (accountId.HasValue())
            {
                request.Resource = "accounts/{accountId}";
                request.AddUrlSegment("accountId", accountId);
            }
            else
            {
                request.Resource = "accounts";
            }

			return ExecuteObject(request);
		}

        /// <summary>
        /// With Connect, you can create Stripe accounts for your users. To do this, you'll first need to register your platform.
        /// </summary>
        /// <param name="managed">Whether you'd like to create a managed or standalone account.</param>
        /// <param name="country">The country the account holder resides in or that the business is legally established in.</param>
        /// <param name="email">The email address of the account holder. For standalone accounts, Stripe will email your user with instructions for how to set up their account.</param>
        /// <returns>Returns the account object, with an additional keys dictionary containing secret and publishable keys for that account.</returns>
        public StripeObject CreateAccount(bool? managed = false, string country = null, string email = null)
        {
            if (managed == false)
            {
                Require.Argument("email", email);
            }

            var request = new RestRequest()
            {
                Method = Method.POST,
                Resource = "accounts"
            };

            if (managed.HasValue) request.AddParameter("managed", managed);
            if (country.HasValue()) request.AddParameter("country", country);
            if (email.HasValue()) request.AddParameter("email", email);

            return ExecuteObject(request);
        }

        /// <summary>
        /// Updates an account by setting the values of the parameters passed. Any parameters not provided will be left unchanged.
        /// </summary>
        /// <param name="accountId">ID of the Account to be updated</param>
        /// <param name="businessLogo"></param>
        /// <param name="businessName">The publicly sharable name for this account</param>
        /// <param name="businessPrimaryColor">A CSS hex color value representing the primary branding color for this account</param>
        /// <param name="businessUrl">The URL that best shows the service or product provided for this account</param>
        /// <param name="supportEmail">A publicly shareable email address that can be reached for support for this account</param>
        /// <param name="supportPhone">A publicly shareable phone number that can be reached for support for this account</param>
        /// <param name="supportUrl">A publicly shareable URL that can be reached for support for this account</param>
        /// <param name="debitNegativeBalances">A boolean for whether or not Stripe should try to reclaim negative balances from the account holder’s bank account.</param>
        /// <param name="declineChargeOn">Account-level settings to automatically decline certain types of charges regardless of the bank’s decision.</param>
        /// <param name="defaultCurrency">Three-letter ISO currency code representing the default currency for the account. This must be a currency that Stripe supports in the account’s country.</param>
        /// <param name="email">Email address of the account holder. For standalone accounts, this is used to email them asking them to claim their Stripe account.</param>
        /// <param name="externalAccount">A card or bank account to attach to the account. You can provide either a token, like the ones returned by Stripe.js, or a dictionary as documented in the external_account parameter for either card or bank account creation.</param>
        /// <param name="legalEntity">Information about the holder of this account, i.e. the user receiving funds from this account</param>
        /// <param name="productDescription">Internal-only description of the product being sold or service being provided by this account. It’s used by Stripe for risk and underwriting purposes.</param>
        /// <param name="statementDescriptor">The text that will appear on credit card statements by default if a charge is being made directly on the account.</param>
        /// <param name="tosAcceptance">Details on who accepted the Stripe terms of service, and when they accepted it.</param>
        /// <param name="transferSchedule">Details on when this account will make funds from charges available, and when they will be paid out to the account holder’s bank account.</param>
        /// <param name="metadata">A set of key/value pairs that you can attach to an account object.</param>
        /// <returns>Returns an account object if the call succeeded. </returns>
        public StripeObject UpdateAccount(string accountId, string businessLogo = null, string businessName = null, string businessPrimaryColor = null,
            string businessUrl = null, string supportEmail = null, string supportPhone = null, string supportUrl = null,
            bool? debitNegativeBalances = null, IDictionary<object, object> declineChargeOn = null, string defaultCurrency = null,
            string email = null, dynamic externalAccount = null, IDictionary<object, object> legalEntity = null,
            string productDescription = null, string statementDescriptor = null, IDictionary<object, object> tosAcceptance = null,
            IDictionary<object, object> transferSchedule = null, IDictionary<object, object> metadata = null)
        {
            Require.Argument("accountId", accountId);

            var request = new RestRequest()
            {
                Method = Method.POST,
                Resource = "accounts/{accountId}"
            };

            request.AddUrlSegment("accountId", accountId);

            if (businessLogo.HasValue()) request.AddParameter("business_logo", businessLogo);
            if (businessName.HasValue()) request.AddParameter("business_name", businessName);
            if (businessPrimaryColor.HasValue()) request.AddParameter("business_primary_color", businessPrimaryColor);
            if (businessUrl.HasValue()) request.AddParameter("business_url", businessUrl);
            if (supportEmail.HasValue()) request.AddParameter("support_email", supportEmail);
            if (supportPhone.HasValue()) request.AddParameter("support_phone", supportPhone);
            if (supportUrl.HasValue()) request.AddParameter("support_url", supportUrl);
            if (debitNegativeBalances.HasValue) request.AddParameter("debit_negative_balances", debitNegativeBalances);
            if (defaultCurrency.HasValue()) request.AddParameter("default_currency", defaultCurrency);
            if (email.HasValue()) request.AddParameter("email", email);
            if (productDescription.HasValue()) request.AddParameter("product_description", productDescription);
            if (statementDescriptor.HasValue()) request.AddParameter("statement_descriptor", statementDescriptor);

            if (declineChargeOn != null) AddDictionaryParameter(declineChargeOn, "decline_charge_on", request);
            if (legalEntity != null) AddDictionaryParameter(legalEntity, "legal_entity", request);
            if (tosAcceptance != null) AddDictionaryParameter(tosAcceptance, "tos_acceptance", request);
            if (transferSchedule != null) AddDictionaryParameter(transferSchedule, "transfer_schedule", request);
            if (metadata != null) AddDictionaryParameter(metadata, "metadata", request);

            if (externalAccount != null)
            {
                if (externalAccount is string)
                    request.AddParameter("external_account", externalAccount);
                else if (externalAccount is ICreditCard)
                    (externalAccount as ICreditCard).AddParametersToRequest(request);
                else
                    throw new ArgumentException("Invalid type of external_account");
            }

            return ExecuteObject(request);
        }

        /// <summary>
        /// With Connect, you may delete Stripe accounts you manage. Managed accounts created using test-mode keys can be deleted at any time. Managed accounts created using live-mode keys may only be deleted once all balances are zero. If you are looking to close your own account, use the data tab in your account settings instead.
        /// </summary>
        /// <param name="accountId">The identifier of the account to be deleted. If none is provided, will default to the account of the API key.</param>
        /// <returns>Returns an object with a deleted parameter on success. If the account ID does not exist, this call returns an error.</returns>
        public StripeObject DeleteAccount(string accountId = null)
        {
            var request = new RestRequest()
            {
                Method = Method.DELETE,
                Resource = "accounts/{accountId}"
            };

            if (accountId.HasValue()) request.AddUrlSegment("accountId", accountId);

            return ExecuteObject(request);
        }

        /// <summary>
        /// Returns a list of accounts connected to your platform via Connect. If you’re not a platform, the list will be empty.
        /// </summary>
        /// <param name="endingBefore">A cursor for use in pagination. ending_before is an object ID that defines your place in the list.</param>
        /// <param name="limit">A limit on the number of objects to be returned. Limit can range between 1 and 100 items.</param>
        /// <param name="startingAfter">A cursor for use in pagination. starting_after is an object ID that defines your place in the list.</param>
        /// <returns>A dictionary with a data property that contains an array of up to limit accounts, starting after account starting_after. Each entry in the array is a separate account object. If no more accounts are available, the resulting array will be empty.</returns>
        public StripeArray ListAccounts(string endingBefore = null, int limit = 10, string startingAfter = null)
        {
            var request = new RestRequest()
            {
                Method = Method.GET,
                Resource = "accounts"
            };

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException("limit", "limit can range between 1 and 100 items.");

            request.AddQueryParameter("limit", limit.ToString());

            if (endingBefore.HasValue()) request.AddParameter("ending_before", endingBefore);
            if (startingAfter.HasValue()) request.AddParameter("starting_after", startingAfter);

            return ExecuteArray(request);
        }
	}
}