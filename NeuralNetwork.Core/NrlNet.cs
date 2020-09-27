using NeuralNetwork.Core.ActivationFuncs;
using NeuralNetwork.Core.Extensions;
using System;

namespace NeuralNetwork.Core
{
    public class NrlNet
    {
        public string Name { get; }
        public IActivationFunc ActivationFunc { get; set; }
        public int[] Layers { get; set; }
        public float[][,] Weights { get; set; }
        public float LearningRate { get; set; }

        public NrlNet(string Name, int[] Layers, IActivationFunc ActivationFunc)
        {
            this.Name = Name;
            this.Layers = Layers;
            this.Weights = new float[Layers.Length][,];
            this.ActivationFunc = ActivationFunc;
            this.LearningRate = 0.25f;
            InitStartWeiths();
        }

        public NrlNet(NrlNetData nrlNetData)
        {
            this.Name = nrlNetData.Name;
            this.Layers = nrlNetData.Layers;
            this.Weights = nrlNetData.Weights;
            if (FuncDictionary.FuncName.TryGetValue(nrlNetData.ActivationFuncName, out IActivationFunc activationFunc))
                this.ActivationFunc = activationFunc;
            else
                this.ActivationFunc = new SigmoidFunc();
        }

        public float[] Query(float[] inputs)
        {
            if (inputs.Length != Layers[0])
                throw new ArithmeticException("Invalid inputs count");

            float[,] inputs_outputs = MathExtensions.MatrixTranspose(inputs);

            for (int i = 0; i < Layers.Length - 1; i++)
            {
                //Upper string make new temp inputs for next layer by multiplying current layer weiths to outputs from previous layer
                //Down string applyes activation func to this inputs, makin from it outputs for next layer
                inputs_outputs = MathExtensions.MatrixMultiply(Weights[i], inputs_outputs);
                MathExtensions.MatrixForEach(ref inputs_outputs, ActivationFunc.ActivationFunc);
            }

            float[] outputs = inputs_outputs.ConvertToSingleArray();

            return outputs;
        }

        protected void InitStartWeiths()
        {
            Random rnd = new Random();
            rnd.Next();
            for (int i = 0; i < Layers.Length - 1; i++)
            {
                int columns = Layers[i];
                int rows = Layers[i + 1];
                float diff = (float)(Math.Pow(Layers[i], -0.5));
                float[,] CurrentLayerWeiths = new float[rows, columns];
                for (int j = 0; j < columns; j++)
                {
                    for (int k = 0; k < rows; k++)
                    {
                        CurrentLayerWeiths[k, j] = (float)rnd.NextDouble(-diff, diff);
                    }
                }
                this.Weights[i] = CurrentLayerWeiths;
            }
        }
    }
}