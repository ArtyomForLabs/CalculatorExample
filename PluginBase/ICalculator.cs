using System.Collections.Generic;

namespace PluginBase
{
    public interface ICalculator
    {
        IList<IOperation> GetOperations();

        double Operate(IOperation operation, double[] operands);
    }
}
