using PluginBase;
using System.ComponentModel.Composition;
using System.Windows;

namespace CalculatorProgrammerMode
{
    [Export(typeof(ICalculatorUIExtension))]
    public class ProgrammerModeExtension : ICalculatorUIExtension
    {
        public string Title
        {
            get { return "Programmer mode"; }
        }

        public string Description
        {
            get { return "Additional operations for programmer."; }
        }

        public FrameworkElement GetUI()
        {
            return ProgrammerMode.Construct();
        }
    }
}
