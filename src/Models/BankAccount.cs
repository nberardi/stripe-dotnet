using System;
using RestSharp;
using RestSharp.Validation;

namespace Stripe
{
	public class BankAccount : IObjectValidation
	{
		public string Country { get; set; }
		public string RoutingNumber { get; set; }
		public string AccountNumber { get; set; }

		/// <summary>
		/// Performs basic validation to make sure required values are present.
		/// </summary>
		void IObjectValidation.Validate()
		{
			Require.Argument("bank_account[country]", Country);
			Require.Argument("bank_account[routing_number]", RoutingNumber);
			Require.Argument("bank_account[account_number]", AccountNumber);
		}

		void IObjectValidation.AddParametersToRequest(RestRequest request)
		{
			request.AddParameter("bank_account[country]", Country);
			request.AddParameter("bank_account[routing_number]", RoutingNumber);
			request.AddParameter("bank_account[account_number]", AccountNumber);
		}
	}
}