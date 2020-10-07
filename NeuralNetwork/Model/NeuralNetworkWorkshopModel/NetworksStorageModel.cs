using NeuralNetwork.Core.Default;

namespace NeuralNetwork.Model.NeuralNetworkWorkshopModel
{
    public class NetworksStorageModel : NeuralNetworksDefaultStorage
    {
        public string Name { get; set; }

        public NetworksStorageModel(bool isStrict) : base(isStrict)
        {
        }
    }
}