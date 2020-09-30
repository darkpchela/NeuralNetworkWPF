using NeuralNetwork.Core.Structs;
using System;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INeuralNetwork : IDisposable
    {
        Func<float, float> ActivationFunc { get; set; }

        int[] Layers { get; set; }

        Matrix2D[] Weigths { get; set; }

        float LearningRate { get; set; }

        void Train(float[] inputValues, float[] targetValues);

        float[] Query(float[] inputValues);
    }
} 