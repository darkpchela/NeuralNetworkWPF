using NeuralNetwork.BLL.NeuralNetwork.Interfaces;
using NeuralNetwork.Core;
using System;

namespace NeuralNetwork.BLL.NeuralNetwork
{
    public class NamedNeuralNetworkData : NeuralNetworkData
    {
        public string Name { get; private set; }
        public Guid Id { get; private set; }

        public NamedNeuralNetworkData(INamedNeuralNetwork concreteNeuralNetwork) : base(concreteNeuralNetwork)
        {
            this.Id = concreteNeuralNetwork.Id;
            this.Name = concreteNeuralNetwork.Name;
        }
    }
}