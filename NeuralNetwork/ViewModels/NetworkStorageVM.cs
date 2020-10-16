using NeuralNetwork.Infrastructure.Converters;
using NeuralNetwork.Infrastructure.Etc;
using NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.ViewModels
{
    public class NetworkStorageVM : INotifyPropertyChanged
    {
        private NetworksStorageModel _storageModel;
        public NetworksStorageModel StorageModel
        {
            get
            {
                return _storageModel;
            }
        }

        public NetworkStorageVM(NetworksStorageModel model = null)
        {
            _storageModel = model;

            if (model != null)
            {
                PropertyDependencyContainer.Regist(nameof(_storageModel.Name), _storageModel, nameof(Name), this);
                PropertyDependencyContainer.Regist(nameof(_storageModel.Networks), _storageModel, nameof(Networks), this, o => ((IEnumerable<NetworkModel>)o).ToViewModels());
            }
        }

        private bool isDefaultStorage;
        public bool IsDefaultStorage
        {
            get
            {
                return isDefaultStorage = Name == NetworkWorkshopModel.DefaultStorageName;
            }
        }

        private bool _isPrototype;
        public bool IsPrototype
        {
            get
            {
                return _isPrototype;
            }
            set
            {
                _isPrototype = value;
                OnPropertyChanged(nameof(IsPrototype));
            }
        }

        private Guid _id;
        public string Id 
        {
            get
            {
                if (IsPrototype)
                    return "Prototype has no Id";

                return _id.ToString();
            }
            set
            {
                if (Guid.TryParse(value, out Guid id))
                    _id = id;

                OnPropertyChanged("Id");
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return string.IsNullOrEmpty(_name) ? (_name = "Unnamed") : _name;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _name = value;

                OnPropertyChanged("Name");
            }
        }

        private int _networksCount;
        public int NetworksCount
        {
            get
            {
                return _networksCount;
            }
            private set
            {
                _networksCount = value;
                OnPropertyChanged("NetworksCount");
            }
        }

        public int InputsCount { get; set; }

        public int OutputsCount { get; set; }

        private IEnumerable<NetworkVM> _networks;
        public IEnumerable<NetworkVM> Networks 
        {
            get
            {
                return _networks;
            }
            set
            {
                _networks = value;
                OnPropertyChanged(nameof(Networks));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}