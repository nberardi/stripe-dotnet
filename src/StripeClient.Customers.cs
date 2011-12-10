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
		public StripeCustomer CreateCustomer(CreditCard card = null, string coupon = null, string email = null, string description = null, string plan = null, DateTimeOffset? trialEnd = null)
		{
			if (card != null)
			{
				Require.Argument("card[number]", card.Number);
				Require.Argument("card[exp_month]", card.ExpMonth);
				Require.Argument("card[exp_year]", card.ExpYear);
			}

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "customers";

			if (card != null)
			{
				request.AddParameter("card[number]", card.Number);
				request.AddParameter("card[exp_month]", card.ExpMonth);
				request.AddParameter("card[exp_year]", card.ExpYear);
				if (card.Cvc.HasValue()) request.AddParameter("card[cvc]", card.ExpYear);
				if (card.Name.HasValue()) request.AddParameter("card[name]", card.ExpYear);
				if (card.AddressLine1.HasValue()) request.AddParameter("card[address_line1]", card.ExpYear);
				if (card.AddressLine2.HasValue()) request.AddParameter("card[address_line2]", card.ExpYear);
				if (card.AddressZip.HasValue()) request.AddParameter("card[address_zip]", card.ExpYear);
				if (card.AddressState.HasValue()) request.AddParameter("card[address_state]", card.ExpYear);
				if (card.AddressCountry.HasValue()) request.AddParameter("card[address_country]", card.ExpYear);
			}
			if (coupon.HasValue()) request.AddParameter("coupon", coupon);
			if (email.HasValue()) request.AddParameter("email", email);
			if (description.HasValue()) request.AddParameter("description", description);
			if (plan.HasValue()) request.AddParameter("plan", plan);
			if (trialEnd.HasValue) request.AddParameter("trialEnd", trialEnd.Value.ToUnixEpoch());

			return Execute<StripeCustomer>(request);
		}

		public StripeCustomer RetrieveCustomer(string customerId)
		{
			Require.Argument("customerId", customerId);

			var request = new RestRequest();
			request.Resource = "customers/{customerId}";

			request.AddUrlSegment("customerId", customerId);

			return Execute<StripeCustomer>(request);
		}

		public StripeCustomer UpdateCustomer(string customerId, CreditCard card = null, string coupon = null, string email = null, string description = null)
		{
			if (card != null)
			{
				Require.Argument("card[number]", card.Number);
				Require.Argument("card[exp_month]", card.ExpMonth);
				Require.Argument("card[exp_year]", card.ExpYear);
			}

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "customers/{customerId}";

			request.AddUrlSegment("customerId", customerId);

			if (card != null)
			{
				request.AddParameter("card[number]", card.Number);
				request.AddParameter("card[exp_month]", card.ExpMonth);
				request.AddParameter("card[exp_year]", card.ExpYear);
				if (card.Cvc.HasValue()) request.AddParameter("card[cvc]", card.ExpYear);
				if (card.Name.HasValue()) request.AddParameter("card[name]", card.ExpYear);
				if (card.AddressLine1.HasValue()) request.AddParameter("card[address_line1]", card.ExpYear);
				if (card.AddressLine2.HasValue()) request.AddParameter("card[address_line2]", card.ExpYear);
				if (card.AddressZip.HasValue()) request.AddParameter("card[address_zip]", card.ExpYear);
				if (card.AddressState.HasValue()) request.AddParameter("card[address_state]", card.ExpYear);
				if (card.AddressCountry.HasValue()) request.AddParameter("card[address_country]", card.ExpYear);
			}
			if (coupon.HasValue()) request.AddParameter("coupon", coupon);
			if (email.HasValue()) request.AddParameter("email", email);
			if (description.HasValue()) request.AddParameter("description", description);

			return Execute<StripeCustomer>(request);
		}

		public StripeCustomer DeleteCustomer(string customerId)
		{
			Require.Argument("customerId", customerId);

			var request = new RestRequest();
			request.Method = Method.DELETE;
			request.Resource = "customers/{customerId}";

			request.AddUrlSegment("customerId", customerId);

			return Execute<StripeCustomer>(request);
		}

		public StripeList<StripeCustomer> ListCustomers(int? count = null, int? offset = null)
		{
			var request = new RestRequest();
			request.Resource = "customers";

			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);

			return Execute<StripeList<StripeCustomer>>(request);
		}
	}
}