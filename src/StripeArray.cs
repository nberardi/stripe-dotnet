using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Stripe
{
	public class StripeArray : IEnumerable<JsonObject>
	{
		private JsonObject _internalJson;

		public bool IsError { get { return _internalJson.HasProperty("error"); } }

		public JsonObject Error { get { return (JsonObject)_internalJson.GetProperty("error"); } }

		internal void SetModel(IDictionary<string, object> model)
		{
			_internalJson = new JsonObject();
			_internalJson.SetModel(model);
		}

		#region IEnumerable<JsonObject> Members

		public IEnumerator<JsonObject> GetEnumerator()
		{
			object data = _internalJson.GetProperty("data");

			if (data != null)
				return ((List<object>)data).OfType<JsonObject>().GetEnumerator();

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
