using PluginBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace ProgrammerCalculatorMode
{
    [Export(typeof(ICalculator))]
    public class CalculatorProgrammer : ICalculator
    {
        public IList<IOperation> GetOperations()
        {
            return new List<IOperation>()
            {
                new Operation { Name='%', NumberOfOperands=2},
                new Operation { Name='«', NumberOfOperands=2},
                new Operation { Name='»', NumberOfOperands=2}
            };
        }

        public double Operate(IOperation operation, double[] operands)
        {
            double result = operation.Name switch
            {
                '%' => operands[0] % operands[1],
                '«' => (int)operands[0] << (int)operands[1],
                '»' => (int)operands[0] >> (int)operands[1],
                _ => throw new InvalidOperationException(),
            };

            return result;
        }
    }
}
