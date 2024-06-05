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
            RpnCalculator calculator = new RpnCalculator();
            Console.WriteLine("Результат: " + calculator.Calculate(input));
            Console.ReadLine();
    }
}