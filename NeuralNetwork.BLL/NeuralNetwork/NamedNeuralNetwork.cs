using NeuralNetwork.BLL.NeuralNetwork.Interfaces;
using NeuralNetwork.Core;
using System;

namespace NeuralNetwork.BLL.NeuralNetwork
{
    public class NamedNeuralNetwork : NeuralNetworkBase, INamedNeuralNetwork
    {
        public string Name { get; set; }

        public Guid Id { get; }

        public NamedNeuralNetwork(int[] layers, Func<float, float> activationFunc, float learningRate = 0.5f,
            string name = null) : base(layers, activationFunc, learningRate)
        {
            this.Id = new Guid();

            if (string.IsNullOrWhiteSpace(name))
                this.Name = Id.ToString();
            else
                this.Name = name;
        }

        public NamedNeuralNetwork(NamedNeuralNetworkData namedNeuralNetworkData) : base(namedNeuralNetworkData)
        {
            this.Name = namedNeuralNetworkData.Name;
            this.Id = namedNeuralNetworkData.Id;
        }
    }
}