using NeuralNetwork.Core.Default;

namespace NeuralNetwork.Models
{
    public class NetworksStorageModel : NeuralNetworksDefaultStorage<NetworkModel>
    {
        public string Name { get; set; }

        public NetworksStorageModel(bool isStrict) : base(isStrict)
        {
        }
    }
}