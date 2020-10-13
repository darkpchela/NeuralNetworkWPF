using NeuralNetwork.Core.Etc;
using NeuralNetwork.Core.Extensions;
using NeuralNetwork.Core.Structs;
using System;

namespace NeuralNetwork.Core.Default
{
    public class NeuralNetworkDefault : NeuralNetworkAbstract
    {
        private Matrix2D[] _QueryHiddenOutputs;

        public NeuralNetworkDefault(int[] layers, Func<float, float> activationFunc, float learningRate = 0.05f)
        {
            this.Id = Guid.NewGuid();
            this.Layers = layers;
            this.ActivationFunc = activationFunc;
            this.LearningRate = learningRate;
            this.Weigths = GetDefaultWeigths();
            this._QueryHiddenOutputs = new Matrix2D[Layers.Length];
        }

        public NeuralNetworkDefault(Matrix2D[] weights, Func<float, float> activationFunc, float learningRate = 0.05f)
        {
            this.Id = Guid.NewGuid();
            this.ActivationFunc = activationFunc;
            this.Weigths = weights;
            this.LearningRate = learningRate;
            this.Layers = new int[Weigths.Length + 1];

            for (int i = 0; i < Weigths.Length;)
            {
                Layers[i] = Weigths[i].Columns;
                Layers[i + 1] = Weigths[i].Rows;

                if ((i + 2) > weights.Length - 1)
                    i++;
                else
                    i += 2;
            }
        }

        public NeuralNetworkDefault(Matrix2D[] weigths, float learningRate = 0.05f) : this(weigths, MathFuncs.Sigmoid, learningRate)
        {
        }

        public NeuralNetworkDefault(NeuralNetworkDefaultData nrlNetData)
        {
            Id = nrlNetData.Id ?? Guid.NewGuid();
            Layers = nrlNetData.Layers;
            Weigths = nrlNetData.Weights ?? GetDefaultWeigths();
            LearningRate = nrlNetData.LearningRate;

            if (!string.IsNullOrEmpty(nrlNetData.ActivationFuncName) && FuncDictionary.TryGetFunc(nrlNetData.ActivationFuncName, out Func<float, float> activationFunc))
                ActivationFunc = activationFunc;
            else
                ActivationFunc = MathFuncs.Sigmoid;
        }

        public override void Train(float[] inputValues, float[] targetValues)
        {
            var targetMatrix = inputValues.ToMatrix2D().Transpose();
            var outputMatrix = Query(inputValues).ToMatrix2D().Transpose();
            var errorMatrix = targetMatrix - outputMatrix;

            for (int i = Layers.Length - 1; i > 0; i--)
            {
                var currentOutputMatrix = _QueryHiddenOutputs[i];
                var previousOutputMatrix = _QueryHiddenOutputs[i - 1];
                var deltaWeigthMatrix = LearningRate * Matrix2D.ScalerProduct(errorMatrix * currentOutputMatrix * (1.0f - currentOutputMatrix), previousOutputMatrix.Transpose());
                Weigths[i] += deltaWeigthMatrix;
                errorMatrix = Matrix2D.ScalerProduct(Weigths[i].Transpose(), errorMatrix);
            }
        }

        public override float[] Query(float[] inputValues)
        {
            if (inputValues.Length != Layers[0])
                throw new ArithmeticException("Invalid inputs count");

            Matrix2D inputs_outputs = inputValues.ToMatrix2D().Transpose();

            _QueryHiddenOutputs[0] = inputs_outputs;

            for (int i = 0; i < Layers.Length - 1; i++)
            {
                inputs_outputs = Matrix2D.ScalerProduct(Weigths[i], inputs_outputs);
                inputs_outputs = inputs_outputs.ForEach(ActivationFunc);
                _QueryHiddenOutputs[i + 1] = inputs_outputs;
            }

            float[] outputs = inputs_outputs.ToSingleArray();

            return outputs;
        }

        protected override Matrix2D[] GetDefaultWeigths()
        {
            Matrix2D[] weigths = new Matrix2D[Layers.Length - 1];

            Random rnd = new Random();

            for (int i = 0; i < Layers.Length - 1; i++)
            {
                int columns = Layers[i];
                int rows = Layers[i + 1];
                float diff = (float)(Math.Pow(Layers[i], -0.5));
                Matrix2D CurrentLayerWeiths = new Matrix2D(rows, columns);
                for (int j = 0; j < columns; j++)
                {
                    for (int k = 0; k < rows; k++)
                    {
                        CurrentLayerWeiths[k, j] = (float)rnd.NextDouble(-diff, diff);
                    }
                }

                weigths[i] = CurrentLayerWeiths;
            }

            return weigths;
        }
    }
}