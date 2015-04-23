using System;

namespace Roslyn.Visug.NewCSharpFeatures.PropertyInitializers {

	public class Person {

		public String Name { get; set; } = "Janssens";

		public String FirstName { get; set; } = "Jan";

		public Byte Age { get; set; } = 38;

		public override string ToString() {
			return this.Name + " " + this.FirstName + " (" + this.Age + ")";
		}

	}

}