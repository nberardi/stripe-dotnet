using System;
using System.Linq;
using Newtonsoft.Json;

namespace Stripe.Models
{
	public class Error
	{
		public string Type { get; set; }
		public string Message { get; set; }
		public string Code { get; set; }
		public string Param { get; set; }
	}
}