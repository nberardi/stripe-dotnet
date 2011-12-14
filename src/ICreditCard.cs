using RestSharp;

namespace Stripe
{
	public interface ICreditCard
	{
		void Validate();

		void AddParametersToRequest(RestRequest request);
	}
}