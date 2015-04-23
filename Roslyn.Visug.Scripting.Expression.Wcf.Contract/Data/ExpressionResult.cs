using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Roslyn.Visug.Scripting.Expression.Wcf.Contract.Data
{

    [DataContract]
    public class ExpressionResult
    {

        [DataMember]
        public MathExpression Expression { get; set; }

        [DataMember]
        public TimeSpan Duration { get; set; }

        [DataMember]
        public List<VariableResult> Results { get; set; }

        [DataMember]
        public String Error { get; set; }

    }

}