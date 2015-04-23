using System;

namespace Roslyn.Visug.NewCSharpFeatures.PropertyInitializers {

	class Program
    {

		static void Main(string[] args)
        {
			var p = new Person();
			Console.WriteLine(p);

			var c = new Car("Toyota", "Prius");
			Console.WriteLine(c.Description);
			Console.ReadKey();
        }

    }

}
