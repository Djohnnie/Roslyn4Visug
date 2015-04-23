using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roslyn.Visug.NewCSharpFeatures.ExpressionBodies {

	public class Person {

		public String Name { get; set; } = "Janssens";

		public String FirstName { get; set; } = "Jan";

		public Byte Age { get; set; } = 38;

		public String FullName => FirstName + " " + Name;

		public override string ToString() => FullName + " (" + Age + ")";

	}

}
