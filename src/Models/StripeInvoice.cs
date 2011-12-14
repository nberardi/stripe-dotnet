using System;
using System.Collections.Generic;
using System.Linq;

namespace Stripe.Models
{
	public class StripeInvoice : StripeBase
	{
		public DateTimeOffset? Created { get; set; }
		public int SubTotal { get; set; }
		public int Total { get; set; }
		public LinesWrapper Lines { get; set; }
		public string Id { get; set; }

		public bool IsUpcoming { get { return Id == null; } }

		public class LinesWrapper : IEnumerable<StripeInvoiceItem>
		{
			public List<StripeInvoiceItem> InvoiceItems { get; set; }

			#region IEnumerable<InvoiceItemResponse> Members

			public IEnumerator<StripeInvoiceItem> GetEnumerator()
			{
				return InvoiceItems.GetEnumerator();
			}

			#endregion

			#region IEnumerable Members

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return InvoiceItems.GetEnumerator();
			}

			#endregion
		}
	}
}
