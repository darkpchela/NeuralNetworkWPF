using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INeuralNetworksStorage<T> : IDisposable where T : NeuralNetworkAbstract 
    {
        Dictionary<Guid, T> NeuralNetworkInstanses { get; set; }

        IEnumerable<Guid> GetIds();

        T GetInstance(Guid id);

        IEnumerable<T> GetAllInstances();

        T RemoveInstance(Guid id);

    }
}