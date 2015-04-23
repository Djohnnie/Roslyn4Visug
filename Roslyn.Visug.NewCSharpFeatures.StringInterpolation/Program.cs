using System;

namespace Roslyn.Visug.NewCSharpFeatures.StringInterpolation {

	class Program {

		static void Main(string[] args) {

			Console.WriteLine(new Person());
			Console.WriteLine(new Person { Age = 1 });
			Console.ReadKey();

		}

	}

}