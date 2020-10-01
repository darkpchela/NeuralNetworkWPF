using NeuralNetwork.Core.Interfaces;
using System;

namespace NeuralNetwork.Core.Default
{
    public class NeuralNetworkDefaultMaster : INeuralNetworkMaster<NeuralNetworkDefault>
    {
        public INeuralNetworksStorage<NeuralNetworkDefault> NetworksStorage { get; set; }

        public NeuralNetworkDefaultMaster()
        {
            NetworksStorage = new NeuralNetworksDefaultStorage();
        }

        public NeuralNetworkDefaultMaster(INeuralNetworksStorage<NeuralNetworkDefault> neuralNetworksStorage)
        {
            this.NetworksStorage = neuralNetworksStorage;
        }

        public float[] QueryNetwork(float[] inputs, Guid networkId)
        {
            var network = NetworksStorage.GetInstance(networkId);
            var outputs = network.Query(inputs);

            return outputs;
        }

        public float[][] QueryAll(float[] inputs) //need to be rewrited
        {
            throw new NotImplementedException();
        }

        public void TrainNetwork(float[] inputs, float[] targets, Guid networkId)
        {
            throw new NotImplementedException();
        }

        public void TrainAll(float[] inputs, float[] targets)
        {
            throw new NotImplementedException();
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
                    NetworksStorage.Dispose();
                }
                disposed = true;
            }
        }

        ~NeuralNetworkDefaultMaster()
        {
            Dispose(false);
        }

        #endregion Disposable
    }
}