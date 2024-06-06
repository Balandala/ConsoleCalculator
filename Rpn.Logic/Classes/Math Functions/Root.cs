using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
    internal class Root : Operation
    {
        public override string Name => "rt";
        public override int Prio => 2;
        public override int ArgumentsNumber => 2;
        public override bool IsFunction => true;

        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];
            var num2 = numbers[1];
            return new Number(Math.Pow(num2.Value, 1/ num1.Value));
        }
    }
}
