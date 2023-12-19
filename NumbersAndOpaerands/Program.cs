using System;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;

namespace Programm;

class Token : Object
{
}
class Number : Token
{
   public double Value;
    public Number(double value)
    {
        Value = value;
    }
}
class Operation : Token
{
    public char Op;
    public int Prio;
    public Operation(char op)
    {
        Op = op;
        switch(op)
        {
            case '+': Prio = 0; break;
            case '-': Prio = 0; break;
            case '*': Prio = 1; break;
            case '/': Prio = 1; break;
            case '^': Prio = 2; break;
            default: throw new ArgumentException("Wrong simbol for operator");
        }
    }
}
class Parenthesis : Token
{
    public bool IsOpened;
    public Parenthesis(char bracket)
    {
        switch(bracket)
        {
            case '(': IsOpened = true; break;
            case ')': IsOpened = false; break;
        }
    }
}

static public class Programm
{
    static void Main(string[] args)
    {
        while (true)
        {
            string input = Console.ReadLine().Replace(" ", "").Replace('.', ',');
            if (input == "")
                break;
            else
            {
                input += ")";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(CalculateRpn(ToRpn(input)));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }
    }

    static List<Token> MakeTokenList(string input)
    {
        List<Token> tokenList = new List<Token>();
        string buff = "";
        foreach (char element in input)
        {
            if (Char.IsDigit(element) || element == ',')
            {
                buff += element;
            }
            else
            {
                if (buff != "")
                    tokenList.Add(new Number(double.Parse(buff)));
                if (element == '(' || element == ')')
                    tokenList.Add(new Parenthesis(element));
                else
                    tokenList.Add(new Operation(element));
                buff = "";
            }
        }
        return tokenList;
    }

    static List<Token> ToRpn(string input)
    {
        List<Token> tokenList = MakeTokenList(input);
        Stack<Token> stack = new Stack<Token>();
        List<Token> output = new List<Token>();
        foreach (Token element in tokenList)
        {
            if (element is Number)
            {
                output.Add(element);
            }
            else if (element is Operation)
            {
                Operation opElement = (Operation)element;
                while (stack.Count > 0 && stack.Peek() is Operation)
                {
                    Operation opStack = (Operation)stack.Peek();
                    if (opStack.Prio >= opElement.Prio)
                    {
                        output.Add(stack.Pop());
                    }
                    else
                    {
                        break;
                    }
                }
                stack.Push(element);
            }
            else if (element is Parenthesis)
            {
                Parenthesis bracket = (Parenthesis)element;
                if (bracket.IsOpened)
                {
                    stack.Push(bracket);
                }
                else
                {
                    while (stack.Count > 0 && !(stack.Peek() is Parenthesis))
                    {
                        output.Add(stack.Pop());
                    }
                    if (stack.Count > 0)
                        stack.Pop();
                }
            }

        }
        return output;
    }
    static double CalculateRpn(List<Token> rpn)
    {
        for (int i = 0; i < rpn.Count;)
        {
            if (rpn[i] is Operation)
            {
                Number n1 = (Number)rpn[i - 2];
                Number n2 = (Number)rpn[i - 1];
                Operation op = (Operation)rpn[i];
                rpn[i] = new Number(Calculate(op.Op, n1.Value, n2.Value));
                for (int j = 0; j < 2; j++)
                {
                    rpn.Remove(rpn[i - 1]);
                    i--;
                }
            }
            else i++;
        }
        Number ans = (Number)rpn[0];
        return ans.Value;
    }
    static double Calculate(char op, double num1, double num2)
    {
        switch (op)
        {
            case '+': return num1 + num2;
            case '-': return num1 - num2;
            case '*': return num1 * num2;
            case '/': return num1 / num2;
            case '^': return Math.Pow(num1, num2);
            default: throw new Exception("Wrong symbol for operator");
        }
    }
}