using NeuralNetwork.Core;

namespace NeuralNetwork.Models
{
    public class NetworkFactoryModel : NeuralNetworkAbstractFactory<NetworkModel, NetworkDataModel>
    {
        public override NetworkModel CreateInstance(NetworkDataModel nNetData)
        {
            return new NetworkModel(nNetData);
        }
    }
}