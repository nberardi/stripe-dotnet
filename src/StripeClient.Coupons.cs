using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using RestSharp.Validation;


namespace Stripe
{
	public partial class StripeClient
	{
        /// <summary>
        /// You can create coupons easily via the coupon management page of the Stripe dashboard. Coupon creation is also accessible via the API if you need to create coupons on the fly.
        /// </summary>
        /// <param name="duration">Specifies how long the discount will be in effect. Can be forever, once, or repeating.</param>
        /// <param name="amountOff">A positive integer representing the amount to subtract from an invoice total (required if percent_off is not passed)</param>
        /// <param name="percentOff">A positive integer between 1 and 100 that represents the discount the coupon will apply (required if amount_off is not passed)</param>
        /// <param name="couponId">Unique string of your choice that will be used to identify this coupon when applying it a customer.</param>
        /// <param name="durationInMonths">Required only if duration is repeating, in which case it must be a positive integer that specifies the number of months the discount will be in effect.</param>
        /// <param name="maxRedemptions">A positive integer specifying the number of times the coupon can be redeemed before it’s no longer valid.</param>
        /// <param name="redeemBy">Unix timestamp specifying the last time at which the coupon can be redeemed. After the redeem_by date, the coupon can no longer be applied to new customers.</param>
        /// <param name="currency">Currency of the amount_off parameter (required if amount_off is passed)</param>
        /// <param name="metaData">A set of key/value pairs that you can attach to a coupon object.</param>
        /// <returns>Returns the coupon object.</returns>
		public StripeObject CreateCoupon(CouponDuration duration, int? amountOff = null, int? percentOff = null, 
            string couponId = null, int? durationInMonths = null, int? maxRedemptions = null,
            DateTimeOffset? redeemBy = null, string currency = null, Dictionary<object, object> metaData = null)
		{
			Require.Argument("duration", duration);

            if (amountOff.HasValue)
                Require.Argument("currency", currency);

            if (!amountOff.HasValue)
                Require.Argument("percentOff", percentOff);

            if (!percentOff.HasValue)
                Require.Argument("amountOff", amountOff);

			if (percentOff.HasValue) 
                Validate.IsBetween(percentOff.Value, 1, 100);

            if (amountOff.HasValue && amountOff.Value < 1) 
                throw new ArgumentException("amount_off cannot be 0 or negative");

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

            request.AddParameter("duration", duration.ToString().ToLowerInvariant());

			if (couponId.HasValue()) request.AddParameter("id", couponId);
            if (amountOff.HasValue) request.AddParameter("amount_off", amountOff);
			if (percentOff.HasValue) request.AddParameter("percent_off", percentOff);
            if (currency.HasValue()) request.AddParameter("currency", currency);
			if (durationInMonths.HasValue) request.AddParameter("duration_in_months", durationInMonths.Value);
			if (maxRedemptions.HasValue) request.AddParameter("max_redemptions", maxRedemptions.Value);
			if (redeemBy.HasValue) request.AddParameter("redeem_by", redeemBy.Value.ToUnixEpoch());
            if (metaData != null) AddDictionaryParameter(metaData, "metadata", request);

			return ExecuteObject(request);
		}

        /// <summary>
        /// Retrieves the coupon with the given ID.
        /// </summary>
        /// <param name="couponId">The ID of the desired coupon.</param>
        /// <returns>Returns a coupon if a valid coupon ID was provided. Returns an error otherwise.</returns>
		public StripeObject RetreiveCoupon(string couponId)
		{
			Require.Argument("couponId", couponId);

			var request = new RestRequest();
            request.Method = Method.GET;
			request.Resource = "coupons/{couponId}";

			request.AddUrlSegment("couponId", couponId);

			return ExecuteObject(request);
		}

        /// <summary>
        /// Updates the metadata of a coupon. Other coupon details (currency, duration, amount_off) are, by design, not editable.
        /// </summary>
        /// <param name="couponId">The identifier of the coupon to be updated.</param>
        /// <param name="metadata">A set of key/value pairs that you can attach to a coupon object.</param>
        /// <returns>The newly updated coupon object if the call succeeded.</returns>
        public StripeObject UpdateCoupon(string couponId, IDictionary<string, string> metadata = null)
        {
            Require.Argument("couponId", couponId);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "coupons/{couponId}";

            request.AddUrlSegment("couponId", couponId);

            return ExecuteObject(request);
        }

        /// <summary>
        /// You can delete coupons via the coupon management page of the Stripe dashboard. However, deleting a coupon does not affect any customers who have already applied the coupon; it means that new customers can’t redeem the coupon. You can also delete coupons via the API.
        /// </summary>
        /// <param name="couponId">The identifier of the coupon to be deleted.</param>
        /// <returns>An object with the deleted coupon’s ID and a deleted flag upon success.</returns>
		public StripeObject DeleteCoupon(string couponId)
		{
			Require.Argument("couponId", couponId);

			var request = new RestRequest();
			request.Method = Method.DELETE;
			request.Resource = "coupons/{couponId}";

			request.AddUrlSegment("couponId", couponId);

			return ExecuteObject(request);
		}

        /// <summary>
        /// Returns a list of your coupons.
        /// </summary>
        /// <param name="created"> A filter on the list based on the object created field. The value can be a string with an integer Unix timestamp</param>
        /// <param name="endingBefore">A cursor for use in pagination. ending_before is an object ID that defines your place in the list.</param>
        /// <param name="limit">A limit on the number of objects to be returned. Limit can range between 1 and 100 items.</param>
        /// <param name="startingAfter">A cursor for use in pagination. starting_after is an object ID that defines your place in the list.</param>
        /// <returns>A dictionary with a data property that contains an array of up to limit coupons, starting after coupon starting_after. Each entry in the array is a separate coupon object.</returns>
		public StripeArray ListCoupons(string created = null, string endingBefore = null,
            int limit = 10, string startingAfter = null)
		{
			var request = new RestRequest();
            request.Method = Method.GET;
			request.Resource = "coupons";

            request.AddParameter("limit", limit, ParameterType.QueryString);

            if (created.HasValue()) request.AddParameter("created", created);
            if (endingBefore.HasValue()) request.AddParameter("ending_before", endingBefore);
            if (startingAfter.HasValue()) request.AddParameter("starting_after", startingAfter);

			return ExecuteArray(request);
		}
	}
}
