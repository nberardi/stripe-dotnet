using System;
using System.Linq;
using NUnit.Framework;

namespace Stripe.Tests
{
	[TestFixture]
	public class CouponTests
	{
		private StripeClient _client;

		[TestFixtureSetUp]
		public void Setup()
		{
			_client = new StripeClient(Constants.ApiKey);
		}

		[Test]
		public void CreateCoupon_Test()
		{
			dynamic response = _client.CreateCoupon(75, CouponDuration.Once);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Id);
		}

		[Test]
		public void RetrieveCoupon_Test()
		{
			dynamic coupon = _client.CreateCoupon(75, CouponDuration.Once);
			dynamic response = _client.RetreiveCoupon(coupon.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(coupon.Id, response.Id);
		}

		[Test]
		public void DeleteCustomer_Test()
		{
			dynamic coupon = _client.CreateCoupon(75, CouponDuration.Once);
			dynamic response = _client.DeleteCoupon(coupon.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Deleted);
			Assert.AreEqual(coupon.Id, response.Id);
		}

		[Test]
		public void ListCoupons_Test()
		{
			dynamic response = _client.ListCoupons();

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Count > 0);
		}
	}
}
