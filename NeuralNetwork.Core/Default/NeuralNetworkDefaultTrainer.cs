namespace NeuralNetwork.Core.Default
{
    public class NeuralNetworkDefaultTrainer
    {
        public float[] Query(NeuralNetworkAbstract network, float[] inputs)
        {
            var outputs = network.Query(inputs);

            return outputs;
        }

        public float[][] Query(NeuralNetworkAbstract[] networks, float[] inputs)
        {
            float[][] outputs = new float[networks.Length][];

            int index = 0;
            foreach (var nn in networks)
            {
                outputs[index] = nn.Query(inputs);
                index++;
            }

            return outputs;
        }

        public void Train(NeuralNetworkAbstract network, float[] inputs, float[] targets)
        {
            network.Train(inputs, targets);
        }

        public void Train(NeuralNetworkAbstract[] networks, float[] inputs, float[] targets)
        {
            foreach (var nn in networks)
            {
                nn.Train(inputs, targets);
            }
        }
    }
}