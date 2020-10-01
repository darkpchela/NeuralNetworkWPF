using NeuralNetwork.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace NeuralNetwork.Core
{
    public class NeuralNetworksDefaultStorage : INeuralNetworksStorage<NeuralNetworkDefault>
    {
        private Dictionary<Guid, NeuralNetworkDefault> _instances;

        public NeuralNetworkStorageConstraints StorageConstraints { get; private set; }

        public int Count
        {
            get
            {
                return _instances.Count;
            }
        }

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

        public void AddInstance(NeuralNetworkDefault nNetworkInstance)
        {
            int currentInputsCounts = nNetworkInstance.Layers[0];
            int currentOutputsCount = nNetworkInstance.Layers[nNetworkInstance.Layers.Length - 1];

            if (_instances.Count == 0)
                StorageConstraints = new NeuralNetworkStorageConstraints(currentInputsCounts, currentOutputsCount);
            else if (currentInputsCounts != StorageConstraints.InputsCount || currentOutputsCount != StorageConstraints.OutputsCount)
                throw new ArgumentException("Invalid network properties");

            _instances.Add(nNetworkInstance.Id, nNetworkInstance);
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
                    foreach (var item in _instances)
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