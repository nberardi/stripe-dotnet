using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
	public class CouponTests
	{
		private StripeClient _client;

		public CouponTests()
		{
			_client = new StripeClient(Constants.ApiKey);
		}

		[Fact]
		public void CreateCoupon_Test()
		{
			dynamic response = _client.CreateCoupon(75, CouponDuration.Once);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.NotNull(response.Id);
		}

		[Fact]
		public void RetrieveCoupon_Test()
		{
			dynamic coupon = _client.CreateCoupon(75, CouponDuration.Once);
			dynamic response = _client.RetreiveCoupon(coupon.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.Equal(coupon.Id, response.Id);
		}

		[Fact]
		public void DeleteCustomer_Test()
		{
			dynamic coupon = _client.CreateCoupon(75, CouponDuration.Once);
			dynamic response = _client.DeleteCoupon(coupon.Id);

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.True(response.Deleted);
			Assert.Equal(coupon.Id, response.Id);
		}

		[Fact]
		public void ListCoupons_Test()
		{
			StripeArray response = _client.ListCoupons();

			Assert.NotNull(response);
			Assert.False(response.IsError);
			Assert.True(response.Any());
		}
	}
}
