using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace CalculatorExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                 {
                     services.AddScoped<ICalculatorViewModel, CalculatorViewModel>();
                     services.AddScoped<ExpressionBase, ExpressionSimple>();

                     services.AddSingleton<MainWindow>();

                 })
                .ConfigureLogging((hostContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddDebug();
                })
                .Build();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = host.Services.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
