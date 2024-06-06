using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
    abstract class Operation : Token
    {
        public abstract string Name { get; }
        public abstract int Prio { get; }
        public abstract int ArgumentsNumber { get; }
        public abstract bool IsFunction { get; }
        public abstract Number Execute(params Number[] numbers);
    }
}
