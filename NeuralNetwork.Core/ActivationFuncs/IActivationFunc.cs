using System;

namespace NeuralNetwork.Core.ActivationFuncs
{
    public interface IActivationFunc
    {
        string Name { get; }
        string StringView { get; }
        Func<float, float> ActivationFunc { get; }
    }
}