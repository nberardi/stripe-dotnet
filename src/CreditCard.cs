using System;
using System.Linq;
using RestSharp;
using RestSharp.Validation;

namespace Stripe
{
	public class CreditCard : ICreditCard
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

		/// <summary>
		/// Preforms basic validation to make sure required values are present.
		/// </summary>
		void ICreditCard.Validate()
		{
			Require.Argument("card[number]", Number);
			Require.Argument("card[exp_month]", ExpMonth);
			Require.Argument("card[exp_year]", ExpYear);

			Validate.IsBetween(ExpMonth, 1, 12);
			Validate.IsBetween(ExpYear, DateTime.Now.Year, 2050);
		}

		void ICreditCard.AddParametersToRequest(RestRequest request)
		{
			request.AddParameter("card[number]", Number);
			request.AddParameter("card[exp_month]", ExpMonth);
			request.AddParameter("card[exp_year]", ExpYear);
			if (Cvc.HasValue()) request.AddParameter("card[cvc]", Cvc);
			if (Name.HasValue()) request.AddParameter("card[name]", Name);
			if (AddressLine1.HasValue()) request.AddParameter("card[address_line1]", AddressLine1);
			if (AddressLine2.HasValue()) request.AddParameter("card[address_line2]", AddressLine2);
			if (AddressZip.HasValue()) request.AddParameter("card[address_zip]", AddressZip);
			if (AddressState.HasValue()) request.AddParameter("card[address_state]", AddressState);
			if (AddressCountry.HasValue()) request.AddParameter("card[address_country]", AddressCountry);
		}
	}
}
