using System;
using System.Collections.Generic;
using System.Linq;

namespace Stripe.Models
{
	public class StripeList<T> : StripeBase
	{
		public int Count { get; set; }
		public List<T> Data { get; set; }
	}
}
