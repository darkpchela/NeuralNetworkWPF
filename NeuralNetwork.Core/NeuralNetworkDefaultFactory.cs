using NeuralNetwork.Core.Interfaces;
using System;

namespace NeuralNetwork.Core
{
    public class NeuralNetworkDefaultFactory : INeuralNetworkFactory<NeuralNetworkDefault, NeuralNetworkDefaultData>
    {
        public INeuralNetworksStorage<NeuralNetworkDefault> NNetworksStorage { get; set; } = new NeuralNetworksDefaultStorage();

        public NeuralNetworkDefault CreateNewInstance(int[] layers, Func<float, float> activationFunc, float learningRate = 0.5f)
        {
            return new NeuralNetworkDefault(layers, activationFunc, learningRate);
        }

        public NeuralNetworkDefault LoadInstance(NeuralNetworkDefaultData nrlNetData)
        {
            return new NeuralNetworkDefault(nrlNetData);
        }
    }
}