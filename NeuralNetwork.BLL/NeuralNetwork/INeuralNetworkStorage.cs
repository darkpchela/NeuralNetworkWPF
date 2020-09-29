using System;
using System.Collections.Generic;

namespace NeuralNetwork.BLL.NeuralNetwork
{
    public interface INeuralNetworkStorage : IDisposable
    {
        Dictionary<Guid, INeuralNetworkInstanse> NeuralNetworkInstanses { get; }
    }
}