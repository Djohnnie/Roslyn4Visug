using System;

namespace Roslyn.Visug.NewCSharpFeatures.NullConditional {

	class Program {

		static void Main(string[] args) {

			var p = new Person
			{
				Name = "Janssens",
				FirstName = "Jan",
				Age = 38,
				Mother = new Person
				{
					FirstName = "Eve",
					Age = 56
				},
				Father = new Person
				{
					FirstName = "Adam",
					Age = 56
				}
			};
			Console.WriteLine(p);
			Console.WriteLine(p.Mother);
			Console.WriteLine(p.Father);
			Console.ReadKey();
		}

	}

}
