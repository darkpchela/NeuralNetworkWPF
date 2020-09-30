using System;

namespace NeuralNetwork.BLL.Interfaces
{
    public interface INeuralNetworkService
    {
        float[] QueryNetwork(float[] inputs, Guid networkId);

        float[][] QueryAll(float[] inputs);

        void TrainNetwork(float[] inputs, float[] targets, Guid networkId);

        void TrainAll(float[] inputs, float[] targets);
    }
}