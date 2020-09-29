using NeuralNetwork.Core.Interfaces;
using System;

namespace NeuralNetwork.Core
{
    public class NeuralNetworkFactory : INeuralNetworkFactory
    {
        public INamedNeuralNetwork GetNewInstance(int[] layers, Func<float, float> activationFunc, float learningRate = 0.5f,
            string name = null)
        {
            return new NamedNeuralNetwork(layers, activationFunc, learningRate, name);
        }

        public INamedNeuralNetwork LoadInstance(NamedNeuralNetworkData nrlNetData)
        {
            return new NamedNeuralNetwork(nrlNetData);
        }
    }
}