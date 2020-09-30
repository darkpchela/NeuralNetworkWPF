using NeuralNetwork.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace NeuralNetwork.Core
{
    public class NeuralNetworksDefaultStorage : INeuralNetworksStorage<NeuralNetworkDefault>
    {
        public Dictionary<Guid, NeuralNetworkDefault> NeuralNetworkInstanses { get; set; }

        public NeuralNetworksDefaultStorage()
        {
            NeuralNetworkInstanses = new Dictionary<Guid, NeuralNetworkDefault>();
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