using static System.Console;
using static System.Convert;
using static System.Math;
using static System.Linq.Enumerable;

namespace Roslyn.Visug.NewCSharpFeatures.UsingStatic {

	class Program {

		static void Main(string[] args) {

			Write("Enter a value for x: ");
			var x = ToDouble(ReadLine());
			Write("Enter a value for y: ");
			var y = ToDouble(ReadLine());
			var h = Sqrt(Pow(x, 2) + Pow(y, 2));
			WriteLine("The result of the calculation sqrt(x^2+y^2): {0:F2}", h);

			var values = Range(1, 1000000);
			var even = values.Where(v => v % 2 == 0);

			ReadKey();
		}

	}

}
