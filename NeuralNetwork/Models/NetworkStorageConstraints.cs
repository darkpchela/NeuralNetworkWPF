namespace NeuralNetwork.Models
{
    public class NetworkStorageConstraints
    {
        public int InputsCount { get; }
        public int OutputsCount { get; }

        public NetworkStorageConstraints(int inputsCount, int outputsCount)
        {
            InputsCount = inputsCount;
            OutputsCount = outputsCount;
        }
    }
}