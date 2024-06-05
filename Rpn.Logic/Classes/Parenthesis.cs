using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
    class Parenthesis : Token
    {
        public bool IsOpened;
        public Parenthesis(char bracket)
        {
            switch (bracket)
            {
                case '(': IsOpened = true; break;
                case ')': IsOpened = false; break;
            }
        }
    }
}
