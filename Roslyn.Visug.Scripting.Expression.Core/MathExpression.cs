using System;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.CSharp;

namespace Roslyn.Visug.Scripting.Expression.Core
{
    public class MathExpression
    {
        private Func<Decimal> _evaluate;
        public String Value { get; set; }

        public MathExpression(String value)
        {
            this.Value = value;
            String script = String.Format("Decimal Evaluate() => {0};", value);
            ScriptState scriptState = CSharpScript.Run(script);
            _evaluate = scriptState.CreateDelegate<Func<Decimal>>("Evaluate");
        }

        public Decimal Evaluate()
        {
            if (_evaluate != null)
            {
                return _evaluate();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}