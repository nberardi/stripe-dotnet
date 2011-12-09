using System;
using System.Linq;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using Stripe.Models;

namespace Stripe
{
	public partial class StripeClient
	{
		public InvoiceItemResponse CreateInvoiceItem(string customerId, decimal amount, string currency, string description = null)
		{
			Require.Argument("customerId", customerId);
			Require.Argument("amount", amount);
			Require.Argument("currency", currency);

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "invoiceitems";

			request.AddParameter("customer", customerId);
			request.AddParameter("amount", Convert.ToInt32(amount * 100));
			request.AddParameter("currency", currency);
			if (description.HasValue()) request.AddParameter("description", description);

			return Execute<InvoiceItemResponse>(request);
		}

		public InvoiceItemResponse RetreiveInvoiceItem(string invoiceItemId)
		{
			Require.Argument("invoiceItemId", invoiceItemId);

			var request = new RestRequest();
			request.Resource = "invoiceitems/{invoiceItemId}";

			request.AddUrlSegment("invoiceItemId", invoiceItemId);

			return Execute<InvoiceItemResponse>(request);
		}

		public InvoiceItemResponse UpdateInvoiceItem(string invoiceItemId, decimal amount, string currency, string description = null)
		{
			Require.Argument("invoiceItemId", invoiceItemId);
			Require.Argument("amount", amount);
			Require.Argument("currency", currency);

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "invoiceitems/{invoiceItemId}";

			request.AddUrlSegment("invoiceItemId", invoiceItemId);

			request.AddParameter("amount", Convert.ToInt32(amount * 100));
			request.AddParameter("currency", currency);
			if (description.HasValue()) request.AddParameter("description", description);

			return Execute<InvoiceItemResponse>(request);
		}

		public DeletedInvoiceItemResponse DeleteInvoiceItem(string invoiceItemId)
		{
			Require.Argument("invoiceItemId", invoiceItemId);

			var request = new RestRequest();
			request.Method = Method.DELETE;
			request.Resource = "invoiceitems/{invoiceItemId}";

			request.AddUrlSegment("invoiceItemId", invoiceItemId);

			return Execute<DeletedInvoiceItemResponse>(request);
		}

		public ListResponse<CouponResponse> ListInvoiceItems(string customerId = null, int? count = null, int? offset = null)
		{
			var request = new RestRequest();
			request.Resource = "invoiceitems";

			if (customerId.HasValue()) request.AddParameter("customer", customerId);
			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);

			return Execute<ListResponse<CouponResponse>>(request);
		}

		public InvoiceResponse RetreiveInvoice(string invoiceId)
		{
			Require.Argument("invoiceId", invoiceId);

			var request = new RestRequest();
			request.Resource = "invoices/{invoiceId}";

			request.AddUrlSegment("invoiceId", invoiceId);

			return Execute<InvoiceResponse>(request);
		}

		public InvoiceResponse RetreiveCustomersUpcomingInvoice(string customerId)
		{
			Require.Argument("customerId", customerId);

			var request = new RestRequest();
			request.Resource = "invoices/upcoming";

			request.AddParameter("customer", customerId);

			return Execute<InvoiceResponse>(request);
		}

		public ListResponse<InvoiceResponse> ListInvoices(string customerId = null, int? count = null, int? offset = null)
		{
			var request = new RestRequest();
			request.Resource = "invoices";

			if (customerId.HasValue()) request.AddParameter("customer", customerId);
			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);

			return Execute<ListResponse<InvoiceResponse>>(request);
		}
	}
}
