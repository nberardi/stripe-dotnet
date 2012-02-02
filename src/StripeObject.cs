using System;
using System.Linq;

namespace Stripe
{
	public class StripeObject : JsonObject
	{
		public bool IsError { get { return HasProperty("error"); } }
	}
}
