using System.Windows;

namespace PluginBase
{
    public interface ICalculatorUIExtension
    {
        string Title { get; }

        string Description { get; }

        FrameworkElement GetUI();
    }
}
