using NeuralNetwork.Core.Interfaces;
using System;

namespace NeuralNetwork.Core
{
    public class NeuralNetworkDefaultFactory : INeuralNetworkFactory<NeuralNetworkDefault, NeuralNetworkDefaultData>
    {
        public NeuralNetworkDefault CreateInstance(int[] layers, Func<float, float> activationFunc, float learningRate = 0.5f)
        {
            return new NeuralNetworkDefault(layers, activationFunc, learningRate);
        }

        public NeuralNetworkDefault CreateInstance(NeuralNetworkDefaultData nNetData)
        {
            return new NeuralNetworkDefault(nNetData);
        }
    }
}