using NeuralNetwork.Core.Default;
using NeuralNetwork.Core.Etc;
using NeuralNetwork.Services.Interfaces;
using NeuralNetwork.Services.Services;
using NeuralNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public string[] GetAllFuncsNames()
        {
            return FuncDictionary.GetAllFuncsNames();
        }

        public void Create(NetworkVM networkPrototype)
        {
            var defData = new NeuralNetworkDefaultData();
            defData.ActivationFuncName = networkPrototype.ActivationFuncName;
            defData.LearningRate = networkPrototype.LearningRate;
            defData.Layers = networkPrototype.Layers.Select(l=>l.NeuronsCount).ToArray();

            _nrlMaster.CreateInstance(defData);
        }

        public float[] Query(float[] inputs, string networkId)
        {
            return _nrlMaster.Query(inputs, Guid.Parse(networkId));
        }


    }
}