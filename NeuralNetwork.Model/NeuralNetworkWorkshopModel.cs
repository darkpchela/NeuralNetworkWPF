using NeuralNetwork.Services.Interfaces;
using NeuralNetwork.Services.Services;
using System;

namespace NeuralNetwork.Model
{
    public class NeuralNetworkWorkshopModel
    {
        private INeuralNetworkDefaultService _nrlNetService;
        private IFileService _fileService;

        public NeuralNetworkWorkshopModel()
        {
            _nrlNetService = new NeuralNetworkDefaultService();
            _fileService = new FileService();
        }

        public float[] Query(float[] inputs, Guid networkId)
        {
            return _nrlNetService.Query(inputs, networkId);
        }
    }
}
