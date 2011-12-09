using System;
using System.Linq;
using Newtonsoft.Json;

namespace Stripe.Models
{
	/// <seealso href="https://github.com/xamarin/XamarinStripe/tree/master/XamarinStripe">Thanks To Xamarin</seealso>
	[JsonObject(MemberSerialization.OptIn)]
	public class ChargeResponse
	{
		[JsonProperty(PropertyName = "amount")]
		public decimal Amount { get; set; }

		[JsonProperty(PropertyName = "fee")]
		public decimal Fee { get; set; }

		[JsonProperty(PropertyName = "currency")]
		public string Currency { get; set; }

		[JsonProperty(PropertyName = "created")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? Created { get; set; }

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "id")]
		public string ID { get; set; }

		[JsonProperty(PropertyName = "attempted")]
		public bool Attempted { get; set; }
		
		[JsonProperty(PropertyName = "refunded")]
		public bool Refunded { get; set; }
		
		[JsonProperty(PropertyName = "paid")]
		public bool Paid { get; set; }
		
		[JsonProperty(PropertyName = "livemode")]
		public bool LiveMode { get; set; }

		[JsonProperty(PropertyName = "card")]
		public CreditCardResponse Card { get; set; }
	}
}
