using System;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INeuralNetworkFactory<T1, T2> where T1 : NeuralNetworkAbstract where T2 : NeuralNetworkAbstractData
    {
        T1 CreateInstance(int[] layers, Func<float, float> activationFunc, float learningRate = 0.5f);
        T1 CreateInstance(T2 nNetData);
    }
}