using System;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INeuralNetworkFactory<out T1, T2> where T1 : NeuralNetworkAbstract
    {
        T1 GetNewInstance(int[] layers, Func<float, float> activationFunc, float learningRate = 0.5f);

        T1 LoadInstance(T2 nrlNetData);
    }
}