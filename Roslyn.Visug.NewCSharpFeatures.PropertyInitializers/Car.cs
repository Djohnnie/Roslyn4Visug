using System;

namespace Roslyn.Visug.NewCSharpFeatures.PropertyInitializers {

	public class Car {

		public String Description { get; } = "Unknown";

		public Car(String make, String type) {
			this.Description = make + " " + type;
		}

	}

}
