using System;
using System.Linq;

namespace Stripe.Models
{
	public class InvoiceItemResponse : StripeBase
	{
		public long Date { get; set; }
		public string Description { get; set; }
		public string Currency { get; set; }
		public int Amount { get; set; }
		public string Id { get; set; }
	}
}
