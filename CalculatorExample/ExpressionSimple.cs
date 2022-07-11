using System;
using System.Linq;
using System.Text;

namespace CalculatorExample
{
    public class ExpressionSimple : ExpressionBase
    {
        private StringBuilder firstOperand = new StringBuilder();
        private StringBuilder secondOperand = new StringBuilder();
        private char operatorCh;

        private bool IsOperatorSupported(char _operator) => Calculators.SelectMany(calc => calc.GetOperations()).Any(op => op.Name == _operator);

        public bool IsOperatorSet => operatorCh != default(char);

        public override void AddSymbol(char _ch)
        {
            if (char.IsDigit(_ch) || System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == _ch.ToString())
            {
                if (IsOperatorSet)
                {
                    secondOperand.Append(_ch);
                }
                else
                {
                    firstOperand.Append(_ch);
                }
            }
            else if (IsOperatorSupported(_ch))
            {
                if (IsOperatorSet)
                {
                    if (secondOperand.Length > 0)
                    {
                        Calculate();
                    }
                    else
                    {
                        base.RemoveLastSymbol();
                    }
                }

                operatorCh = _ch;
            }
            else
            {
                throw new InvalidOperationException();
            }

            base.AddSymbol(_ch);
        }

        public override string Calculate()
        {
            if (IsOperatorSet
                && firstOperand.Length > 0
                && secondOperand.Length > 0
                && double.TryParse(firstOperand.ToString(), out double firstOperandDouble)
                && double.TryParse(secondOperand.ToString(), out double secondOperandDouble))
            {
                string result = string.Empty;

                foreach (var calculator in Calculators)
                {
                    var operation = calculator.GetOperations().FirstOrDefault(op => op.Name == operatorCh);
                    if (operation != null)
                    {
                        result = calculator
                            .Operate(operation, new double[] { firstOperandDouble, secondOperandDouble })
                            .ToString();
                        break;
                    }
                }

                Formula = result;
                operatorCh = default(char);

                firstOperand.Clear();
                firstOperand.Append(result);

                secondOperand.Clear();
            }

            return Formula;
        }

        public override void ClearAll()
        {
            base.ClearAll();

            firstOperand.Clear();
            secondOperand.Clear();
            operatorCh = default(char);
        }

        public override void RemoveLastSymbol()
        {
            if (secondOperand.Length > 0)
            {
                secondOperand.Remove(secondOperand.Length - 1, 1);
            }
            else if (Formula.Length > 0 && Formula[^1] == operatorCh)
            {
                operatorCh = default(char);
            }
            else if (firstOperand.Length > 0)
            {
                firstOperand.Remove(firstOperand.Length - 1, 1);
            }

            base.RemoveLastSymbol();
        }
    }
}
