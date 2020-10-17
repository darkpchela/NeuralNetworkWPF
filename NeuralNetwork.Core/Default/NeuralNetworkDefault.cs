using NeuralNetwork.Core.Etc;
using NeuralNetwork.Core.Extensions;
using NeuralNetwork.Core.Structs;
using System;

namespace NeuralNetwork.Core.Default
{
    public class NeuralNetworkDefault : NeuralNetworkAbstract
    {
        public Matrix2D[] AllOutputs { get; private set; }

        public NeuralNetworkDefault(int[] layers, Func<float, float> activationFunc, float learningRate = 0.05f)
        {
            this.Id = Guid.NewGuid();
            this.Layers = layers;
            this.ActivationFunc = activationFunc;
            this.LearningRate = learningRate;
            this.Weigths = GetDefaultWeigths();
            this.AllOutputs = new Matrix2D[Layers.Length];
        }

        public NeuralNetworkDefault(Matrix2D[] weights, Func<float, float> activationFunc, float learningRate = 0.05f)
        {
            this.Id = Guid.NewGuid();
            this.ActivationFunc = activationFunc;
            this.Weigths = weights;
            this.LearningRate = learningRate;
            this.Layers = new int[Weigths.Length + 1];
            this.AllOutputs = new Matrix2D[Layers.Length];

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

        public NeuralNetworkDefault(NeuralNetworkDefaultData nrlNetData)
        {
            Id = nrlNetData.Id ?? Guid.NewGuid();
            Layers = nrlNetData.Layers;
            Weigths = nrlNetData.Weights ?? GetDefaultWeigths();
            LearningRate = nrlNetData.LearningRate;
            AllOutputs = new Matrix2D[Layers.Length];

            if (!string.IsNullOrEmpty(nrlNetData.ActivationFuncName) && FuncDictionary.TryGetFunc(nrlNetData.ActivationFuncName, out Func<float, float> activationFunc))
                ActivationFunc = activationFunc;
            else
                ActivationFunc = MathFuncs.Sigmoid;
        }

        public override void Train(float[] inputValues, float[] targetValues)
        {
            var targetMatrix = targetValues.ToMatrix2D().Transpose();
            var outputMatrix = Query(inputValues).ToMatrix2D().Transpose();
            var errorMatrix = targetMatrix - outputMatrix;

            for (int i = Layers.Length - 1; i > 0; i--)
            {
                var currentOutputMatrix = AllOutputs[i];
                var previousOutputMatrix = AllOutputs[i - 1];
                var deltaWeigthMatrix = LearningRate * (Matrix2D.ScalerProduct(errorMatrix * currentOutputMatrix * (1.0f - currentOutputMatrix), previousOutputMatrix.Transpose()));
                errorMatrix = Matrix2D.ScalerProduct(Weigths[i - 1].Transpose(), errorMatrix);
                Weigths[i - 1] += deltaWeigthMatrix;
            }
        }

        public override float[] Query(float[] inputValues)
        {
            if (inputValues.Length != Layers[0])
                throw new ArithmeticException("Invalid inputs count");

            Matrix2D inputs_outputs = inputValues.ToMatrix2D().Transpose();

            AllOutputs[0] = inputs_outputs;

            for (int i = 0; i < Layers.Length - 1; i++)
            {
                inputs_outputs = Matrix2D.ScalerProduct(Weigths[i], inputs_outputs);
                inputs_outputs = inputs_outputs.ForEach(ActivationFunc);
                AllOutputs[i + 1] = inputs_outputs;
            }

            float[] outputs = inputs_outputs.ToSingleArray();

            return outputs;
        }

        public override float[] BackQuery(float[] tragetValues)
        {
            var outputs = tragetValues.ToMatrix2D().Transpose();
            var inputs = outputs.ForEach(MathFuncs.SigmoidReverse);

            for (int i = AllOutputs.Length - 1; i > 0; i--)
            {
                outputs = Matrix2D.ScalerProduct(Weigths[i -1].Transpose(), inputs);
                outputs -= outputs.GetMin();
                outputs /= outputs.GetMax();
                outputs *= 0.98f;
                outputs += 0.01f;
                inputs = outputs.ForEach(MathFuncs.SigmoidReverse);
                inputs -= inputs.GetMin();
                inputs /= inputs.GetMax();
                inputs *= 0.98f;
                inputs += 0.01f;
            }

            return inputs.ToSingleArray();

        //    def backquery(self, targets_list):
        //# transpose the targets list to a vertical array
        //final_outputs = numpy.array(targets_list, ndmin = 2).T

        //# calculate the signal into the final output layer
        //    final_inputs = self.inverse_activation_function(final_outputs)

        //# calculate the signal out of the hidden layer
        //    hidden_outputs = numpy.dot(self.who.T, final_inputs)
        //# scale them back to 0.01 to .99
        //hidden_outputs -= numpy.min(hidden_outputs)
        //hidden_outputs /= numpy.max(hidden_outputs)
        //hidden_outputs *= 0.98
        //hidden_outputs += 0.01

        //# calculate the signal into the hidden layer
        //    hidden_inputs = self.inverse_activation_function(hidden_outputs)

        //# calculate the signal out of the input layer
        //    inputs = numpy.dot(self.wih.T, hidden_inputs)
        //# scale them back to 0.01 to .99
        //inputs -= numpy.min(inputs)
        //inputs /= numpy.max(inputs)
        //inputs *= 0.98
        //inputs += 0.01


        //return inputs
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