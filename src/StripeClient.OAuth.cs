using System;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Validation;


namespace Stripe
{
	public partial class StripeClient
	{
		public string CreateOAuthAuthorizationUrl(string clientId, string scope = "read_only", string landing = "login", string state = "")
		{
			if (string.IsNullOrWhiteSpace(clientId))
			{
				throw new ArgumentNullException("clientId");
			}

			var url = new StringBuilder();
			url.Append(string.Format("https://connect.stripe.com/oauth/authorize?response_type=code&client_id={0}", clientId));

			if (!string.IsNullOrWhiteSpace(scope))
			{
				url.Append(string.Format("&scope={0}", scope));
			}

			if (!string.IsNullOrWhiteSpace(landing))
			{
				url.Append(string.Format("&stripe_landing={0}", landing));
			}

			if (!string.IsNullOrWhiteSpace(state))
			{
				url.Append(string.Format("&state={0}", state));
			}

			return url.ToString();
		}

		public StripeObject RetrieveOAuthToken(string code)
		{
			Require.Argument("code", code);

			var request = new RestRequest();
			request.Method = Method.POST;
			request.AddHeader("Authorization", string.Concat("Bearer ", this.ApiKey));
			request.Resource = "oauth/token?code={code}&grant_type=authorization_code";
			request.AddUrlSegment("code", code);

			return ExecuteConnectObject(request);
		}
	}
}