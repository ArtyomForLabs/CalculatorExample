using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows.Controls;

namespace CalculatorProgrammerMode
{
    /// <summary>
    /// Interaction logic for ProgrammerMode.xaml
    /// </summary>
    public partial class ProgrammerMode : UserControl
    {
        public ProgrammerMode(IProgrammerModeViewModel _viewModel)
        {
            InitializeComponent();

            DataContext = _viewModel;
        }

        public static ProgrammerMode Construct()
        {
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IProgrammerModeViewModel, ProgrammerModeViewModel>();

                    services.AddSingleton<ProgrammerMode>();

                })
                .ConfigureLogging((hostContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddDebug();
                })
                .Build();

            return host.Services.GetService<ProgrammerMode>();
        }
    }
}
