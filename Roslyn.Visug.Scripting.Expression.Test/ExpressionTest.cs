using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roslyn.Visug.Scripting.Expression.Core;

namespace Roslyn.Visug.Scripting.Expression.Test
{
    [TestClass]
    public class ExpressionTest
    {
        [TestMethod]
        public void ConstantMathExpressionTest()
        {
            MathExpression x1 = new MathExpression("11");
            Assert.AreEqual(11, x1.Evaluate());
        }

        [TestMethod]
        public void AdditionMathExpressionTest()
        {
            MathExpression x1 = new MathExpression("1 + 2");
            Assert.AreEqual(3, x1.Evaluate());
        }

        [TestMethod]
        public void CombinedMathExpressionTest()
        {
            MathExpression x1 = new MathExpression("1 + 2 * 3 - 4");
            Assert.AreEqual(3, x1.Evaluate());
        }

        [TestMethod]
        public void BracketsMathExpressionTest()
        {
            MathExpression x1 = new MathExpression("1 + 2 * ( 3 - 4 )");
            Assert.AreEqual(-1, x1.Evaluate());
        }
    }
}
