using System;
using System.Linq;

namespace Stripe.Models
{
	public class StripePlan : StripeBase
	{
		public int Amount { get; set; }
		public PlanFrequency Interval { get; set; }
		public string Name { get; set; }
		public string Currency { get; set; }
		public string Id { get; set; }
		public bool Deleted { get; set; }
	}
}
