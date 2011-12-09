using System;
using System.Linq;
using Newtonsoft.Json;

namespace Stripe.Models
{
	/// <seealso href="https://github.com/xamarin/XamarinStripe/tree/master/XamarinStripe">Thanks To Xamarin</seealso>
	[JsonObject(MemberSerialization.OptIn)]
	public class CreditCardResponse
	{
		[JsonProperty(PropertyName = "country")]
		public string Country { get; set; }
		[JsonProperty(PropertyName = "cvc_check")]
		public string CvcCheck { get; set; }

		[JsonProperty(PropertyName = "exp_month")]
		public int ExpirationMonth { get; set; }
		[JsonProperty(PropertyName = "exp_year")]
		public int ExpirationYear { get; set; }

		[JsonProperty(PropertyName = "last4")]
		public string Last4 { get; set; }
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }
	}
}
