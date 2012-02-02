using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Stripe
{
	public class StripeArray : JsonObject, IEnumerable<JsonObject>
	{
		public bool IsError { get { return HasProperty("error"); } }

		#region IEnumerable<JsonObject> Members

		public IEnumerator<JsonObject> GetEnumerator()
		{
			object data;
			TryGetProperty("data", out data);

			if (data != null)
				return ((IEnumerable<JsonObject>)data).GetEnumerator();

			return new List<JsonObject>(0).GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
