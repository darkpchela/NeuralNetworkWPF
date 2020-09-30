using NeuralNetwork.BLL.Interfaces;
using NeuralNetwork.Core;
using NeuralNetwork.Core.Interfaces;
using System;

namespace NeuralNetwork.BLL.Services
{
    public class NeuralNetworkService : INeuralNetworkDefaultService
    {
        private INeuralNetworkMaster<NeuralNetworkDefault> NeuralNetworkMaster { get; set; }

        public NeuralNetworkService()
        {
            NeuralNetworkMaster = new NeuralNetworkDefaultMaster();
        }

        public NeuralNetworkService(INeuralNetworkMaster<NeuralNetworkDefault> neuralNetworkMaster)
        {
            this.NeuralNetworkMaster = neuralNetworkMaster;
        }

        public float[] QueryNetwork(float[] inputs, Guid networkId)
        {
            return NeuralNetworkMaster.QueryNetwork(inputs, networkId);
        }

        public float[][] QueryAll(float[] inputs)
        {
            return NeuralNetworkMaster.QueryAll(inputs);
        }

        public void TrainNetwork(float[] inputs, float[] targets, Guid networkId)
        {
            NeuralNetworkMaster.TrainNetwork(inputs, targets, networkId);
        }

        public void TrainAll(float[] inputs, float[] targets)
        {
            NeuralNetworkMaster.TrainAll(inputs, targets);
        }

        public NeuralNetworkDefault GetNeuralNetworkInstance(Guid id)
        {
            return NeuralNetworkMaster.NetworksStorage.NeuralNetworkInstanses[id];
        }
    }
}