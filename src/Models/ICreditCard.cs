using RestSharp;

namespace Stripe
{
	public interface ICreditCard : IObjectValidation {
        void AddParametersToRequest_Old(RestRequest request);
    }
}