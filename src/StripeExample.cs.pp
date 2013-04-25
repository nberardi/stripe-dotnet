using System;
using Stripe;

namespace $rootnamespace$
{
	public static class Program
	{
		public static void Main()
		{
			var apiKey = "Your API Key"; // can be found here https://manage.stripe.com/#account/apikeys
			var api = new StripeClient(apiKey); // you can learn more about the api here https://stripe.com/docs/api

			var card = new CreditCard {
				Number = "4242424242424242",
				ExpMonth = 3,
				ExpYear = 2015,
				Cvc = "123"
			};

			dynamic response = api.CreateCharge(
				amount: 100.00m, // $100
				currency: "usd",
				card: card);

			if (!response.IsError && response.Paid)
				Console.WriteLine("Whoo Hoo...  We made our first sale!");
			else
				Console.WriteLine("Payment failed. :(");

			Console.Read();
		}
	}
}
