using NeuralNetwork.Infrastructure.Commands;
using System;
using System.Windows.Input;

namespace NeuralNetwork.UI
{
    public abstract class Tab : ITab
    {
        public string Name { get; set; }

        public ICommand CloseCommand { get; }

        public event EventHandler CloseRequested;

        public Tab()
        {
            CloseCommand = new RelayCommand(obj => CloseRequested?.Invoke(this, EventArgs.Empty));
        }
    }
}