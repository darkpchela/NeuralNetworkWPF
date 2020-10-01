using NeuralNetwork.Core.Default;
using NeuralNetwork.Services.Interfaces;
using NeuralNetwork.Services.Services;
using System;
using System.Collections.Generic;

namespace NeuralNetwork.Model.NeuralNetworkWorkshopModel
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

        public void Create(List<int> layers, string activationFuncName, float learningRate)
        {
            var defData = new NeuralNetworkDefaultData();
            defData.ActivationFuncName = activationFuncName;
            defData.LearningRate = learningRate;
            defData.Layers = layers.ToArray();

            _nrlMaster.CreateInstance(defData);
        }

        public float[] Query(float[] inputs, string networkId)
        {

            return _nrlMaster.Query(inputs, Guid.Parse(networkId));
        }
    }
}