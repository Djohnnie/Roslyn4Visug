using System;
using System.Collections.Generic;

namespace Roslyn.Visug.NewCSharpFeatures.IndexInitializers
{
    class Program
    {
        static void Main(string[] args)
        {
            var dictionary1 = new Dictionary<Int32, String>
            {
                {1, "one" },
                {3, "three" },
                {9, "nine" }
            };

            var dictionary2 = new Dictionary<Int32, String>
            {
                [1] = "one",
                [3] = "three",
                [9] = "nine"
            };

            Console.ReadKey();
        }
    }
}
