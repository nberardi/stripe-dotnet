using System;
using System.Linq;
using RestSharp;
using RestSharp.Validation;


namespace Stripe
{
	public partial class StripeClient
	{
		public StripeObject CreateCoupon(int percentOff, CouponDuration duration, string couponId = null, int? durationInMonths = null, int? maxRedemptions = null, DateTimeOffset? redeemBy = null)
		{
			Require.Argument("percentOff", percentOff);
			Require.Argument("duration", duration);

			Validate.IsBetween(percentOff, 1, 100);

			if (duration == CouponDuration.Repeating)
			{
				Require.Argument("durationInMonths", durationInMonths);
				Validate.IsBetween(durationInMonths.Value, 0, Int32.MaxValue);
			}
			else if (durationInMonths.HasValue && duration != CouponDuration.Repeating)
			{
				throw new ArgumentException("'durationInMonths' is only valid when 'duration' is set to 'Repeating'", "durationInMonths");
			}

			var request = new RestRequest();
			request.Method = Method.POST;
			request.Resource = "coupons";

			if (couponId.HasValue()) request.AddParameter("id", couponId);
			request.AddParameter("percent_off", percentOff);
			request.AddParameter("duration", duration.ToString().ToLowerInvariant());
			if (durationInMonths.HasValue) request.AddParameter("duration_in_months", durationInMonths.Value);
			if (maxRedemptions.HasValue) request.AddParameter("max_redemptions", maxRedemptions.Value);
			if (redeemBy.HasValue) request.AddParameter("redeem_by", redeemBy.Value.ToUnixEpoch());

			return ExecuteObject(request);
		}

		public StripeObject RetreiveCoupon(string couponId)
		{
			Require.Argument("couponId", couponId);

			var request = new RestRequest();
			request.Resource = "coupons/{couponId}";

			request.AddUrlSegment("couponId", couponId);

			return ExecuteObject(request);
		}

		public StripeObject DeleteCoupon(string couponId)
		{
			Require.Argument("couponId", couponId);

			var request = new RestRequest();
			request.Method = Method.DELETE;
			request.Resource = "coupons/{couponId}";

			request.AddUrlSegment("couponId", couponId);

			return ExecuteObject(request);
		}

		public StripeArray ListCoupons(int? count = null, int? offset = null)
		{
			var request = new RestRequest();
			request.Resource = "coupons";

			if (count.HasValue) request.AddParameter("count", count.Value);
			if (offset.HasValue) request.AddParameter("offset", offset.Value);

			return ExecuteArray(request);
		}
	}
}
