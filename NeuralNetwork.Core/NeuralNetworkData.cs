using NeuralNetwork.Core.Etc;
using NeuralNetwork.Core.Interfaces;

namespace NeuralNetwork.Core
{
    public class NeuralNetworkData
    {
        public string ActivationFuncName { get; }
        public int[] Layers { get; }
        public Matrix2D[] Weights { get; }

        public NeuralNetworkData(INeuralNetwork nrlNet)
        {
            this.ActivationFuncName = FuncDictionary.GetFuncName(nrlNet.ActivationFunc) ?? "Unknown function";
            this.Layers = nrlNet.Layers;
            this.Weights = nrlNet.Weigths;
        }
    }
}