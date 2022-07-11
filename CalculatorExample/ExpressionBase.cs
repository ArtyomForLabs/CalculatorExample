using CalculatorExample.BaseOperations;
using PluginBase;
using System.Collections.Generic;
using System.Text;

namespace CalculatorExample
{
    public abstract class ExpressionBase
    {
        private readonly StringBuilder formula;
        private readonly IEnumerable<ICalculator> calculators;

        public ExpressionBase()
        {
            formula = new StringBuilder();
            calculators = new List<ICalculator>()
            {
                new CalculatorBase()
            };
        }

        public IEnumerable<ICalculator> Calculators
        {
            get => calculators;

            set
            {
                if (value != null)
                {
                    (calculators as List<ICalculator>).AddRange(value);
                }
            }
        }

        public string Formula
        {
            get => formula.ToString();
            set
            {
                formula.Clear();
                formula.Append(value);
            }
        }

        public virtual void AddSymbol(char _ch) => formula.Append(_ch);

        public virtual void RemoveLastSymbol()
        {
            if (formula.Length > 0)
            {
                formula.Remove(formula.Length - 1, 1);
            }
        }

        public virtual void ClearAll()
        {
            formula.Clear();
        }

        public abstract string Calculate();
    }
}
