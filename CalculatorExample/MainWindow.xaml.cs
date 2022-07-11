using Microsoft.Extensions.Logging;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger<CalculatorViewModel> logger;
        private CalculatorPluginImport extensions;

        public MainWindow(ICalculatorViewModel _calcViewModel, ILogger<CalculatorViewModel> _logger)
        {
            logger = _logger;

            InitializeComponent();

            LoadExensions();

            InitializeButtonOperations(_calcViewModel);
            InitializeTabExtensions();

            _calcViewModel.Calculators = extensions?.CalculatorOperationsExtensions;

            DataContext = _calcViewModel;
        }

        private void LoadExensions()
        {
            var extPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(typeof(CalculatorViewModel).Assembly.Location), "Ext");
            if (Directory.Exists(extPath))
            {
                var catalog = new DirectoryCatalog(extPath);
                var container = new CompositionContainer(catalog);

                extensions = new CalculatorPluginImport(() => logger.LogInformation("Extensions have been loaded."));

                container.ComposeParts(extensions);
            }
        }

        private void InitializeButtonOperations(ICalculatorViewModel _calcViewModel)
        {
            if (extensions != null)
            {
                var operators = extensions.CalculatorOperationsExtensions.SelectMany(calc => calc.GetOperations());

                foreach (var op in operators)
                {
                    var b = _calcViewModel.InitOperationButton(op);
                    OperationExtensions.Items.Add(b);
                }
            }
        }

        private void InitializeTabExtensions()
        {
            if (extensions != null)
            {
                foreach (var ext in extensions.CalculatorUIExtensions)
                {
                    FrameworkElement uiControl = ext.GetUI();
                    var headerPanel = new StackPanel { Orientation = Orientation.Horizontal };
                    headerPanel.Children.Add(new Label { Content = ext.Title, ToolTip = ext.Description });
                    var ti = new TabItem { Header = headerPanel, Content = uiControl };

                    ExtensionsTab.Items.Add(ti);
                }
            }
        }
    }
}
