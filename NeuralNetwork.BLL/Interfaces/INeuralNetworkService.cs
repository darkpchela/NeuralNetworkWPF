using NeuralNetwork.Model.DTOModels;
using NeuralNetwork.Core;
using System;

namespace NeuralNetwork.Model.Interfaces
{
    public interface INeuralNetworkService<T> where T : NeuralNetworkAbstract
    {
        float[] Query(float[] inputs, Guid networkId);

        float[][] QueryAll(float[] inputs);

        void Train(float[] inputs, float[] targets, Guid networkId);

        void TrainAll(float[] inputs, float[] targets);

        void CreateInstance(NeuralNetworkDataDTO nNetworkData);
    }
}