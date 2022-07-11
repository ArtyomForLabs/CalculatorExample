using PluginBase;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CalculatorExample
{
    public interface ICalculatorViewModel
    {
        public IEnumerable<ICalculator> Calculators { get; set; }

        public Button InitOperationButton(IOperation _operation);
    }
}
