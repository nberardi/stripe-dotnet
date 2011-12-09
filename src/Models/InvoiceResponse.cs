using System;
using System.Collections.Generic;
using System.Linq;

namespace Stripe.Models
{
	public class InvoiceResponse : StripeBase
	{
		public long? Created { get; set; }
		public int SubTotal { get; set; }
		public int Total { get; set; }
		public LinesWrapperResponse Lines { get; set; }
		public string Id { get; set; }

		public bool IsUpcoming { get { return Id == null; } }

		public class LinesWrapperResponse : IEnumerable<InvoiceItemResponse>
		{
			public List<InvoiceItemResponse> InvoiceItems { get; set; }

			#region IEnumerable<InvoiceItemResponse> Members

			public IEnumerator<InvoiceItemResponse> GetEnumerator()
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
