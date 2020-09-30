using System;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INeuralNetworkFactory<T1, T2> where T1 : NeuralNetworkAbstract
    {
        INeuralNetworksStorage<T1> NNetworksStorage { get; set; }
        T1 CreateNewInstance(int[] layers, Func<float, float> activationFunc, float learningRate = 0.5f);

        T1 LoadInstance(T2 nrlNetData);
    }
}