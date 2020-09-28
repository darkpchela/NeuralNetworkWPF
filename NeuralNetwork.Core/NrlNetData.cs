using NeuralNetwork.Core.ActivationFuncs;
using NeuralNetwork.Core.Extensions;

namespace NeuralNetwork.Core
{
    public class NrlNetData
    {
        public string Name { get; }
        public string ActivationFuncName { get; }
        public int[] Layers { get; }
        public Matrix2D[] Weights { get; }

        public NrlNetData(string Name, int[] Layers, Matrix2D[] Weiths, IActivationFunc activationFunc)
        {
            this.Name = Name;
            this.Layers = Layers;
            this.Weights = Weiths;
            this.ActivationFuncName = activationFunc.Name;
        }
    }
}