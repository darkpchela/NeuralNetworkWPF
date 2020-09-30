using NeuralNetwork.BLL.Interfaces;
using NeuralNetwork.Core;
using NeuralNetwork.Core.Interfaces;
using System;

namespace NeuralNetwork.BLL.Services
{
    public class NeuralNetworkDefaultService : INeuralNetworkDefaultService
    {
        private INeuralNetworkFactory<NeuralNetworkDefault, NeuralNetworkDefaultData> _nrlNetFactory;
        private INeuralNetworksStorage<NeuralNetworkDefault> _nrlNetStorage;

        public NeuralNetworkDefaultService()
        {
            _nrlNetFactory = new NeuralNetworkDefaultFactory();
            _nrlNetStorage = new NeuralNetworksDefaultStorage();
        }

        public NeuralNetworkDefaultService(INeuralNetworksStorage<NeuralNetworkDefault> neuralNetworksStorage,
            INeuralNetworkFactory<NeuralNetworkDefault, NeuralNetworkDefaultData> neuralNetworkFactory)
        {
            this._nrlNetFactory = neuralNetworkFactory;
            this._nrlNetStorage = neuralNetworksStorage;
        }

        public float[] QueryNetwork(float[] inputs, Guid networkId)
        {
            var network = _nrlNetStorage.GetInstance(networkId);

            return network.Query(inputs);
        }

        public float[][] QueryAll(float[] inputs)
        {
            throw new NotImplementedException();
        }

        public void TrainNetwork(float[] inputs, float[] targets, Guid networkId)
        {
            var network = _nrlNetStorage.GetInstance(networkId);
            network.Train(inputs, targets);
        }

        public void TrainAll(float[] inputs, float[] targets)
        {
            throw new NotImplementedException();
        }

        public NeuralNetworkDefault GetNNetworkInstance(Guid id)
        {
            return _nrlNetStorage.GetInstance(id);
        }

    }
}