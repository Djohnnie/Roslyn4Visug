using System;

namespace Roslyn.Visug.NewCSharpFeatures.StringInterpolation {

	public class Person {

		public String Name { get; set; } = "Janssens";

		public String FirstName { get; set; } = "Jan";

		public Byte Age { get; set; } = 38;

		public override string ToString() => $"{Name} {FirstName} ({Age} year{(Age != 1 ? "s" : "")} old)";
    }

}