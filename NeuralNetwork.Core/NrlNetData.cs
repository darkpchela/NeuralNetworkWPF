using NeuralNetwork.Core.ActivationFuncs;

namespace NeuralNetwork.Core
{
    public class NrlNetData
    {
        public string Name { get; }
        public string ActivationFuncName { get; }
        public int[] Layers { get; set; }
        public float[][,] Weights { get; }

        public NrlNetData(string Name, int[] Layers, float[][,] Weiths, IActivationFunc activationFunc)
        {
            this.Name = Name;
            this.Layers = Layers;
            this.Weights = Weiths;
            this.ActivationFuncName = activationFunc.Name;
        }
    }
}