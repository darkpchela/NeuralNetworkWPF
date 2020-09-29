using System;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INeuralNetworkFactory
    {
        INamedNeuralNetwork GetNewInstance(int[] layers, Func<float, float> activationFunc, float learningRate = 0.5f,
            string name = null);

        INamedNeuralNetwork LoadInstance(NamedNeuralNetworkData nrlNetData);
    }
}