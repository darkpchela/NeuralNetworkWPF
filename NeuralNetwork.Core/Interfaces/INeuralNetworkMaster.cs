using System;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INeuralNetworkMaster<T1, T2> : IDisposable where T1 : NeuralNetworkAbstract where T2 : NeuralNetworkAbstractData
    {
        INeuralNetworkFactory<T1, T2>  NeuralNetworkFactory { get; }
        INeuralNetworksStorage<T1> NetworksStorage { get; set; }

        float[] Query(float[] inputs, Guid networkId);

        float[][] QueryAll(float[] inputs);

        void Train(float[] inputs, float[] targets, Guid networkId);

        void TrainAll(float[] inputs, float[] targets);

        void CreateInstance(T2 nrlNetworkData);

        void CreateRangeOfInstances(T2[] nrlNetworkDatas);
    }
}