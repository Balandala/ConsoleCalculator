using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
    internal class Sin : Operation
    {
        public override string Name => "sin";
        public override int Prio => 2;
        public override int ArgumentsNumber => 1;
        public override Number Execute(params Number[] numbers)
        {
            var num = numbers[0];
            return new Number(Math.Sin(num.Value));
        }
    }
}
