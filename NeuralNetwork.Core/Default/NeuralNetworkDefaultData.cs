using NeuralNetwork.Core.Etc;

namespace NeuralNetwork.Core.Default
{
    public class NeuralNetworkDefaultData : NeuralNetworkAbstractData
    {
        public NeuralNetworkDefaultData()
        {
        }

        public NeuralNetworkDefaultData(NeuralNetworkDefault nrlNet)
        {
            Id = nrlNet.Id;
            ActivationFuncName = FuncDictionary.GetFuncName(nrlNet.ActivationFunc) ?? "Unknown function";
            Layers = nrlNet.Layers;
            Weights = nrlNet.Weigths;
        }
    }
}