using System;
using System.Collections.Generic;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INeuralNetworksStorage : IDisposable
    {
        Dictionary<Guid, INamedNeuralNetwork> NeuralNetworkInstanses { get; }
    }
}