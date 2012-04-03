using System;
using System.Linq;
using Xunit;

namespace Stripe.Tests
{
	public class JsonPropertyNameEqualityComparerTest
	{
		[Fact]
		public void Straight()
		{
			var eq = new JsonPropertyNameEqualityComparer();
			var s1 = "Test";
			var s2 = "Test";

			Assert.True(eq.Equals(s1, s2));
		}

		[Fact]
		public void Different_Case()
		{
			var eq = new JsonPropertyNameEqualityComparer();
			var s1 = "Test";
			var s2 = "test";

			Assert.True(eq.Equals(s1, s2));
		}

		[Fact]
		public void Different_Underscore()
		{
			var eq = new JsonPropertyNameEqualityComparer();
			var s1 = "TestProperty";
			var s2 = "Test_Property";

			Assert.True(eq.Equals(s1, s2));
		}

		[Fact]
		public void Different_Underscore_And_Case()
		{
			var eq = new JsonPropertyNameEqualityComparer();
			var s1 = "TestProperty";
			var s2 = "test_property";

			Assert.True(eq.Equals(s1, s2));
		}
	}
}
