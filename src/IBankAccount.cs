using RestSharp;

namespace Stripe
{
    public interface IBankAccount : IObjectValidation
    {
        void AddParametersToRequest_Old(RestRequest request);
    }
}
