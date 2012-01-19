using System;
using System.Linq;
using System.Reflection;
using RestSharp;

namespace Stripe
{
	/// <seealso href="https://stripe.com/docs/api"/>
	public partial class StripeClient
	{
		public string ApiVersion { get; set; }
		public string ApiEndpoint { get; set; }
		public string ApiKey { get; private set; }

		private RestClient _client;

		public StripeClient(string apiKey)
		{
			ApiVersion = "v1";
			ApiEndpoint = "https://api.stripe.com/";
			ApiKey = apiKey;

			// silverlight friendly way to get current version
			var assembly = Assembly.GetExecutingAssembly();
			AssemblyName assemblyName = new AssemblyName(assembly.FullName);
			var version = assemblyName.Version;

			_client = new RestClient();
			_client.UserAgent = "strip-csharp/" + version;
			_client.Authenticator = new StripeAuthenticator(apiKey);
			_client.BaseUrl = String.Format("{0}{1}", ApiEndpoint, ApiVersion);

			//_client.AddHandler("application/json", new JsonDeserializer());
			//_client.AddHandler("text/json", new JsonDeserializer());
			//_client.AddHandler("text/x-json", new JsonDeserializer());
			//_client.AddHandler("text/javascript", new JsonDeserializer());
			//_client.AddHandler("*", new JsonDeserializer());
		}

		/// <summary>
		/// Execute a manual REST request
		/// </summary>
		/// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
		/// <param name="request">The RestRequest to execute (will use client credentials)</param>
		public T Execute<T>(RestRequest request) where T : new()
		{
			request.OnBeforeDeserialization = (resp) =>
			{
				// for individual resources when there's an error to make
				// sure that RestException props are populated
				if (((int)resp.StatusCode) >= 400)
				{
					request.RootElement = "";
				}
			};

			var response = _client.Execute<T>(request);
			
			return response.Data;
		}

		/// <summary>
		/// Execute a manual REST request
		/// </summary>
		/// <param name="request">The RestRequest to execute (will use client credentials)</param>
		public RestResponse Execute(RestRequest request)
		{
			return _client.Execute(request);
		}
	}
}
