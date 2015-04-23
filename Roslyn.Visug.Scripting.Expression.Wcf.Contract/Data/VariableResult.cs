using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Roslyn.Visug.Scripting.Expression.Wcf.Contract.Data
{
    [DataContract]
    public class VariableResult
    {

        [DataMember]
        public Variable Variable { get; set; }

        [DataMember]
        public Dictionary<Decimal, Decimal> Results { get; set; }

    }
}