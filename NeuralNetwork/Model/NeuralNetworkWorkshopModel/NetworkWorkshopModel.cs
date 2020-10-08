using NeuralNetwork.Core.Default;
using NeuralNetwork.Core.Etc;
using NeuralNetwork.Services.Interfaces;
using NeuralNetwork.Services.Services;
using NeuralNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.Model.NeuralNetworkWorkshopModel
{
    public class NetworkWorkshopModel :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private string _test="1";
        public string Test
        {
            get
            {
                return _test;
            }
            set
            {
                _test = value;
                OnPropertyChanged("Test");
            }
        }

        public static NetworkWorkshopModel Instanse { get; } = new NetworkWorkshopModel();
        public ObservableCollection<NetworksStorageModel> Storages { get; private set; }
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
            Storages = new ObservableCollection<NetworksStorageModel>()
            {
                _tempStorage 
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
                var storage = Storages.First(s => s.Id == Guid.Parse(storageId));
                storage.AddInstance(network);
            }
            Test = "2";
        }

        public float[] Query(float[] inputs, string networkId)
        {
            return null;
        }

        private NeuralNetworkDefaultData NetworkViewModelToNeuralNetworkDefaultData(NetworkVM networkVM)
        {
            var defData = new NeuralNetworkDefaultData();
            if (Guid.TryParse(networkVM.Id, out Guid id))
                defData.Id = id;

            defData.ActivationFuncName = networkVM.CurrentFunc;
            defData.LearningRate = networkVM.LearningRate;
            defData.Layers = networkVM.Layers.Select(l => l.NeuronsCount).ToArray();

            return defData;
        }
    }
}