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

        private void UpdateSources(object sneder, WorkshopSourceChangedEventArgs e)
        {
            switch (e.SourceName)
            {
                case Source.Storages:
                    Storages = new ObservableCollection<StoragePreviewVM>(_workshopModel.Storages.ToPreviewViewModels());
                    break;
                case Source.Networks:
                    if (SelectedStorage != null && e.SourceId == SelectedStorage.Id)
                    NetworksAtStorage = new ObservableCollection<NetworkInfoVM>(_workshopModel.GetStorageModel(e.SourceId).GetAllInstances().ToViewModels());
                    break;
            }
        }
        
        private NetworkWorkshopModel _workshopModel = NetworkWorkshopModel.Instanse;
        public NetworkWorkshopVM()
        {
            _workshopModel.SourceChanged += UpdateSources;
        }


        private NetworkRedactorVM _redactorVM;
        public NetworkRedactorVM RedactorVM
        {
            get
            {
                return _redactorVM;
            }
            set
            {
                _redactorVM = value;
                OnPropertyChanged("RedactorVM");
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

        private ObservableCollection<StoragePreviewVM> _storages;
        public ObservableCollection<StoragePreviewVM> Storages
        {
            get
            {
                return _storages ?? (_storages = new ObservableCollection<StoragePreviewVM>(_workshopModel.Storages.ToPreviewViewModels()));
            }
            set
            {
                _storages = value;
                OnPropertyChanged("Storages");
            }
        }

        private StoragePreviewVM _selectedStorage;
        public StoragePreviewVM SelectedStorage
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
                NetworksAtStorage = new ObservableCollection<NetworkInfoVM>(storageModel.GetAllInstances().ToViewModels());
                RedactorVM = new NetworkRedactorVM 
                { 
                    StorageAtWork = storageModel.ToViewModel() 
                };
                RedactorIsActive = true;
                OnPropertyChanged("SelectedStorage");
            }
        }

        private ObservableCollection<NetworkInfoVM> _networksAtStorage;
        public ObservableCollection<NetworkInfoVM> NetworksAtStorage
        {
            get
            {
                return _networksAtStorage ?? (_networksAtStorage = new ObservableCollection<NetworkInfoVM>());
            }
            set
            {
                _networksAtStorage = value;
                OnPropertyChanged("NetworksAtStorage");
            }
        }

        private NetworkInfoVM _selectedNetwork;
        public NetworkInfoVM SelectedNetwork
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

        private RelayCommand _newNetwork;
        public RelayCommand NewNetwork
        {
            get
            {
                return _newNetwork ?? (_newNetwork = new RelayCommand(obj =>
                {
                    RedactorVM = new NetworkRedactorVM();
                    RedactorIsActive = true;
                    if (SelectedStorage is null)
                        SelectedStorage = _workshopModel.TempStorage.ToPreviewModel();
                    RedactorVM.StorageAtWork =_workshopModel.GetStorageModel(SelectedStorage.Id).ToViewModel();
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