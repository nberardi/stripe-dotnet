using System;
using System.Linq;
using Newtonsoft.Json;

namespace Stripe.Models
{
	public abstract class StripeBase
	{
		public bool IsError { get { return Error != null; } }
		public Error Error { get; set; }
	}
}
