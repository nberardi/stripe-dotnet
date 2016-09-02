using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Script.Serialization;
using RestSharp;
using System.Net;

namespace Stripe
{
    /// <seealso href="https://stripe.com/docs/api"/>
    public partial class StripeClient
    {
        public string ApiVersion { get; set; }
        public string ApiEndpoint { get; set; }
        public string ApiKey { get; private set; }

        private readonly RestClient _client;

        public StripeClient(string apiKey)
            : this(apiKey, null, null)
        {
        }

        public StripeClient(string apiKey, string apiVersion, string apiEndpoint)
        {
            ApiVersion = "v1";
            ApiEndpoint = "https://api.stripe.com/";
            ApiKey = apiKey;
            
            // silverlight friendly way to get current version
            var assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = new AssemblyName(assembly.FullName);
            var version = assemblyName.Version;

            _client = new RestClient();
            _client.UserAgent = "stripe-dotnet/" + version;
            _client.Authenticator = new StripeAuthenticator(apiKey);
            _client.BaseUrl = new Uri(String.Format("{0}{1}", string.IsNullOrWhiteSpace(apiEndpoint) ? ApiEndpoint : apiEndpoint, ApiVersion));

            if (apiVersion.HasValue())
                _client.AddDefaultParameter("Stripe-Version", apiVersion, ParameterType.HttpHeader);
        }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public StripeObject ExecuteObject(RestRequest request)
        {
            request.OnBeforeDeserialization = (resp) =>
            {
                // for individual resources when there's an error to make
                // sure that RestException props are populated
                if (((int)resp.StatusCode) >= 400)
                    request.RootElement = "";
            };

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            var response = _client.Execute(request);
            var json = Deserialize(response.Content);
            var obj = new StripeObject();
            obj.SetModel(json);

            return obj;
        }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public StripeArray ExecuteArray(RestRequest request)
        {
            request.OnBeforeDeserialization = (resp) =>
            {
                // for individual resources when there's an error to make
                // sure that RestException props are populated
                if (((int)resp.StatusCode) >= 400)
                    request.RootElement = "";
            };

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            var response = _client.Execute(request);
            var json = Deserialize(response.Content);
            var obj = new StripeArray();
            obj.SetModel(json);

            return obj;
        }

        private IDictionary<string, object> Deserialize(string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;

            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<IDictionary<string, object>>(input);
        }

        private void AddDictionaryParameter(IDictionary<object, object> parameter, string objectName, RestRequest request)
        {
            foreach (var key in parameter.Keys)
            {
                request.AddParameter(string.Format("{0}[{1}]", objectName, key), parameter[key]);
            }
        }
    }
}
