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
        RpnCalculator calculator = new RpnCalculator(input);
        var ans = calculator.Calculate(double.Parse(Console.ReadLine()));
        Console.WriteLine("Результат: {0}", ans);  
        Console.ReadLine();
    }
}