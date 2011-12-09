using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stripe.Models
{
	public class PlanResponse : StripeBase
	{
		public int Amount { get; set; }
		public PlanFrequency Interval { get; set; }
		public string Name { get; set; }
		public string Currency { get; set; }
		public string Id { get; set; }
	}
}
