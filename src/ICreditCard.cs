using RestSharp;

namespace Stripe
{
    public interface ICreditCard : IObjectValidation
    {
        void AddParametersToRequest_Card(RestRequest request);
        void AddParametersToRequest_Source(RestRequest request);
        void AddParametersToRequest_Update(RestRequest request);
    }
}