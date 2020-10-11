using NeuralNetwork.Core.Default;
using NeuralNetwork.Core.Etc;
using NeuralNetwork.Infrastructure.Etc;
using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services;
using NeuralNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.Models
{
    public class NetworkWorkshopModel : INotifyPropertyChanged
    {
        public const string DefaultStorageName = "Default_storage";

        private NeuralNetworkDefaultTrainer _trainer;
        private NetworkFactoryModel _factory;
        private IFileService _fileService;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public static NetworkWorkshopModel Instanse { get; } = new NetworkWorkshopModel();

        private NetworkWorkshopModel()
        {
            _fileService = new FileService();
            _trainer = new NeuralNetworkDefaultTrainer();
            _factory = new NetworkFactoryModel();
            TempStorage = new NetworksStorageModel(false) 
            {
                Name = DefaultStorageName
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

        public NetworkModel GetNetworkPrototype()
        {
            return _factory.CreatePrototype();
        }

        public void CreateNetwork(NetworkVM networkPrototype, string storageId = null)
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
        }

        public void CreateStorage()
        {
            var storage = new NetworksStorageModel(true);
            Storages.Add(storage);
            OnPropertyChanged("Storages");
        }

        public bool SaveNetwork()
        {
            return true;
        }

        private NetworkDataModel NetworkViewModelToNetworkDataModel(NetworkVM networkVM)
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