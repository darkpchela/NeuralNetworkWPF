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
        public float[][,] Weigths { get; set; }
        public float LearningRate { get; set; }

        
        private float[][,] _QueryHiddenOutputs;

        public NrlNet(string Name, int[] Layers, IActivationFunc ActivationFunc, float LearningRate = 0.05f)
        {
            this.Name = Name;
            this.Layers = Layers;
            this.Weigths = new float[Layers.Length][,];
            this.ActivationFunc = ActivationFunc;
            this.LearningRate = LearningRate;
            this._QueryHiddenOutputs = new float[Layers.Length][,];
            InitStartWeiths();
        }

        public NrlNet(NrlNetData nrlNetData)
        {
            this.Name = nrlNetData.Name;
            this.Layers = nrlNetData.Layers;
            this.Weigths = nrlNetData.Weights;
            if (FuncDictionary.FuncName.TryGetValue(nrlNetData.ActivationFuncName, out IActivationFunc activationFunc))
                this.ActivationFunc = activationFunc;
            else
                this.ActivationFunc = new SigmoidFunc();
        }

        public void Train(float[] inputValues, float[] targetValues)
        {
            var targetMatrix = MathExtensions.MatrixTranspose(targetValues);
            var outputMatrix = MathExtensions.MatrixTranspose(Query(inputValues));
            var errorMatrix = MathExtensions.MatrixSubtract(targetMatrix, outputMatrix);

            for (int i = Layers.Length - 1; i >= 0 ; i--)
            {
                var previousErrorMatrix = MathExtensions.MatrixMultiply(MathExtensions.MatrixTranspose(Weigths[i]), errorMatrix);
                var previousOutputMatrix = _QueryHiddenOutputs[i];
            }
        }
        
        //for i in reversed(range(len(self.layers) - 1)):
        //    current_output_signals = outputs[i]
        //    if i == 0:
        //        previous_signals = inputs
        //        pass
        //    else:
        //        previous_signals = outputs[i - 1]
        //        pass


        //    self.weigths[i] += self.learning_rate* np.dot(errors* current_output_signals * (1.0 - current_output_signals), 
        //                                                    np.transpose(previous_signals))
            
        //    errors = np.dot(np.transpose(self.weigths[i]), errors) 
        //    pass

        //pass

        public float[] Query(float[] inputValues)
        {
            if (inputValues.Length != Layers[0])
                throw new ArithmeticException("Invalid inputs count");

            float[,] inputs_outputs = MathExtensions.MatrixTranspose(inputValues);

            _QueryHiddenOutputs[0] = (float[,])inputs_outputs.Clone();

            for (int i = 0; i < Layers.Length - 1; i++)
            {
                //Upper string make new temp inputs for next layer by multiplying current layer weiths to outputs from previous layer
                //Down string applyes activation func to this inputs, makin from it outputs for next layer
                inputs_outputs = MathExtensions.MatrixMultiply(Weigths[i], inputs_outputs);
                MathExtensions.MatrixForEach(ref inputs_outputs, ActivationFunc.ActivationFunc);
                _QueryHiddenOutputs[i+1] = (float[,])inputs_outputs.Clone();
            }

            float[] outputs = inputs_outputs.ConvertToSingleArray();

            return outputs;
        }

        protected void InitStartWeiths()
        {
            Random rnd = new Random();

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
                this.Weigths[i] = CurrentLayerWeiths;
            }
        }
    }
}