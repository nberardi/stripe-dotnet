using System;
using RestSharp;

namespace Stripe
{
	public interface IObjectValidation
	{
		void Validate();

		void AddParametersToRequest(RestRequest request);
	}
}