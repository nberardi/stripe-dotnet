using System;
using System.Linq;
using Newtonsoft.Json;

namespace Stripe.Models
{
	public class CreditCardResponse : StripeBase
	{
		public string Country { get; set; }
		public string CvcCheck { get; set; }
		public int ExpMonth { get; set; }
		public int ExpYear { get; set; }
		public string Last4 { get; set; }
		public string Type { get; set; }
	}
}
