using System;
using System.Linq;
using RestSharp;
using RestSharp.Validation;


namespace Stripe
{
	public partial class StripeClient
	{
		public StripeObject RetrieveAccount()
		{
			var request = new RestRequest();
			request.Resource = "account";

			return ExecuteObject(request);
		}
	}
}