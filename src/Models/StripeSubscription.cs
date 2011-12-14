using System;
using System.Linq;

namespace Stripe.Models
{
	public class StripeSubscription : StripeBase
	{
		public DateTimeOffset CurrentPeriodEnd { get; set; }
		public string Status { get; set; }
		public StripeSubscriptionPlan Plan { get; set; }
		public DateTimeOffset CurrentPeriodStart { get; set; }
		public DateTimeOffset Start { get; set; }
		public DateTimeOffset TrialStart { get; set; }
		public DateTimeOffset TrialEnd { get; set; }
		public DateTimeOffset? CanceledAt { get; set; }
		public DateTimeOffset? EndedAt { get; set; }
		public string Customer { get; set; }
	}
}
