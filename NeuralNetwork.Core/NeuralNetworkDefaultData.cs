using NeuralNetwork.Core.Etc;
using NeuralNetwork.Core.Extensions;

namespace NeuralNetwork.Core
{
    public class NeuralNetworkDefaultData : NeuralNetworkAbstractData
    {
        public NeuralNetworkDefaultData() { }

        public NeuralNetworkDefaultData(NeuralNetworkDefault nrlNet)
        {
            this.Id = nrlNet.Id;
            this.ActivationFuncName = FuncDictionary.GetFuncName(nrlNet.ActivationFunc) ?? "Unknown function";
            this.Layers = nrlNet.Layers;
            this.Weights = nrlNet.Weigths;
        }
    }
}