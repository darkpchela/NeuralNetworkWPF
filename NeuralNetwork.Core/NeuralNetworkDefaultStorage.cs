using NeuralNetwork.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace NeuralNetwork.Core
{
    public class NeuralNetworksDefaultStorage : INeuralNetworksStorage<NeuralNetworkDefault>
    {
        private Dictionary<Guid, NeuralNetworkDefault> _instances { get; set; }

        public NeuralNetworksDefaultStorage()
        {
            _instances = new Dictionary<Guid, NeuralNetworkDefault>();
        }

        public IEnumerable<Guid> GetIds()
        {
            return _instances.Keys;
        }

        public NeuralNetworkDefault GetInstance(Guid id)
        {
            return _instances[id];
        }

        public IEnumerable<NeuralNetworkDefault> GetAllInstances()
        {
            return _instances.Values;
        }

        public void RemoveInstance(Guid id)
        {
            _instances.Remove(id);
        }

        public void AddInstance(NeuralNetworkDefault neuralNetworkInstance)
        {
            _instances.Add(neuralNetworkInstance.Id, neuralNetworkInstance);
        }

        #region Disposable

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    foreach (var item in NeuralNetworkInstanses)
                    {
                        item.Value.Dispose();
                    }
                }
                disposed = true;
            }
        }

        ~NeuralNetworksDefaultStorage()
        {
            Dispose(false);
        }

        #endregion Disposable
    }
}