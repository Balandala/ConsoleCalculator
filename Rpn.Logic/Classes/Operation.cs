using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
    class Operation : Token
    {
        public char Op;
        public int Prio;
        public Operation(char op)
        {
            Op = op;
            switch (op)
            {
                case '+': Prio = 0; break;
                case '-': Prio = 0; break;
                case '*': Prio = 1; break;
                case '/': Prio = 1; break;
                case '^': Prio = 2; break;
                default: throw new ArgumentException("Invalid simbol");
            }
        }
    }
}
