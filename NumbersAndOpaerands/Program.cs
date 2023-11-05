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
            string input = Console.ReadLine().Replace(" ", "");
            if (input == "")
                break;
            else
            {
                Console.WriteLine(string.Join(' ', ReturnOpPrioTupleList(input)));
                Console.WriteLine(string.Join(' ', ReturnNumbersList(input)));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(CalculateInput(input));
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }

    static List<(char, int)> ReturnOpPrioTupleList(string input)
    {
        var availbleOperators = new List<char>() {'*', '/', '+', '-', '^'};
        var operatorsList = new List<(char, int)>();
        char op;
        int prio = 0;
        foreach (var element in input)
        {
            if (availbleOperators.Contains(element))
            {
                op = element;
                if ((element == '*') || (element == '/'))
                    operatorsList.Add((op, prio + 1));
                else if (element == '^')
                    operatorsList.Add((op, prio + 2));
                else
                operatorsList.Add((op, prio)); 
            }
            else if (element == '(')
                prio += 3;
            else if (element == ')')
                prio -= 3;
        }
        return operatorsList;
    }

    static List<double> ReturnNumbersList(string input)
    {
        string number = "";
        var numbersList = new List<double>();
        int index = -1;
        char previousNotDigit = ' ';
        foreach (var digit in input + " ")
        {
            if (Char.IsDigit(digit))
            {
                number += digit;
            }
            else if (number != "")
            {
                if (previousNotDigit == '.' || previousNotDigit == ',')
                    numbersList[index] += Math.Round(double.Parse(number) * Math.Pow(0.1, number.Length), number.Length);
                else
                {
                    numbersList.Add(double.Parse((number)));
                    index++;
                }
                number = "";
                previousNotDigit = digit;      
            }
            else continue; 
        }
        return numbersList;
    }

    static double CalculateInput(string input)
    {
        var operators = ReturnOpPrioTupleList(input);
        var numbers = ReturnNumbersList(input);
        if (input[0] == '-')
        {
            numbers[0] *= -1;
            operators.RemoveAt(0);
        }
        while (numbers.Count > 1)
        {
            int maxPrio = operators.Max(t => t.Item2);
            int index = operators.FindIndex(t => t.Item2 == maxPrio);
            numbers[index] = Calculate(operators[index].Item1, numbers[index], numbers[index + 1]); ;
            numbers.RemoveAt(index + 1);
            operators.RemoveAt(index);
        }
        return numbers[0];
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
