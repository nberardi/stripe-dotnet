using System;
using System.Linq;
using NUnit.Framework;
using Stripe.Models;

namespace Stripe.Tests
{
	[TestFixture]
	public class CouponTests
	{
		private StripeClient _client;

		[SetUp]
		public void Setup()
		{
			_client = new StripeClient(Constants.ApiKey);
		}

		[Test]
		public void CreateCoupon_Test()
		{
			var response = _client.CreateCoupon(75, CouponDuration.Once);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Id);
		}

		[Test]
		public void RetrieveCoupon_Test()
		{
			var coupon = _client.CreateCoupon(75, CouponDuration.Once);
			var response = _client.RetreiveCoupon(coupon.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.AreEqual(coupon.Id, response.Id);
		}

		[Test]
		public void DeleteCustomer_Test()
		{
			var coupon = _client.CreateCoupon(75, CouponDuration.Once);
			var response = _client.DeleteCoupon(coupon.Id);

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsTrue(response.Deleted);
			Assert.AreEqual(coupon.Id, response.Id);
		}

		[Test]
		public void ListCoupons_Test()
		{
			var response = _client.ListCoupons();

			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsError);
			Assert.IsNotNull(response.Data);
			Assert.IsTrue(response.Data.Count > 0);
		}
	}
}
