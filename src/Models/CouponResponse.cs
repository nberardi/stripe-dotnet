using System;
using System.Linq;

namespace Stripe.Models
{
	public class CouponResponse : StripeBase
	{
		public CouponDuration Duration { get; set; }
		public int? DurationInMonths { get; set; }
		public int PercentOff { get; set; }
		public string Id { get; set; }
	}
}
