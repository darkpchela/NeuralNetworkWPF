﻿using NeuralNetwork.Core.Default;
using NeuralNetwork.Core.Etc;
using NeuralNetwork.Services.Interfaces;
using NeuralNetwork.Services.Services;
using NeuralNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NeuralNetwork.Model.NeuralNetworkWorkshopModel
{
    public class NetworkWorkshopModel
    {
        public static NetworkWorkshopModel Instanse { get; } = new NetworkWorkshopModel();

        private Dictionary<Guid, NetworksStorageModel> _strorages;
        private NeuralNetworkDefaultTrainer _trainer;
        private NeuralNetworkDefaultFactory _factory;
        private NetworksStorageModel _tempStorage;

        private IFileService _fileService;

        private NetworkWorkshopModel()
        {
            _fileService = new FileService();

            _trainer = new NeuralNetworkDefaultTrainer();
            _factory = new NeuralNetworkDefaultFactory();
            _tempStorage = new NetworksStorageModel(false) 
            {
                Name = "Temp_storage"
            };
            _strorages = new Dictionary<Guid, NetworksStorageModel>()
            {
                { _tempStorage.Id, _tempStorage }
            };
        }

        public string[] GetAllFuncsNames()
        {
            return FuncDictionary.GetAllFuncsNames();
        }

        public void Create(NetworkVM networkPrototype, string storageId = null)
        {
            var defData = NetworkViewModelToNeuralNetworkDefaultData(networkPrototype);
            var network = _factory.CreateInstance(defData);
            if (string.IsNullOrEmpty(storageId))
            {
                _tempStorage.AddInstance(network);
            }
            else
            {
                var storage = _strorages[Guid.Parse(storageId)];
                storage.AddInstance(network);
            }
        }

        public float[] Query(float[] inputs, string networkId)
        {
            return null;
        }

        private NeuralNetworkDefaultData NetworkViewModelToNeuralNetworkDefaultData(NetworkVM networkVM)
        {
            var defData = new NeuralNetworkDefaultData();
            if (!string.IsNullOrEmpty(networkVM.Id) && Guid.TryParse(networkVM.Id, out Guid id))
                defData.Id = id;

            defData.ActivationFuncName = networkVM.CurrentFunc;
            defData.LearningRate = networkVM.LearningRate;
            defData.Layers = networkVM.Layers.Select(l => l.NeuronsCount).ToArray();

            return defData;
        }
    }
}