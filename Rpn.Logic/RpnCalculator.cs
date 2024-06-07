using Rpn.Logic;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Xml.Linq;

namespace Rpn.Logic;
public class RpnCalculator
{
    string _expression;
    Variable _variable = new Variable(0);
    public double Calculate(double value)
    {
        var rpn = ToRpn(_expression);
        _variable.Value = value;
        double output = CalculateRpn(rpn);
        return output;
    }
    public RpnCalculator(string expression)
    {
        _expression = expression;
    }
    private List<Token> ToRpn(string input)
    {
        List<Token> tokenList = TokenCreator.MakeTokenList(input, _variable);
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
                    Operation op = (Operation)rpn[i];
                    var args = new Number[op.ArgumentsNumber];
                    for (int num = 0; num < op.ArgumentsNumber; num++)
                    {
                        args[num] = (Number)rpn[i - num-1];
                    }
                    rpn[i] = op.Execute(args);
                    for (int num = 0; num < op.ArgumentsNumber; num++)
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
    private static class TokenCreator
    {
        static List<Operation> _availableOpetations = new List<Operation>
        {
            new Plus(), new Minus(), new Multiply(), new Division(), new Root(), new Cos(), new Sin(), new Ctg(), new SqRoot(), new Power(), new Log(), new Tg()
        };
        public static List<Token> MakeTokenList(string input, Variable variable)
        {
            input = input.Replace(" ", "").ToLower();
            List<Token> tokenList = new List<Token>();
            string numBuff = "";
            string otherBuff = "";
            foreach (char element in input)
            {
                if (Char.IsDigit(element) || element == '.')
                {
                    if (otherBuff != "")
                    {
                        tokenList = AddTokens(otherBuff, tokenList, variable);
                        otherBuff = "";
                    }
                    numBuff += element;
                }
                else if (element == ',')
                    continue;
                else
                {
                    if (numBuff != "")
                    {
                        tokenList.Add(new Number(double.Parse(numBuff.Replace(".", "."))));
                        numBuff = "";
                    }
                    otherBuff += element;
                }
            }
            if (otherBuff != "")
                tokenList = AddTokens(otherBuff, tokenList, variable);
            if (numBuff != "")
                tokenList.Add(new Number(double.Parse(numBuff.Replace(".", "."))));
            return tokenList;
        }
        private static List<Token> AddTokens(string str, List<Token> tokens, Variable variable)
        {
            string buffer = "";
            foreach (char symbol in str)
            {
                if (symbol == '(' || symbol == ')')
                {
                    if (buffer != "")
                    {
                        tokens = AddOperations(buffer, tokens);
                        buffer = "";
                    }
                    tokens.Add(new Parenthesis(symbol));
                }
                else if (symbol == 'x')
                {
                    if (buffer != "")
                    {
                        tokens = AddOperations(buffer, tokens);
                        buffer = "";
                    }
                    tokens.Add(variable);
                }
                else
                    buffer += symbol;
            }
            if (buffer != "")
            {
                tokens = AddOperations(buffer, tokens);
            }
            return tokens;
        }
        private static List<Token> AddOperations(string str, List<Token> tokens)
        {
            char preveousChar = ' ';
            string buffer = "";
            foreach (char symbol in str)
            {
                foreach (Operation operation in _availableOpetations)
                {
                    if (operation.Name == symbol.ToString())
                    {
                        tokens.Add(operation);
                        buffer = "";
                    }
                    else if (symbol != preveousChar)
                    {
                        buffer += symbol;
                        preveousChar = symbol;
                    }
                }
            }
            if (buffer != "" && buffer != preveousChar.ToString())
            { 
                foreach (Operation operation in _availableOpetations)
                {
                    if (operation.Name == buffer)
                    {
                        tokens.Add(operation);
                        buffer = "";
                    }          
                }
                if (buffer != "")
                    throw new Exception("Operation is not found");
            }
        return tokens;
        }
    }
}  
