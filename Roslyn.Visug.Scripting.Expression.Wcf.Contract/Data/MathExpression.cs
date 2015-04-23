using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Roslyn.Visug.Scripting.Expression.Wcf.Contract.Data
{

    [DataContract]
    public class MathExpression
    {

        [DataMember]
        public String Expression { get; set; }

        [DataMember]
        public List<Variable> Variables { get; set; } 

    }

}