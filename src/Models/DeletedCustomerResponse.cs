using System;
using System.Linq;

namespace Stripe.Models
{
	public class DeletedCustomerResponse : StripeBase
	{
		public bool Deleted { get; set; }
		public string Id { get; set; }
	}
}
