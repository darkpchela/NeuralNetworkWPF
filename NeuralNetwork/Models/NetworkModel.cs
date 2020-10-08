using NeuralNetwork.Core.Default;

namespace NeuralNetwork.Models
{
    public class NetworkModel : NeuralNetworkDefault
    {
        public string Name { get; set; }

        public NetworkModel(NetworkDataModel networkDataModel) : base(networkDataModel)
        {
            Name = networkDataModel.Name;
        }
    }
}