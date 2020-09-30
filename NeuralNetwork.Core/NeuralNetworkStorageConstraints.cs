namespace NeuralNetwork.Core
{
    public class NeuralNetworkStorageConstraints
    {
        public int InputsCount { get; }
        public int OutputsCount { get; }

        public NeuralNetworkStorageConstraints(int inputsCount, int outputsCount)
        {
            InputsCount = inputsCount;
            OutputsCount = outputsCount;
        }
    }
}