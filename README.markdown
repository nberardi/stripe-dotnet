# About the Library

Stripe is a simple, developer-friendly way to accept payments online. We believe that enabling transactions on the web is a problem rooted in code, not finance, and we want to help put more websites in business.

Currently, the library is feature complete for the Stripe API which can be found here: https://stripe.com/docs/api

# How can I install it using NuGet?

To install Stripe, run the following command in the Package Manager Console

```
PM> Install-Package Stripe
```

# How can I start using this library?

```csharp
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
```

# Authors

*	Nick Berardi (https://github.com/nberardi)
*	Paul Irwin (https://github.com/paulirwin)
*	Giovani Brady (https://github.com/Giovani-Brady)