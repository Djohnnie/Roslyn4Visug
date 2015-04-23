using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.CSharp;
using Roslyn.Visug.Scripting.Expression.Wcf.Contract.Data;

namespace Roslyn.Visug.Scripting.Expression.Wcf.Host
{
    public class ExpressionEngine
    {
        private Func<Decimal[], Decimal> _evaluate;
        public MathExpression Expression { get; set; }

        public ExpressionEngine(MathExpression expression)
        {
            String parameters = String.Format("Decimal[] values");
            Expression = expression;
            String script = String.Format("Decimal Evaluate({0}) => {1};", parameters, expression.Expression.Replace("x", "values[0]"));
            ScriptState scriptState = CSharpScript.Run(script);
            _evaluate = scriptState.CreateDelegate<Func<Decimal[], Decimal>>("Evaluate");
        }

        public List<VariableResult> Evaluate()
        {
            if (_evaluate != null)
            {
                var results = new List<VariableResult>();
                foreach (var variable in Expression.Variables)
                {
                    var result = new VariableResult
                    {
                        Variable = variable,
                        Results = new Dictionary<Decimal, Decimal>()
                    };
                    foreach (var value in GetRange(variable))
                    {
                        result.Results.Add(value, _evaluate(new[] { value }));
                    }
                    results.Add(result);
                }
                return results;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public IEnumerable<Decimal> GetRange(Variable variable)
        {
            for (var value = variable.LowerBound; value <= variable.UpperBound; value += variable.Increment)
            {
                yield return value;
            }
        }
    }
}