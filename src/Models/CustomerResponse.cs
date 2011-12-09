using System;
using System.Linq;

namespace Stripe.Models
{
	public class CustomerResponse : StripeBase
	{
		public string Description { get; set; }
		public bool LiveMode { get; set; }
		public long Created { get; set; }
		public CreditCardResponse ActiveCard { get; set; }
		public string Id { get; set; }
	}
}
