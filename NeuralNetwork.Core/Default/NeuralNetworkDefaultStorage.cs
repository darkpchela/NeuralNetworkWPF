using System;
using System.Collections.Generic;

namespace NeuralNetwork.Core.Default
{
    public class NeuralNetworksDefaultStorage<T> where T : NeuralNetworkAbstract
    {
        private Dictionary<Guid, T> _instances;

        public Guid Id { get; }

        public bool IsStrict { get; }

        public NeuralNetworkStorageConstraints StorageConstraints { get; private set; }

        public NeuralNetworksDefaultStorage(bool isStrict)
        {
            _instances = new Dictionary<Guid, T>();
            Id = Guid.NewGuid();
            IsStrict = isStrict;
        }

        public virtual int Count
        {
            get
            {
                return _instances.Count;
            }
        }

        public virtual IEnumerable<Guid> GetIds()
        {
            return _instances.Keys;
        }

        public virtual T GetInstance(Guid id)
        {
            return _instances[id];
        }

        public virtual IEnumerable<T> GetAllInstances()
        {
            return _instances.Values;
        }

        public virtual void RemoveInstance(Guid id)
        {
            _instances.Remove(id);
        }

        public virtual void AddInstance(T nNetworkInstance)
        {
            int currentInputsCounts = nNetworkInstance.Layers[0];
            int currentOutputsCount = nNetworkInstance.Layers[nNetworkInstance.Layers.Length - 1];

            if (IsStrict)
            {
                if (_instances.Count == 0 && StorageConstraints is null)
                    StorageConstraints = new NeuralNetworkStorageConstraints(currentInputsCounts, currentOutputsCount);

                if (currentInputsCounts != StorageConstraints.InputsCount || currentOutputsCount != StorageConstraints.OutputsCount)
                    throw new ArgumentException("Invalid network properties");
            }

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