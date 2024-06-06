using Rpn.Logic;
using System.Reflection.Metadata.Ecma335;
using System.Xml;

namespace Rpn.Logic;
public class RpnCalculator
{
    string Expression;
    Variable variable = new Variable(0);
    public double Calculate(double value)
    {
        var rpn = ToRpn(Expression);
        variable.Value = value;
        double output = CalculateRpn(rpn);
        return output;
    }
    public RpnCalculator(string expression)
    {
        Expression = expression.Replace(" ", "").ToLower() + ")";
    }
    private List<Token> MakeTokenList(string input)
    {
        List<Operation> availableOpetations = new List<Operation>
        {
            new Plus(), new Minus(), new Multiply(), new Division(), new Root()
        };
        List<Token> tokenList = new List<Token>();
        bool isBuildingOperation;
        string numBuff = "";
        string opBuff = "";
        foreach (char element in input)
        {
            isBuildingOperation = false;
            if (Char.IsDigit(element) || element == '.')
            {
                numBuff += element;
            }
            else
            {
                if (numBuff != "")
                {
                    tokenList.Add(new Number(double.Parse(numBuff.Replace(".", ","))));
                    numBuff = "";
                }
                if (element == '(' || element == ')')
                    tokenList.Add(new Parenthesis(element));
                else if (element == 'x')
                    tokenList.Add(variable);
                else if (element == ',')
                    continue;
                else
                {
                    opBuff += element;
                    isBuildingOperation = true;
                }
            }
            if (!isBuildingOperation && opBuff != "")
            {
                tokenList.Add(CreateOperation(opBuff, availableOpetations));
                opBuff = "";
            }
        }
        return tokenList;
    }
    private Operation CreateOperation(string buffer, List<Operation> operations)
    {
        foreach (Operation operation in operations)
        {
            if (operation.Name == buffer)
                return operation;
        }
        throw new Exception("Operation is not found");
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
                    Operation op = (Operation)rpn[i];
                    var args = new Number[op.ArgumentsNumber];
                    for (int j = 0; j < op.ArgumentsNumber; j++)
                    {
                        args[j] = (Number)rpn[i - j-1];
                    }
                    rpn[i] = op.Execute(args);
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
}  
