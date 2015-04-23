using System;
using System.Collections.Generic;
using System.ServiceModel;
using Roslyn.Visug.Scripting.Expression.Wcf.Contract.Data;
using Roslyn.Visug.Scripting.Expression.Wcf.Contract.Service;

namespace Roslyn.Visug.Scripting.Expression.Wcf.Client
{
    class Program
    {
        private const String AzureEndpoint = "http://roslynscripting2.azurewebsites.net/ExpressionService.svc";
        private const String LocalEndpoint = "http://localhost:1293/ExpressionService.svc";

        static void Main(string[] args)
        {
            var binding = new BasicHttpBinding { MaxReceivedMessageSize = Int32.MaxValue };
            var endpoint = new EndpointAddress(AzureEndpoint);
            var client = ChannelFactory<IExpressionService>.CreateChannel(binding, endpoint);
            var request = new MathExpression
            {
                Expression = "2*x*x+3*x-4",
                Variables = new List<Variable>(new[]
                    {
                        new Variable {Name = "x", LowerBound = -100M, UpperBound = 100M, Increment = .01M}
                    })
            };
            var response = client?.Evaluate(request);
            Console.WriteLine(response?.Expression);
            Console.ReadKey();
        }
    }
}
