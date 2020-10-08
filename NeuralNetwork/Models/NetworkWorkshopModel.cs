using NeuralNetwork.Core.Default;
using NeuralNetwork.Core.Etc;
using NeuralNetwork.Infrastructure.Etc;
using NeuralNetwork.Services.Interfaces;
using NeuralNetwork.Services.Services;
using NeuralNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.Models
{
    public class NetworkWorkshopModel
    {
        public event WorkshopSourceChangedEventHandler SourceChanged;
        private void OnSourceChanged(WorkshopSourceChangedEventArgs e)
        {
            SourceChanged?.Invoke(this, e);
        }

        private NeuralNetworkDefaultTrainer _trainer;
        private NetworkFactoryModel _factory;
        private IFileService _fileService;

        public static NetworkWorkshopModel Instanse { get; } = new NetworkWorkshopModel();

        private NetworkWorkshopModel()
        {
            _fileService = new FileService();
            _trainer = new NeuralNetworkDefaultTrainer();
            _factory = new NetworkFactoryModel();
            TempStorage = new NetworksStorageModel(false) 
            {
                Name = "Temp_storage"
            };
            Storages = new ObservableCollection<NetworksStorageModel>()
            {
                TempStorage 
            };
        }


        public NetworksStorageModel TempStorage { get; }
        public ObservableCollection<NetworksStorageModel> Storages { get; }

        public NetworksStorageModel GetStorageModel(string id)
        {
            return Storages.FirstOrDefault(s => s.Id == Guid.Parse(id));
        }

        public string[] GetAllFuncsNames()
        {
            return FuncDictionary.GetAllFuncsNames();
        }

        public void Create(NetworkInfoVM networkPrototype, string storageId = null)
        {
            var defData = NetworkViewModelToNetworkDataModel(networkPrototype);
            var network = _factory.CreateInstance(defData);
            if (string.IsNullOrEmpty(storageId))
            {
                TempStorage.AddInstance(network);
            }
            else
            {
                var storage = Storages.First(s => s.Id == Guid.Parse(storageId));
                storage.AddInstance(network);
            }

            OnSourceChanged(new WorkshopSourceChangedEventArgs(Source.Networks, storageId ?? TempStorage.Id.ToString()));
        }

        private NetworkDataModel NetworkViewModelToNetworkDataModel(NetworkInfoVM networkVM)
        {
            var defData = new NetworkDataModel();
            if (Guid.TryParse(networkVM.Id, out Guid id))
                defData.Id = id;

            defData.ActivationFuncName = networkVM.CurrentFunc;
            defData.LearningRate = networkVM.LearningRate;
            defData.Layers = networkVM.Layers.Select(l => l.NeuronsCount).ToArray();

            return defData;
        }

    }
}