using NeuralNetwork.Core.Default;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.Models
{
    public class NetworksStorageModel : NeuralNetworksDefaultStorage<NetworkModel> , INotifyPropertyChanged
    {
        public string Name { get; set; }

        public NetworksStorageModel(bool isStrict) : base(isStrict)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}