using NeuralNetwork.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace NeuralNetwork.Core
{
    public class NeuralNetworksStorageTemp : INeuralNetworksStorage
    {
        public Dictionary<Guid, INamedNeuralNetwork> NeuralNetworkInstanses { get; }

        public NeuralNetworksStorageTemp()
        {
            NeuralNetworkInstanses = new Dictionary<Guid, INamedNeuralNetwork>();
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

        ~NeuralNetworksStorageTemp()
        {
            Dispose(false);
        }

        #endregion Disposable
    }
}