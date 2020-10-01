using NeuralNetwork.BLL.DTOModels;
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

        public void CreateInstance(NeuralNetworkDataDTO nNetworkData)
        {
            var data = nNetworkData.ToNeuralNetworkData<NeuralNetworkDefaultData>();
            var instance = _nrlNetFactory.CreateInstance(data);

            _nrlNetStorage.AddInstance(instance);
        }

        public float[] Query(float[] inputs, Guid networkId)
        {
            var network = _nrlNetStorage.GetInstance(networkId);

            return network.Query(inputs);
        }

        public float[][] QueryAll(float[] inputs)
        {
            float[][] outputs = new float[_nrlNetStorage.StorageConstraints.OutputsCount][];
            var allInstances = _nrlNetStorage.GetAllInstances();

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
            var network = _nrlNetStorage.GetInstance(networkId);
            network.Train(inputs, targets);
        }

        public void TrainAll(float[] inputs, float[] targets)
        {
            var allInstances = _nrlNetStorage.GetAllInstances();

            foreach (var nn in allInstances)
            {
                nn.Train(inputs, targets);
            }
        }
    }
}