using System;
using System.Windows.Input;

namespace NeuralNetwork.UI
{
    public interface ITab
    {
        string Name { get; set; }
        ICommand CloseCommand { get; }

        event EventHandler CloseRequested;
    }
}