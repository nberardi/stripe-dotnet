using System;
using System.Collections.Generic;
using System.Linq;

namespace Stripe.Models
{
	public class CustomerListResponse : StripeBase
	{
		public int Count { get; set; }
		public List<CustomerResponse> Data { get; set; }
	}
}
