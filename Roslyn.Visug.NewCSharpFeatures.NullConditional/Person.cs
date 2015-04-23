using System;

namespace Roslyn.Visug.NewCSharpFeatures.NullConditional {

	public class Person {

		public String Name { get; set; }

		public String FirstName { get; set; }

		public Byte Age { get; set; }

		public String Description => this.FirstName + " " + this.Name + " (" + this.Age+ ")";

		public Person Mother { get; set; }

		public Person Father { get; set; }

		public override string ToString() {
			return this.Description 
				+ Environment.NewLine + " Mother: " + (this.Mother?.Description ?? " no mother") 
				+ Environment.NewLine + " Father: " + (this.Father?.Description ?? " no father");
		}

	}

}
