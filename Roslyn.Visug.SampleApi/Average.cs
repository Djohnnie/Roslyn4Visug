using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roslyn.Visug.SampleApi
{
    public static class Average
    {

        [Obsolete]
        public static Decimal Calculate(Decimal number1, Decimal number2)
        {
            return (number1 + number2) / 2;
        }

        [Obsolete]
        public static Decimal Calculate(Decimal number1, Decimal number2, Decimal number3)
        {
            return (number1 + number2 + number3) / 3;
        }

        public static Decimal Calculate(Decimal[] numbers)
        {
            return numbers.Average();
        }

    }
}
