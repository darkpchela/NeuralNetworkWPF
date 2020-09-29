using NeuralNetwork.Core.Etc;
using NeuralNetwork.Core.Extensions;
using NeuralNetwork.Core.Interfaces;
using System;

namespace NeuralNetwork.Core
{
    public class NeuralNetworkBase : INeuralNetwork
    {
        public Func<float, float> ActivationFunc { get; set; }
        public int[] Layers { get; set; }
        public Matrix2D[] Weigths { get; set; }
        public float LearningRate { get; set; }

        public Matrix2D[] _QueryHiddenOutputs;

        public NeuralNetworkBase(int[] Layers, Func<float, float> ActivationFunc, float LearningRate = 0.05f)
        {
            this.Layers = Layers;
            this.Weigths = new Matrix2D[Layers.Length];
            this.ActivationFunc = ActivationFunc;
            this.LearningRate = LearningRate;
            this._QueryHiddenOutputs = new Matrix2D[Layers.Length];
            InitStartWeiths();
        }

        public NeuralNetworkBase(NeuralNetworkData nrlNetData)
        {
            this.Layers = nrlNetData.Layers;
            this.Weigths = nrlNetData.Weights;
            if (FuncDictionary.TryGetFunc(nrlNetData.ActivationFuncName, out Func<float, float> activationFunc))
                this.ActivationFunc = activationFunc;
            else
                this.ActivationFunc = MathFuncs.Sigmoid;
        }

        public void Train(float[] inputValues, float[] targetValues)
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

        public float[] Query(float[] inputValues)
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

        protected virtual void InitStartWeiths()
        {
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
                this.Weigths[i] = CurrentLayerWeiths;
            }
        }

        #region Disposable

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
            }
        }

        ~NeuralNetworkBase()
        {
            Dispose(false);
        }

        #endregion Disposable
    }
}