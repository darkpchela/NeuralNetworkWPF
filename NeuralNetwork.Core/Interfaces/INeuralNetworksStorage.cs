using System;
using System.Collections.Generic;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INeuralNetworksStorage<T> : IDisposable where T : NeuralNetworkAbstract 
    {
        Dictionary<Guid, T> NeuralNetworkInstanses { get; set; }
    }
}