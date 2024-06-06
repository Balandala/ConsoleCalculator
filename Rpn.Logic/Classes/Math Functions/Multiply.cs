using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
    internal class Multiply : Operation
    {
        public override string Name => "*";
        public override int Prio => 1;
        public override int ArgumentsNumber => 2;
        public override bool IsFunction => false;

        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];
            var num2 = numbers[1];
            return new Number(num2.Value * num1.Value);
        }
    }
}
