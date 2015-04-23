using System;

namespace Roslyn.Visug.NewCSharpFeatures.ExpressionBodies {

	class Program {

		static void Main(string[] args) {

			var p = new Person();
			Console.WriteLine(p.FullName);
			Console.WriteLine(p);
			Console.ReadKey();
		}

	}

}
