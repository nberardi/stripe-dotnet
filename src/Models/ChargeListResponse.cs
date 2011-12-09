using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Stripe.Models
{
	[JsonObject(MemberSerialization.OptIn)]
	public class ChargeListResponse
	{
		[JsonProperty(PropertyName = "data")]
		public IList<ChargeResponse> Data { get; set; }
	}
}
