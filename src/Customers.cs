using System;
using System.Linq;
using RestSharp;
using RestSharp.Extensions;
using Stripe.Models;
using RestSharp.Validation;

namespace Stripe
{
	public partial class StripeClient
	{
		public CustomerResponse CreateCustomer(CreditCardRequest card = null, string coupon = null, string email = null, string description = null, string plan = null, DateTimeOffset? trialEnd = null)
		{
			if (card != null)
			{
				Require.Argument("card[number]", card.Number);
				Require.Argument("card[exp_month]", card.ExpirationMonth);
				Require.Argument("card[exp_year]", card.ExpirationYear);
			}

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "customers";

			if (card != null)
			{
				request.AddParameter("card[number]", card.Number);
				request.AddParameter("card[exp_month]", card.ExpirationMonth);
				request.AddParameter("card[exp_year]", card.ExpirationYear);
				if (card.Cvc.HasValue()) request.AddParameter("card[cvc]", card.ExpirationYear);
				if (card.Name.HasValue()) request.AddParameter("card[name]", card.ExpirationYear);
				if (card.AddressLine1.HasValue()) request.AddParameter("card[address_line1]", card.ExpirationYear);
				if (card.AddressLine2.HasValue()) request.AddParameter("card[address_line2]", card.ExpirationYear);
				if (card.AddressZip.HasValue()) request.AddParameter("card[address_zip]", card.ExpirationYear);
				if (card.AddressState.HasValue()) request.AddParameter("card[address_state]", card.ExpirationYear);
				if (card.AddressCountry.HasValue()) request.AddParameter("card[address_country]", card.ExpirationYear);
			}
			if (coupon.HasValue()) request.AddParameter("coupon", coupon);
			if (email.HasValue()) request.AddParameter("email", email);
			if (description.HasValue()) request.AddParameter("description", description);
			if (plan.HasValue()) request.AddParameter("plan", plan);
			if (trialEnd.HasValue) request.AddParameter("trialEnd", trialEnd.Value.ToUnixEpoch());

			return Execute<CustomerResponse>(request);
		}

		public CustomerResponse RetrieveCustomer(string customerId)
		{
			Require.Argument("customerId", customerId);

			var request = new RestRequest();
			request.Resource = "customers/{customerId}";

			request.AddUrlSegment("customerId", customerId);

			return Execute<CustomerResponse>(request);
		}

		public CustomerResponse UpdateCustomer(string customerId, CreditCardRequest card = null, string coupon = null, string email = null, string description = null)
		{
			if (card != null)
			{
				Require.Argument("card[number]", card.Number);
				Require.Argument("card[exp_month]", card.ExpirationMonth);
				Require.Argument("card[exp_year]", card.ExpirationYear);
			}

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "customers/{customerId}";

			if (card != null)
			{
				request.AddParameter("card[number]", card.Number);
				request.AddParameter("card[exp_month]", card.ExpirationMonth);
				request.AddParameter("card[exp_year]", card.ExpirationYear);
				if (card.Cvc.HasValue()) request.AddParameter("card[cvc]", card.ExpirationYear);
				if (card.Name.HasValue()) request.AddParameter("card[name]", card.ExpirationYear);
				if (card.AddressLine1.HasValue()) request.AddParameter("card[address_line1]", card.ExpirationYear);
				if (card.AddressLine2.HasValue()) request.AddParameter("card[address_line2]", card.ExpirationYear);
				if (card.AddressZip.HasValue()) request.AddParameter("card[address_zip]", card.ExpirationYear);
				if (card.AddressState.HasValue()) request.AddParameter("card[address_state]", card.ExpirationYear);
				if (card.AddressCountry.HasValue()) request.AddParameter("card[address_country]", card.ExpirationYear);
			}
			if (coupon.HasValue()) request.AddParameter("coupon", coupon);
			if (email.HasValue()) request.AddParameter("email", email);
			if (description.HasValue()) request.AddParameter("description", description);

			return Execute<CustomerResponse>(request);
		}

		public CustomerResponse DeleteCustomer(string customerId)
		{
			Require.Argument("customerId", customerId);

			var request = new RestRequest();
			request.Method = Method.DELETE;
			request.Resource = "customers/{customerId}";

			request.AddUrlSegment("customerId", customerId);

			return Execute<CustomerResponse>(request);
		}

		public CustomerListResponse ListCustomers(int count = 10, int offset = 0)
		{
			var request = new RestRequest();
			request.Resource = "customers";

			request.AddParameter("count", count);
			request.AddParameter("offset", offset);

			return Execute<CustomerListResponse>(request);
		}
	}
}