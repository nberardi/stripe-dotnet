using System;
using System.Linq;

namespace Stripe
{
	public static class DateTimeExtensions
	{
		private static DateTimeOffset epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

		public static long ToUnixEpoch(this DateTimeOffset dt)
		{
			dt = dt.ToUniversalTime();
			return Convert.ToInt64((dt - epoch).TotalSeconds);
		}
	}
}
