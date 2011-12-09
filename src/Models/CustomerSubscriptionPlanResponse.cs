using System;
using System.Linq;

namespace Stripe.Models
{
	public class CustomerSubscriptionPlanResponse
	{
		public int Amount { get; set; }
		public PlanFrequency Interval { get; set; }
		public int TrialPeriodDays { get; set; }
		public string Id { get; set; }
	}
}
