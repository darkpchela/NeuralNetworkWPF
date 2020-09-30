using NeuralNetwork.BLL.Interfaces;
using NeuralNetwork.Core.Interfaces;
using System;

namespace NeuralNetwork.BLL.Services
{
    public class NeuralNetworkService : INeuralNetworkService
    {
        private INeuralNetworkMaster NeuralNetworkMaster { get; set; }

        public NeuralNetworkService(INeuralNetworkMaster neuralNetworkMaster)
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
    }
}