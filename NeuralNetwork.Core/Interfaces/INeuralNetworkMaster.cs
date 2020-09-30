using System;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INeuralNetworkMaster
    {
        INeuralNetworksStorage NetworksStorage { get; set; }

        float[] QueryNetwork(float[] inputs, string networkName);

        float[] QueryNetwork(float[] inputs, Guid networkId);

        float[][] QueryAll(float[] inputs);

        void TrainNetwork(float[] inputs, float[] targets, string networkName);

        void TrainNetwork(float[] inputs, float[] targets, Guid networkId);

        void TrainAll(float[] inputs, float[] targets, Guid networkId);
    }
}