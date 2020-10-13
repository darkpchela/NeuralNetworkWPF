using NeuralNetwork.Core.Default;
using NeuralNetwork.Core.Etc;
using NeuralNetwork.Infrastructure.Etc;
using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services;
using NeuralNetwork.Infrastructure.Services.Strategies;
using NeuralNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.IO;

namespace NeuralNetwork.Models
{
    public class NetworkWorkshopModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public const string DefaultStorageName = "Default_storage";
        private string _defaultWorkingFolder = Path.Combine(Directory.GetCurrentDirectory(), "Storages");
        private string _defaultStorageMetaFileName = Path.Combine(Directory.GetCurrentDirectory(), "Storages", DefaultStorageName, "meta.json"); 

        private NetworkFactoryModel _factory;
        private IFileService _fileService;

        private string _workingFolder;
        public string WorkingFolder
        {
            get
            {
                return _workingFolder;
            }
            set
            {
                _workingFolder = value;
                OnPropertyChanged(nameof(WorkingFolder));
            }
        }


        public static NetworkWorkshopModel Instanse { get; } = new NetworkWorkshopModel();

        private NetworkWorkshopModel()
        {
            _fileService = new FileService();
            _factory = new NetworkFactoryModel();
            Storages = new ObservableCollection<NetworksStorageModel>();
            WorkingFolder = _defaultWorkingFolder;
            InitializeDefaultStorage();
        }

        public NetworksStorageModel DefaultStorage { get; private set; }
        public ObservableCollection<NetworksStorageModel> Storages { get; private set; }

        public NetworksStorageModel GetStorageModel(string id)
        {
            return Storages.FirstOrDefault(s => s.Id == Guid.Parse(id));
        }

        public NetworkModel GetNetworkPrototype()
        {
            return _factory.CreatePrototype();
        }

        public async void CreateNetwork(NetworkVM networkPrototype, string storageId = null)
        {

            var defData = NetworkViewModelToNetworkDataModel(networkPrototype);
            var network = _factory.CreateInstance(defData);
            var storage = string.IsNullOrEmpty(storageId) ? DefaultStorage : Storages.First(s => s.Id == Guid.Parse(storageId));

            storage.AddInstance(network);

            await SaveStorageAsync(storage.Id.ToString());
        }

        public async void CreateStorage(NetworkStorageVM storagePrototype)
        {
            var storageModel = new NetworksStorageModel()
            {
                Name = storagePrototype.Name
            };

            Storages.Add(storageModel);
            await SaveStorageAsync(storageModel.Id.ToString());
        }

        public async Task<bool> SaveStorageAsync(string storageId)
        {
            var storageModel = Storages.FirstOrDefault(s => s.Id == Guid.Parse(storageId));

            if (storageModel is null)
                return false;

            return await _fileService.SaveToFileAsync<NetworksStorageModel>(storageModel, WorkingFolder, new StorageModelSaveStrategy());
        }

        public void RemoveStorage(string storageId)
        {
            Storages.Remove(Storages.First(s => s.Id == Guid.Parse(storageId)));
        }

        public async Task<bool> SaveNetworkAsync(string networkId, string storageId)
        {
            var networkModel = GetStorageModel(storageId).GetInstance(Guid.Parse(networkId));
            var data = networkModel.GetNetworkData();
            var saved = await _fileService.SaveToFileAsync(data, WorkingFolder, new NetworkDataModelSaveStrategy());
            return saved;
        }

        public void ChangeWorkingFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                return;

            WorkingFolder = folderPath;
        }

        public async void LoadNetworkAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            var data = await _fileService.ReadFromFileAsync<NetworkDataModel>(fileName, new NetworkDataModelReadStrategy());
            var networkModel = new NetworkModel(data);
            DefaultStorage.AddInstance(networkModel);
        }

        public async Task<bool> LoadStorageAsync(string fileName)
        {
            try
            {
                var storageModel = await _fileService.ReadFromFileAsync<NetworksStorageModel>(fileName, new StorageModelReadStrategy());
                if (Storages.FirstOrDefault(s => s.Id == storageModel.Id) is null)
                {
                    Storages.Add(storageModel);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }


        private async void InitializeDefaultStorage()
        {
            try
            {
                var defStorageModel = await _fileService.ReadFromFileAsync<NetworksStorageModel>(_defaultStorageMetaFileName, new StorageModelReadStrategy());
                DefaultStorage = defStorageModel;
                Storages.Add(defStorageModel);
            }
            catch
            {

                var defStorageModel = new NetworksStorageModel(false)
                {
                    Name = DefaultStorageName
                };

                DefaultStorage = defStorageModel;
                Storages.Add(defStorageModel);

                await SaveStorageAsync(defStorageModel.Id.ToString());
            }
        }
        private NetworkDataModel NetworkViewModelToNetworkDataModel(NetworkVM networkVM)
        {
            var defData = new NetworkDataModel();
            if (Guid.TryParse(networkVM.Id, out Guid id))
                defData.Id = id;

            defData.Name = networkVM.Name;
            defData.ActivationFuncName = networkVM.CurrentFunc;
            defData.LearningRate = networkVM.LearningRate;
            defData.Layers = networkVM.Layers.Select(l => l.NeuronsCount).ToArray();

            return defData;
        }

    }
}