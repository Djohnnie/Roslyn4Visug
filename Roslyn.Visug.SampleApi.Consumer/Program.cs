using System;

namespace Roslyn.Visug.SampleApi.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {

            Decimal result1 = Average.Calculate(1, 2);
            Decimal result2 = Average.Calculate(1, 2, 3);

            Console.WriteLine("test", 3);

        }
    }
}
