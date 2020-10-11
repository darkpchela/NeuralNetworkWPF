using NeuralNetwork.Infrastructure.Commands;
using NeuralNetwork.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Reflection;
using System.Windows.Data;
using NeuralNetwork.Infrastructure.Converters;
using NeuralNetwork.Infrastructure.Etc;
using System.Collections.Specialized;

namespace NeuralNetwork.ViewModels
{
    public class NetworkWorkshopVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        
        private NetworkWorkshopModel _workshopModel = NetworkWorkshopModel.Instanse;
        public NetworkWorkshopVM()
        {
            PropertyDependencyContainer.Regist(nameof(_workshopModel.Storages), _workshopModel, nameof(Storages), this,
                s => new ObservableCollection<NetworkStorageVM>(((IEnumerable<NetworksStorageModel>)s).ToViewModels()));
        }

        private EditorVM _editorVM;
        public EditorVM EditorVM
        {
            get
            {
                return _editorVM;
            }
            set
            {
                _editorVM = value;
                OnPropertyChanged(nameof(EditorVM));
            }
        }

        private bool _redactorIsAtcive;
        public bool RedactorIsActive
        {
            get
            {
                return _redactorIsAtcive;
            }
            set
            {
                _redactorIsAtcive = value;
                OnPropertyChanged("RedactorIsActive");
            }
        }

        private ObservableCollection<NetworkStorageVM> _storages;
        public ObservableCollection<NetworkStorageVM> Storages
        {
            get
            {
                return _storages ?? (_storages = new ObservableCollection<NetworkStorageVM>(_workshopModel.Storages.ToViewModels()));
            }
            set
            {
                _storages = value;
                OnPropertyChanged("Storages");
            }
        }

        private NetworkStorageVM _selectedStorage;
        public NetworkStorageVM SelectedStorage
        {
            get
            {
                return _selectedStorage;
            }
            set
            {
                _selectedStorage = value;
                if (value is null) 
                {
                    RedactorIsActive = false;
                    return;
                }
                var storageModel = _workshopModel.GetStorageModel(value.Id);
                NetworksAtStorage = new ObservableCollection<NetworkVM>(storageModel.Networks.ToViewModels());
                EditorVM = new EditorVM
                {
                    StorageAtWork = SelectedStorage
                };
                RedactorIsActive = true;
                OnPropertyChanged("SelectedStorage");
            }
        }

        private NetworkVM _selectedNetwork;
        public NetworkVM SelectedNetwork
        {
            get
            {
                return _selectedNetwork;
            }
            set
            {
                _selectedNetwork = value;
                OnPropertyChanged("SelectedNetwork");
            }
        }

        private ObservableCollection<NetworkVM> _networksAtStorage;
        public ObservableCollection<NetworkVM> NetworksAtStorage
        {
            get
            {
                return _networksAtStorage ?? (_networksAtStorage = new ObservableCollection<NetworkVM>());
            }
            set
            {
                _networksAtStorage = value;
                OnPropertyChanged("NetworksAtStorage");
            }
        }

        private RelayCommand _newNetwork;
        public RelayCommand NewNetwork
        {
            get
            {
                return _newNetwork ?? (_newNetwork = new RelayCommand(obj =>
                {
                    EditorVM = new EditorVM();
                    RedactorIsActive = true;
                    if (SelectedStorage is null)
                        SelectedStorage = _workshopModel.TempStorage.GetViewModel();
                    EditorVM.StorageAtWork =_workshopModel.GetStorageModel(SelectedStorage.Id).GetViewModel();
                    EditorVM.NetworkAtWork = _workshopModel.GetNetworkPrototype().GetViewModel();
                }));
            }
        }

        private RelayCommand _newStorage;
        public RelayCommand NewStorage
        {
            get
            {
                return _newStorage ?? (_newStorage = new RelayCommand(obj =>
                {
                    _workshopModel.CreateStorage();
                }));
            }
        }

    }
}