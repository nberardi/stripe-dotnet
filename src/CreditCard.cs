using System;
using System.Linq;

namespace Stripe
{
	public class CreditCard
	{
		/// <summary>
		/// Cardholder's full name
		/// </summary>
		public string Name { get; set; }

		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string AddressState { get; set; }
		public string AddressZip { get; set; }
		public string AddressCountry { get; set; }

		/// <summary>
		/// The card number, as a string without any separators.
		/// </summary>
		public string Number { get; set; }

		/// <summary>
		/// Card security code
		/// </summary>
		public string Cvc { get; set; }
		
		/// <summary>
		/// Two digit number representing the card's expiration month.
		/// </summary>
		public int ExpMonth { get; set; }

		/// <summary>
		/// Four digit number representing the card's expiration year.
		/// </summary>
		public int ExpYear { get; set; }

		/// <summary>
		/// Alternative method for setting expiration date.
		/// </summary>
		public DateTime Expiration
		{
			get { return new DateTime(ExpYear, ExpMonth, 1); }
			set
			{
				ExpMonth = value.Month;
				ExpYear = value.Year;
			}
		}
	}
}
