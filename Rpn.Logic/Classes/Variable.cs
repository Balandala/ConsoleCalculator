using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
    class Variable : Number
    {
        public Variable(double value) : base(value)
        {
        }
        public void SetValue(double x)
        {
            Value = x;
        }
    }
}
