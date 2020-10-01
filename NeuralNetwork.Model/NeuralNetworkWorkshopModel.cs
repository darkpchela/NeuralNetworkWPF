using NeuralNetwork.Core.Default;
using NeuralNetwork.Services.Interfaces;
using NeuralNetwork.Services.Services;
using System;

namespace NeuralNetwork.Model
{
    public class NeuralNetworkWorkshopModel
    {
        private NeuralNetworkDefaultMaster _nrlMaster;
        private IFileService _fileService;

        public NeuralNetworkWorkshopModel()
        {
            _nrlMaster = new NeuralNetworkDefaultMaster();
            _fileService = new FileService();
        }

        public float[] Query(float[] inputs, Guid networkId)
        {
            return _nrlMaster.Query(inputs, networkId);
        }
    }
}