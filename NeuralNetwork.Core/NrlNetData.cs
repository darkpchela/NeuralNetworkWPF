namespace NeuralNetwork.Core
{
    public class NrlNetData
    {
        public string ActivationFuncName { get; }
        public int[] Layers { get; }
        public Matrix2D[] Weights { get; }

        public NrlNetData(int[] Layers, Matrix2D[] Weiths, string activationFuncName)
        {
            this.Layers = Layers;
            this.Weights = Weiths;
            this.ActivationFuncName = activationFuncName;
        }
    }
}