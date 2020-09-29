using NeuralNetwork.Core.Interfaces;
using System;

namespace NeuralNetwork.BLL.NeuralNetwork
{
    public interface INeuralNetworkInstanse : INeuralNetwork
    {
        string Name { get; set; }
        Guid Id { get; }
    }
}