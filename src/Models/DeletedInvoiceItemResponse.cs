using System;
using System.Linq;

namespace Stripe.Models
{
	public class DeletedInvoiceItemResponse : StripeBase
	{
		public bool Deleted { get; set; }
		public string Id { get; set; }
	}
}
