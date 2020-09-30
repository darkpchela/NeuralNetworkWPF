using NeuralNetwork.Core.Interfaces;
using System;

namespace NeuralNetwork.Core
{
    public class NeuralNetworkMaster : INeuralNetworkMaster
    {
        public INeuralNetworksStorage NetworksStorage { get; set; }

        public NeuralNetworkMaster(INeuralNetworksStorage neuralNetworksStorage)
        {
            this.NetworksStorage = neuralNetworksStorage;
        }

        public float[] QueryNetwork(float[] inputs, string networkName)
        {
            throw new NotImplementedException();
        }

        public float[] QueryNetwork(float[] inputs, Guid networkId)
        {
            if (!NetworksStorage.NeuralNetworkInstanses.ContainsKey(networkId))
                throw new ArgumentException("Invalid ID");

            var network = NetworksStorage.NeuralNetworkInstanses[networkId];
            var outputs = network.Query(inputs);

            return outputs;
        }

        public float[][] QueryAll(float[] inputs) //need to be rewrited
        {
            throw new NotImplementedException();
        }

        public void TrainNetwork(float[] inputs, float[] targets, string networkName)
        {
            throw new NotImplementedException();
        }

        public void TrainNetwork(float[] inputs, float[] targets, Guid networkId)
        {
            throw new NotImplementedException();
        }

        public void TrainAll(float[] inputs, float[] targets, Guid networkId)
        {
            throw new NotImplementedException();
        }
    }
}