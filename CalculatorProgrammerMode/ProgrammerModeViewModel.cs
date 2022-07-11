using Microsoft.Extensions.Logging;
using PluginBase;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalculatorProgrammerMode
{
    public class ProgrammerModeViewModel : IProgrammerModeViewModel, INotifyPropertyChanged
    {
        private readonly ILogger<ProgrammerMode> logger;

        public ProgrammerModeViewModel(ILogger<ProgrammerMode> _logger)
        {
            logger = _logger;
        }

        public string BinNumber { get; set; }

        public string InputNumbers { get; set; }

        private Command binCommand;
        public Command BinCommand =>
            binCommand ??= new Command(obj =>
            {
                logger.LogInformation("BinCommand");

                InputNumbers = NumberConverter.ToBin(InputNumbers);

                OnPropertyChanged("InputNumbers");
            });

        private Command hexCommand;
        public Command HexCommand =>
            hexCommand ??= new Command(obj =>
            {
                logger.LogInformation("HexCommand");

                InputNumbers = NumberConverter.ToHex(InputNumbers);

                OnPropertyChanged("InputNumbers");
            });

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
