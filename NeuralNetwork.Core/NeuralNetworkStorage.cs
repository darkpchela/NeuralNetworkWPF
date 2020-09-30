using NeuralNetwork.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace NeuralNetwork.Core
{
    public class NamedNeuralNetworksStorage : INeuralNetworksStorage<NamedNeuralNetwork>
    {
        public Dictionary<Guid, NamedNeuralNetwork> NeuralNetworkInstanses { get; }

        public NamedNeuralNetworksStorage()
        {
            NeuralNetworkInstanses = new Dictionary<Guid, NamedNeuralNetwork>();
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

        ~NamedNeuralNetworksStorage()
        {
            Dispose(false);
        }

        #endregion Disposable
    }
}