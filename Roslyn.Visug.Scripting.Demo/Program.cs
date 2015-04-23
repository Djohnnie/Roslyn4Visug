using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.CSharp;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Roslyn.Visug.Scripting.Demo {

	class Program {

		static void Main(string[] args) {
			var sw = Stopwatch.StartNew();
			var result = CSharpScript.Eval("1 + 2");
			sw.Stop();
			Console.WriteLine($"1. 1+2={result} ({sw.ElapsedMilliseconds}ms)");

			sw = Stopwatch.StartNew();
			var scriptState = CSharpScript.Run("int c = 2 * 3;");
			var value = scriptState.Variables["c"].Value;
			sw.Stop();
			Console.WriteLine($"2. c={value} ({sw.ElapsedMilliseconds}ms)");

			sw = Stopwatch.StartNew();
			ScriptOptions options = ScriptOptions.Default
				.AddReferences(Assembly.GetAssembly(typeof(Stopwatch)))
				.AddNamespaces("System.Diagnostics");
			scriptState = CSharpScript.Run("var sw = Stopwatch.StartNew(); double c = Math.Pow(3, 2); sw.Stop(); var swv = sw.ElapsedTicks;", options);
			value = scriptState.Variables["c"].Value;
			var swv = scriptState.Variables["swv"].Value;
			sw.Stop();
			Console.WriteLine($"3. c={value} (swv={swv}t) ({sw.ElapsedMilliseconds}ms)");

			sw = Stopwatch.StartNew();
			result = CSharpScript.Eval("2 * X * X + 3 * Y + 4", new D { X = 1, Y = 2 });
			sw.Stop();
			Console.WriteLine($"4. 2x²+3y+4={result} ({sw.ElapsedMilliseconds}ms)");

			sw = Stopwatch.StartNew();
			scriptState = CSharpScript.Run("long Factorial( int v ) => v > 1 ? Factorial( v - 1 ) * v : 1;");
			var factorial = scriptState.CreateDelegate<Func<Int32, Int64>>("Factorial");
			sw.Stop();
			Console.WriteLine($"5. 5!={factorial(5)} ({sw.ElapsedMilliseconds}ms)");

			sw = Stopwatch.StartNew();
			options = ScriptOptions.Default
				.AddReferences(Assembly.GetAssembly(typeof(System.Dynamic.DynamicObject)), Assembly.GetAssembly(typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo)), Assembly.GetAssembly(typeof(System.Dynamic.ExpandoObject)))	 // System.Dynamic
				.AddNamespaces("System.Dynamic");
			scriptState = CSharpScript.Run("dynamic dyn = new ExpandoObject(); dyn.Cubed = (Func<int,int>)((value)=>value * value * value);", options);
			dynamic dyn = scriptState.Variables["dyn"].Value;
			sw.Stop();
			Console.WriteLine($"6. dyn={dyn.Cubed(5)} ({sw.ElapsedMilliseconds}ms)");

			Console.ReadKey();
		}

	}

	public class D {
		public int X { get; set; }
		public int Y { get; set; }
	}

}
