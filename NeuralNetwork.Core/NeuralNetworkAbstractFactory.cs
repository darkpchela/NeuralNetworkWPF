namespace NeuralNetwork.Core
{
    public abstract class NeuralNetworkAbstractFactory<T1, T2> where T1 : NeuralNetworkAbstract where T2 : NeuralNetworkAbstractData
    {
        public abstract T1 CreateInstance(T2 nNetData);
    }
}