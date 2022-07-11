using Microsoft.Extensions.Logging;
using PluginBase;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace CalculatorExample
{
    public class CalculatorViewModel : ICalculatorViewModel, INotifyPropertyChanged
    {
        private readonly ExpressionBase expression;
        private readonly ILogger<CalculatorViewModel> logger;

        public CalculatorViewModel(ExpressionBase _expression, ILogger<CalculatorViewModel> _logger)
        {
            expression = _expression;
            logger = _logger;
        }

        public string Formula
        {
            get => expression.Formula;
        }

        public IEnumerable<ICalculator> Calculators
        {
            get => expression.Calculators;

            set
            {
                expression.Calculators = value;
            }
        }

        private Command calculateCommand;
        public Command CalculateCommand =>
            calculateCommand ??= new Command(obj =>
            {
                logger.LogInformation("CalculateCommand");

                expression.Calculate();

                OnPropertyChanged("Formula");
            });

        private Command addSymbolCommand;
        public Command AddSymbolCommand =>
            addSymbolCommand ??= new Command(obj =>
            {
                if (char.TryParse(obj as string, out char symbolToAdd))
                {
                    logger.LogInformation($"AddSymbolCommand('{symbolToAdd}')");

                    expression.AddSymbol(symbolToAdd);

                    OnPropertyChanged("Formula");
                }
            });

        private Command clearAllCommand;
        public Command ClearAllCommand =>
            clearAllCommand ??= new Command(obj =>
            {
                logger.LogInformation("ClearAllCommand");

                expression.ClearAll();

                OnPropertyChanged("Formula");
            });

        private Command clearLastCommand;
        public Command ClearLastCommand =>
            clearLastCommand ??= new Command(obj =>
            {
                logger.LogInformation("ClearLastCommand");

                expression.RemoveLastSymbol();

                OnPropertyChanged("Formula");
            });

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public Button InitOperationButton(IOperation _operation)
        {
            var b = new Button();
            b.Width = 30;
            b.Height = 30;
            b.Content = _operation.Name.ToString();
            b.Tag = _operation;
            b.Command = this.AddSymbolCommand;
            b.CommandParameter = _operation.Name.ToString();

            return b;
        }
    }
}
