using System;
using RestSharp;
using RestSharp.Validation;

namespace Stripe
{
    public class CreditCard : ICreditCard
    {
        /// <summary>
        /// Token provided from Stripe.js after card creation
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Cardholder's full name
        /// </summary>
        public string Name { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressCity { get; set; }
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
        /// Performs basic validation to make sure required values are present.
        /// </summary>
        void IObjectValidation.Validate()
        {
            Require.Argument("number", Number);
            Require.Argument("exp_month", ExpMonth);
            Require.Argument("exp_year", ExpYear);

            Validate.IsBetween(ExpMonth, 1, 12);
            Validate.IsBetween(ExpYear, DateTime.Now.Year, int.MaxValue);
        }

        void IObjectValidation.AddParametersToRequest(RestRequest request)
        {
            if (Token.HasValue())
            {
                request.AddParameter("source", Token);
            }
            else
            {
                request.AddParameter("external_account[object]", "card");
                request.AddParameter("external_account[number]", Number);
                request.AddParameter("external_account[exp_month]", ExpMonth);
                request.AddParameter("external_account[exp_year]", ExpYear);
                if (Cvc.HasValue()) request.AddParameter("external_account[cvc]", Cvc);
                if (Name.HasValue()) request.AddParameter("external_account[name]", Name);
                if (AddressLine1.HasValue()) request.AddParameter("external_account[address_line1]", AddressLine1);
                if (AddressLine2.HasValue()) request.AddParameter("external_account[address_line2]", AddressLine2);
                if (AddressCity.HasValue()) request.AddParameter("external_account[address_city]", AddressCity);
                if (AddressState.HasValue()) request.AddParameter("external_account[address_state]", AddressState);
                if (AddressZip.HasValue()) request.AddParameter("external_account[address_zip]", AddressZip);
                if (AddressCountry.HasValue()) request.AddParameter("external_account[address_country]", AddressCountry);
            }
        }

        public void AddParametersToRequest_Source(RestRequest request)
        {
            if (Token.HasValue())
            {
                request.AddParameter("source", Token);
            }
            else
            {
                request.AddParameter("source[object]", "card");
                request.AddParameter("source[number]", Number);
                request.AddParameter("source[exp_month]", ExpMonth);
                request.AddParameter("source[exp_year]", ExpYear);
                if (Cvc.HasValue()) request.AddParameter("source[cvc]", Cvc);
                if (Name.HasValue()) request.AddParameter("source[name]", Name);
                if (AddressLine1.HasValue()) request.AddParameter("source[address_line1]", AddressLine1);
                if (AddressLine2.HasValue()) request.AddParameter("source[address_line2]", AddressLine2);
                if (AddressCity.HasValue()) request.AddParameter("source[address_city]", AddressCity);
                if (AddressState.HasValue()) request.AddParameter("source[address_state]", AddressState);
                if (AddressZip.HasValue()) request.AddParameter("source[address_zip]", AddressZip);
                if (AddressCountry.HasValue()) request.AddParameter("source[address_country]", AddressCountry);
            }
        }

        public void AddParametersToRequest_Card(RestRequest request)
        {
            if (Token.HasValue())
            {
                request.AddParameter("card", Token);
            }
            else
            {
                request.AddParameter("card[object]", "card");
                request.AddParameter("card[number]", Number);
                request.AddParameter("card[exp_month]", ExpMonth);
                request.AddParameter("card[exp_year]", ExpYear);
                if (Cvc.HasValue()) request.AddParameter("card[cvc]", Cvc);
                if (Name.HasValue()) request.AddParameter("card[name]", Name);
                if (AddressLine1.HasValue()) request.AddParameter("card[address_line1]", AddressLine1);
                if (AddressLine2.HasValue()) request.AddParameter("card[address_line2]", AddressLine2);
                if (AddressCity.HasValue()) request.AddParameter("card[address_city]", AddressCity);
                if (AddressState.HasValue()) request.AddParameter("card[address_state]", AddressState);
                if (AddressZip.HasValue()) request.AddParameter("card[address_zip]", AddressZip);
                if (AddressCountry.HasValue()) request.AddParameter("card[address_country]", AddressCountry);
            }
        }

        public void AddParametersToRequest_Update(RestRequest request)
        {
            if (ExpMonth > default(int)) request.AddParameter("exp_month", ExpMonth);
            if (ExpYear > default(int)) request.AddParameter("exp_year", ExpYear);
            if (Name.HasValue()) request.AddParameter("name", Name);
            if (AddressLine1.HasValue()) request.AddParameter("address_line1", AddressLine1);
            if (AddressLine2.HasValue()) request.AddParameter("address_line2", AddressLine2);
            if (AddressCity.HasValue()) request.AddParameter("address_city", AddressCity);
            if (AddressState.HasValue()) request.AddParameter("address_state", AddressState);
            if (AddressZip.HasValue()) request.AddParameter("address_zip", AddressZip);
            if (AddressCountry.HasValue()) request.AddParameter("address_country", AddressCountry);
        }
    }
}
