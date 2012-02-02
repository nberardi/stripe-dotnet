using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;


namespace Stripe.Tests
{
	[TestFixture]
	public class JsonPropertyNameEqualityComparerTest
	{
		[Test]
		public void Straight()
		{
			var eq = new JsonPropertyNameEqualityComparer();
			var s1 = "Test";
			var s2 = "Test";

			Assert.IsTrue(eq.Equals(s1, s2));
		}

		[Test]
		public void Different_Case()
		{
			var eq = new JsonPropertyNameEqualityComparer();
			var s1 = "Test";
			var s2 = "test";

			Assert.IsTrue(eq.Equals(s1, s2));
		}

		[Test]
		public void Different_Underscore()
		{
			var eq = new JsonPropertyNameEqualityComparer();
			var s1 = "TestProperty";
			var s2 = "Test_Property";

			Assert.IsTrue(eq.Equals(s1, s2));
		}

		[Test]
		public void Different_Underscore_And_Case()
		{
			var eq = new JsonPropertyNameEqualityComparer();
			var s1 = "TestProperty";
			var s2 = "test_property";

			Assert.IsTrue(eq.Equals(s1, s2));
		}
	}
}
