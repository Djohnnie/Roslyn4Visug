using System.ServiceModel;
using Roslyn.Visug.Scripting.Expression.Wcf.Contract.Data;

namespace Roslyn.Visug.Scripting.Expression.Wcf.Contract.Service
{

    [ServiceContract]
    public interface IExpressionService
    {

        [OperationContract]
        ExpressionResult Evaluate(MathExpression expression);

    }

}