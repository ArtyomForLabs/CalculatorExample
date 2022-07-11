using PluginBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace CalculatorExample.BaseOperations
{
    [Export(typeof(ICalculator))]
    public class CalculatorBase : ICalculator
    {
        public IList<IOperation> GetOperations()
        {
            return new List<IOperation>()
            {
                new Operation { Name='+', NumberOfOperands=2},
                new Operation { Name='-', NumberOfOperands=2},
                new Operation { Name='/', NumberOfOperands=2},
                new Operation { Name='*', NumberOfOperands=2}
            };
        }

        public double Operate(IOperation operation, double[] operands)
        {
            double result = operation.Name switch
            {
                '+' => operands[0] + operands[1],
                '-' => operands[0] - operands[1],
                '/' => operands[0] / operands[1],
                '*' => operands[0] * operands[1],
                _ => throw new InvalidOperationException(),
            };

            return result;
        }
    }
}
