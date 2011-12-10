using System;
using System.Linq;

namespace Stripe.Models
{
	public class StripeCustomer : StripeBase
	{
		public string Description { get; set; }
		public bool LiveMode { get; set; }
		public long Created { get; set; }
		public StripeCard ActiveCard { get; set; }
		public string Id { get; set; }
		public bool Deleted { get; set; }
	}
}
