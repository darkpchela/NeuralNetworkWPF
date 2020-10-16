using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Etc
{
    public delegate void ObservableTaskEventHandler(object sender, ObservableTaskEventArgs eventArgs);

    public class ObservableTaskEventArgs : EventArgs
    {
        public Task Task { get; }

        public ObservableTaskEventArgs(Task task)
        {
            Task = task;
        }
    }
}