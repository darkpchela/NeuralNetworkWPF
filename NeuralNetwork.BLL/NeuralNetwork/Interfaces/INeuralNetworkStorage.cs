using System;
using System.Collections.Generic;

namespace NeuralNetwork.BLL.NeuralNetwork.Interfaces
{
    public interface INeuralNetworkStorage : IDisposable
    {
        IEnumerable<INamedNeuralNetwork> NeuralNetworkInstanses { get; }
    }
}