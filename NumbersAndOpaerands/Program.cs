using System;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;

namespace Programm;

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
                Console.WriteLine(string.Join(' ', ToRpn(input)));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }

    static List<string> MakeTokenList(string input)
    {
        List<string> tokenList = new List<string>();
        string buff = "";
        foreach (char element in input)
        {
            if (Char.IsDigit(element) || element == ',')
                buff += element;
            else
            {
                if (buff != "")
                    tokenList.Add((buff));
                tokenList.Add(element+"");
                buff = "";
            }
        }
        return tokenList;
    }

    static List<object> ToRpn(string input)
    {
        List<string> tokenList = MakeTokenList(input);
        Stack<string> stack = new Stack<string>();
        List<object> rpnExpresssion = new List<object>();
        foreach (object element in tokenList)
        { 
            if (double.TryParse((string)element, out double result))
            {
                rpnExpresssion.Add(element);
            }
            else if ((stack.Count > 0) && GetPriority(stack.Peek()) > GetPriority((string)element) && stack.Peek() != "(")
            {
                if ((string)element != ")")
                {
                    rpnExpresssion.Add(stack.Pop());
                    stack.Push((string)element);
                }
                else
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        rpnExpresssion.Add(stack.Pop());
                    }
                    if (stack.Count > 0)
                        stack.Pop();
                }
            }
            else
            {
                stack.Push((string)element);
            }
        }
        return rpnExpresssion;
    }

    static double Calculate(string op, double num1, double num2)
    {
        switch (char.Parse(op))
        {
            case '+': return num1 + num2;
            case '-': return num1 - num2;
            case '*': return num1 * num2;
            case '/': return num1 / num2;
            case '^': return Math.Pow(num1, num2);
            default: throw new Exception("Wrong symbol for operator");
        }
    }
    static int GetPriority(string op)
    {
        switch (char.Parse(op))
        {
            case '+': return 0;
            case '-': return 0;
            case '*': return 1;
            case '/': return 1;
            case '(': return 1;
            case ')': return -1;
            default: return -1;

        }
    }
}
