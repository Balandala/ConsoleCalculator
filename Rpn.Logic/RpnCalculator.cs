﻿using Rpn.Logic;
using System.Reflection.Metadata.Ecma335;

namespace Rpn.Logic;
public class RpnCalculator
{
    Variable variable = new Variable(0);
    public void SetVariable(double value)
    {
        variable.SetValue(value);
    }
    public double Calculate(string expression)
    {
        expression = expression.Replace(" ", "").Replace('.', ',').ToLower() + ")";
        double output = CalculateRpn(ToRpn(expression));
        return output;
    }
    private List<Token> MakeTokenList(string input)
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
                else if (element == 'x' || element == 'y')
                    tokenList.Add(variable);
                else
                    tokenList.Add(new Operation(element));
                buff = "";
            }
        }
        if (buff != "")
        tokenList.Add(new Number(double.Parse(buff)));
        return tokenList;
    }
    private List<Token> ToRpn(string input)
    {
        List<Token> tokenList = MakeTokenList(input);
        Stack<Token> stack = new Stack<Token>();
        List<Token> output = new List<Token>();
        foreach (Token element in tokenList)
        {
            if (element is Number || element is Variable)
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
        while (stack.Count > 0)
        {
            output.Add((Operation)stack.Pop());
        }
        return output;
    }
    private double CalculateRpn(List<Token> rpn)
    {
        if (rpn.Count > 0)
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
        else return 0;
    }
    private double Calculate(char op, double num1, double num2)
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