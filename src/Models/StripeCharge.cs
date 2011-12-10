using System;
using System.Linq;

namespace Stripe.Models
{
	public class StripeCharge : StripeBase
	{
		public int Amount { get; set; }
		public int Fee { get; set; }
		public string Currency { get; set; }
		public long Created { get; set; }
		public string Description { get; set; }
		public string Id { get; set; }
		public bool Attempted { get; set; }
		public bool Refunded { get; set; }
		public bool Paid { get; set; }
		public bool LiveMode { get; set; }
		public StripeCard Card { get; set; }
	}
}
