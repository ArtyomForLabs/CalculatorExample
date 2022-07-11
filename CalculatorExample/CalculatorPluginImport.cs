using PluginBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace CalculatorExample
{
    public class CalculatorPluginImport : IPartImportsSatisfiedNotification
    {
        private readonly Action importCompleted;

        public CalculatorPluginImport(Action _importCompleted) => this.importCompleted = _importCompleted;

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<ICalculatorUIExtension> CalculatorUIExtensions { get; set; }

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<ICalculator> CalculatorOperationsExtensions { get; set; }

        public void OnImportsSatisfied() => this.importCompleted();
    }
}
