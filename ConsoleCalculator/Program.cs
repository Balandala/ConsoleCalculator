using System;
using System.Data.Common;
using Rpn.Logic;

namespace Rpn.ConsoleApp;
static public class Programm
{
    static void Main(string[] args)
    {
        Console.Write("Введите выражение: ");
        string input = Console.ReadLine();
        Console.Write("\nВведите значение переменной: ");
        RpnCalculator calculator = new RpnCalculator();
        calculator.SetVariable(double.Parse(Console.ReadLine()));
        Console.WriteLine("Результат: " + calculator.Calculate(input));
        Console.ReadLine();
    }
}