using NeuralNetwork.Core.Etc;
using NeuralNetwork.Core.Interfaces;

namespace NeuralNetwork.Core
{
    public class NrlNetData
    {
        public string ActivationFuncName { get; }
        public int[] Layers { get; }
        public Matrix2D[] Weights { get; }

        public NrlNetData(INeuralNetwork nrlNet)
        {
            this.ActivationFuncName = FuncDictionary.GetFuncName(nrlNet.ActivationFunc) ?? "Unknown func";
            this.Layers = nrlNet.Layers;
            this.Weights = nrlNet.Weigths;
        }
    }
}