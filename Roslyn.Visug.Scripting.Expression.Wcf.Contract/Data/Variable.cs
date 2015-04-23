using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Roslyn.Visug.Scripting.Expression.Wcf.Contract.Data
{
    [DataContract]
    public class Variable
    {
        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public Decimal LowerBound { get; set; }

        [DataMember]
        public Decimal UpperBound { get; set; }

        [DataMember]
        public Decimal Increment { get; set; }
    }
}