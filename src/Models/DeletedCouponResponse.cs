using System;
using System.Linq;

namespace Stripe.Models
{
	public class DeletedCouponResponse : StripeBase
	{
		public bool Deleted { get; set; }
		public string Id { get; set; }
	}
}
