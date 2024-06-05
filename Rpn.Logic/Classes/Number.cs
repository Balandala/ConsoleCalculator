using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
    class Number : Token
    {
        public double Value;

        public Number(double value)
        {
            Value = value;
        }
    }
}
