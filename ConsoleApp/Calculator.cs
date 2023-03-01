using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Calculator
    {
        public static string Calculate(string line)
        {
            line = UnarMinus(line);
            char[] exp = line.ToCharArray();
            try
            {
                return Convert.ToString(Expression(exp));
            }
            catch (DivideByZeroException)
            {
                return ("Division by zero!");
            }
            catch (OverflowException)
            {
                return ("It's too big number!");
            }
            catch (Exception)
            {
                return ("Check if the expression is correct!");
            }
        }

        public static string UnarMinus(string operators)
        {
            if (operators[0] == '-' || operators[0] == '+')
            {
                operators = operators.Insert(0, "0");
            }
            for (int i = 1; i < operators.Length; i++)
            {
                if (((operators[i - 1] == '(') || (operators[i - 1] == '+') || (operators[i] == '+') || (operators[i - 1] == '-')) && ((operators[i] == '-')))
                {
                    operators = operators.Insert(i, "0");
                }
            }
            return operators;
        }

        static double Expression(char[] exp)
        {
            Stack<double> numberStack = new Stack<double>();
            Stack<char> operStack = new Stack<char>();

            operStack.Push('(');

            int pos = 0;
            while (pos <= exp.Length)
            {
                if (pos == exp.Length || exp[pos] == ')')
                {
                    ProcessClosingParenthesis(numberStack, operStack);
                    pos++;
                }
                else if (exp[pos] >= '0' && exp[pos] <= '9')
                {
                    pos = ProcessInputNumber(exp, pos, numberStack);
                }
                else
                {
                    ProcessInputOperator(exp[pos], numberStack, operStack);
                    pos++;
                }
            }
            return numberStack.Pop();
        }

        static void ProcessClosingParenthesis(Stack<double> numberStack, Stack<char> operStack)
        {
            while (operStack.Peek() != '(')
                ExecuteOperation(numberStack, operStack);

            operStack.Pop();
        }

        static int ProcessInputNumber(char[] exp, int pos, Stack<double> numberStack)
        {
            int value = 0;
            while (pos < exp.Length && exp[pos] >= '0' && exp[pos] <= '9')
                value = checked(10 * value + (int)(exp[pos++] - '0'));
            numberStack.Push(value);
            return pos;
        }

        static void ProcessInputOperator(char op, Stack<double> numberStack, Stack<char> operStack)
        {
            while (operStack.Count > 0 &&
            OperatorCausesEvaluation(op, operStack.Peek()))
                ExecuteOperation(numberStack, operStack);
            operStack.Push(op);
        }

        static bool OperatorCausesEvaluation(char op, char prevOp)
        {
            bool evaluate = false;
            switch (op)
            {
                case '+':
                case '-':
                    evaluate = (prevOp != '(');
                    break;
                case '*':
                case '/':
                    evaluate = (prevOp == '*' || prevOp == '/');
                    break;
                case ')':
                    evaluate = true;
                    break;
            }
            return evaluate;
        }

        static void ExecuteOperation(Stack<double> numberStack, Stack<char> operStack)
        {
            double rightOperand = numberStack.Pop();
            double leftOperand = numberStack.Pop();
            char op = operStack.Pop();
            double result = 0;
            switch (op)
            {
                case '+':
                    result = leftOperand + rightOperand;
                    break;
                case '-':
                    result = leftOperand - rightOperand;
                    break;
                case '*':
                    result = leftOperand * rightOperand;
                    break;
                case '/':
                    result = leftOperand / rightOperand;
                    break;
            }
            numberStack.Push(result);
        }


    }
}
