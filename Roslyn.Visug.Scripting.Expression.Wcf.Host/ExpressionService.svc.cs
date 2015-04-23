using System;
using System.Diagnostics;
using Roslyn.Visug.Scripting.Expression.Wcf.Contract.Data;
using Roslyn.Visug.Scripting.Expression.Wcf.Contract.Service;

namespace Roslyn.Visug.Scripting.Expression.Wcf.Host
{

    public class ExpressionService : IExpressionService
    {
        public ExpressionResult Evaluate(MathExpression expression)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var response = new ExpressionResult();
            try
            {
                var expressionEngine = new ExpressionEngine(expression);
                response.Expression = expression;
                response.Results = expressionEngine.Evaluate();
            }
            catch (Exception ex)
            {
                response.Error = ex.ToString();
            }
            sw.Stop();
            response.Duration = TimeSpan.FromTicks(sw.ElapsedTicks);
            return response;
        }
    }

}