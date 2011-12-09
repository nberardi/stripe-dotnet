using System;
using System.Linq;

namespace Stripe.Models
{
	public class CustomerSubscriptionResponse : StripeBase
	{
		public long CurrentPeriodEnd { get; set; }
		public string Status { get; set; }
		public CustomerSubscriptionPlanResponse Plan { get; set; }
		public long CurrentPeriodStart { get; set; }
		public long Start { get; set; }
		public long TrialStart { get; set; }
		public long TrialEnd { get; set; }
		public long? CanceledAt { get; set; }
		public long? EndedAt { get; set; }
		public string Customer { get; set; }
	}
}
