using NeuralNetwork.Core.Interfaces;
using System;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INamedNeuralNetwork : INeuralNetwork
    {
        string Name { get; set; }
        Guid Id { get; }
    }
}