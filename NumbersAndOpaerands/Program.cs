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
                input += " ";
                //Console.WriteLine(string.Join(' ', ReturnOpPrioTupleList(input)));
                //Console.WriteLine(string.Join(' ', ReturnNumbersList(input)));
                Console.WriteLine(string.Join(' ', ToRpn(input)));
                Console.ForegroundColor = ConsoleColor.Green;
                //Console.WriteLine(CalculateInput(input));
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }

    static List<object> MakeTokenList(string input)
    {
        var tokenList = new List<object>();
        string buff = "";
        foreach (char element in input)
        {
            if (Char.IsDigit(element) || element == ',')
                buff += element;
            else
            {
                tokenList.Add((buff));
                tokenList.Add(element);
                buff = "";
            }
        }
        return tokenList;
    }

    static List<object> ToRpn(string input)
    {
        var tokenList = MakeTokenList(input);
        Stack<string> stack = new Stack<string>();
        var rpnExpresssion = new List<object>();
        var availbleOperators = new List<string>() {"(", "*", "/", "+", "-", ")"};
        foreach (object element in tokenList)
        {
            if (double.TryParse(element.ToString(), out double result))
            {
                rpnExpresssion.Add(result);
            }
            if (availbleOperators.Contains(element.ToString()))
            {
                stack.Push(element.ToString());
                if (element.Equals(")") || element.Equals(" "))
                {
                    while (stack.Peek() != "(" && stack.Count != 0)
                        rpnExpresssion.Add(stack.Pop());
                    stack.Pop();
                }
            }
          
        }
        return rpnExpresssion;
    }

    //static List<(char, int)> ReturnOpPrioTupleList(string input)
    //{
    //    var availbleOperators = new List<char>() {'*', '/', '+', '-', '^'};
    //    var operatorsList = new List<(char, int)>();
    //    char op;
    //    int prio = 0;
    //    foreach (var element in input)
    //    {
    //        if (availbleOperators.Contains(element))
    //        {
    //            op = element;
    //            if ((element == '*') || (element == '/'))
    //                operatorsList.Add((op, prio + 1));
    //            else if (element == '^')
    //                operatorsList.Add((op, prio + 2));
    //            else
    //            operatorsList.Add((op, prio)); 
    //        }
    //        else if (element == '(')
    //            prio += 3;
    //        else if (element == ')')
    //            prio -= 3;
    //    }
    //    return operatorsList;
    //}

    //static List<double> ReturnNumbersList(string input)
    //{
    //    string number = "";
    //    var numbersList = new List<double>();
    //    int index = -1;
    //    char previousNotDigit = ' ';
    //    foreach (var digit in input + " ")
    //    {
    //        if (Char.IsDigit(digit))
    //        {
    //            number += digit;
    //        }
    //        else if (number != "")
    //        {
    //            if (previousNotDigit == '.' || previousNotDigit == ',')
    //                numbersList[index] += Math.Round(double.Parse(number) * Math.Pow(0.1, number.Length), number.Length);
    //            else
    //            {
    //                numbersList.Add(double.Parse((number)));
    //                index++;
    //            }
    //            number = "";
    //            previousNotDigit = digit;      
    //        }
    //        else continue; 
    //    }
    //    return numbersList;
    //}

    //static double CalculateInput(string input)
    //{
    //    var operators = ReturnOpPrioTupleList(input);
    //    var numbers = ReturnNumbersList(input);
    //    if (input[0] == '-')
    //    {
    //        numbers[0] *= -1;
    //        operators.RemoveAt(0);
    //    }
    //    while (numbers.Count > 1)
    //    {
    //        int maxPrio = operators.Max(t => t.Item2);
    //        int index = operators.FindIndex(t => t.Item2 == maxPrio);
    //        numbers[index] = Calculate(operators[index].Item1, numbers[index], numbers[index + 1]); ;
    //        numbers.RemoveAt(index + 1);
    //        operators.RemoveAt(index);
    //    }
    //    return numbers[0];
    //}

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
}
