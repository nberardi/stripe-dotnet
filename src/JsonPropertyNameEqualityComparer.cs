using System;
using System.Collections.Generic;
using System.Linq;

namespace Stripe
{
	public class JsonPropertyNameEqualityComparer : IEqualityComparer<string>
	{
		#region IEqualityComparer<string> Members

		public bool Equals(string x, string y)
		{
			return GetHashCode(x) == GetHashCode(y);
		}

		public int GetHashCode(string obj)
		{
			return obj.ToLowerInvariant().Replace("_", "").GetHashCode();
		}

		#endregion
	}
}
