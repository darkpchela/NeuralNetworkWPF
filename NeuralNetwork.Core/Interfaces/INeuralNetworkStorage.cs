using System;
using System.Collections.Generic;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INeuralNetworkStorage : IDisposable
    {
        IEnumerable<INamedNeuralNetwork> NeuralNetworkInstanses { get; }
    }
}