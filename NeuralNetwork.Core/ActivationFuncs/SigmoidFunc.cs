using NeuralNetwork.Core.Extensions;
using System;

namespace NeuralNetwork.Core.ActivationFuncs
{
    public class SigmoidFunc : IActivationFunc
    {
        public string Name => "Sigmoid";

        public string StringView => "y(x) = 1 / (1 + (e ^ -x))";
        public Func<float, float> ActivationFunc => MathExtensions.Sigmoid;
    }
}