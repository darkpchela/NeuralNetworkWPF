using NeuralNetwork.Core.Interfaces;
using System;

namespace NeuralNetwork.Core.Default
{
    public class NeuralNetworkDefaultMaster : INeuralNetworkMaster<NeuralNetworkDefault, NeuralNetworkDefaultData>
    {
        public INeuralNetworksStorage<NeuralNetworkDefault> NetworksStorage { get; set; }

        public INeuralNetworkFactory<NeuralNetworkDefault, NeuralNetworkDefaultData> NeuralNetworkFactory { get; set; }

        public NeuralNetworkDefaultMaster()
        {
            NetworksStorage = new NeuralNetworksDefaultStorage();
            NeuralNetworkFactory = new NeuralNetworkDefaultFactory();
        }

        public NeuralNetworkDefaultMaster(INeuralNetworksStorage<NeuralNetworkDefault> neuralNetworksStorage)
        {
            this.NetworksStorage = neuralNetworksStorage;
        }

        public float[] Query(float[] inputs, Guid networkId)
        {
            var network = NetworksStorage.GetInstance(networkId);
            var outputs = network.Query(inputs);

            return outputs;
        }

        public float[][] QueryAll(float[] inputs)
        {
            float[][] outputs = new float[NetworksStorage.StorageConstraints.OutputsCount][];
            var allInstances = NetworksStorage.GetAllInstances();

            int index = 0;
            foreach (var nn in allInstances)
            {
                outputs[index] = nn.Query(inputs);
                index++;
            }

            return outputs;
        }

        public void Train(float[] inputs, float[] targets, Guid networkId)
        {
            var network = NetworksStorage.GetInstance(networkId);
            network.Train(inputs, targets);
        }

        public void TrainAll(float[] inputs, float[] targets)
        {
            var allInstances = NetworksStorage.GetAllInstances();

            foreach (var nn in allInstances)
            {
                nn.Train(inputs, targets);
            }
        }

        public void CreateInstance(NeuralNetworkDefaultData nrlNetworkData)
        {
            var instance = NeuralNetworkFactory.CreateInstance(nrlNetworkData);

            NetworksStorage.AddInstance(instance);
        }

        public void CreateRangeOfInstances(NeuralNetworkDefaultData[] nrlNetworkDatas)
        {
            for (int i = 0; i < nrlNetworkDatas.Length; i++)
            {
                NetworksStorage.AddInstance(NeuralNetworkFactory.CreateInstance(nrlNetworkDatas[i]));
            }
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