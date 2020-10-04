using NeuralNetwork.Infrastructure.Commands;
using NeuralNetwork.Model.NeuralNetworkWorkshopModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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

        private bool _storageSelected;
        public bool StorageSelcted
        {
            get
            {
                return _storageSelected;
            }
            set
            {
                _storageSelected = value;
                OnPropertyChanged("StorageSelected");
            }
        }

        private bool _networkSelected;
        public bool NetworkSelected
        {
            get
            {
                return _networkSelected;
            }
            set
            {
                _networkSelected = value;
                OnPropertyChanged("NetworkSelected");
            }
        }

        private ObservableCollection<NetworkStorageVM> _networkStoragesList = new ObservableCollection<NetworkStorageVM>
        {
            new NetworkStorageVM{Id = Guid.NewGuid().ToString(), InputsCount = 3, OutputsCount = 4, Networks =
            new ObservableCollection<NetworkVM>
            {
                new NetworkVM{ Id = Guid.NewGuid().ToString(), ActivationFuncName = "Sigmoid", InputsCount = 3, OutputsCount = 4, LayersCount = 4,
                    LearningRate = 0.25f},
                                new NetworkVM{ Id = Guid.NewGuid().ToString(), ActivationFuncName = "Sigmoid", InputsCount = 3, OutputsCount = 4, LayersCount = 4,
                    LearningRate = 0.25f}
            }
            },
            new NetworkStorageVM{Id = Guid.NewGuid().ToString(), InputsCount = 3, OutputsCount = 4, Networks =
            new ObservableCollection<NetworkVM>
            {
                new NetworkVM{ Id = Guid.NewGuid().ToString(), ActivationFuncName = "Sigmoid", InputsCount = 3, OutputsCount = 4, LayersCount = 4,
                    LearningRate = 0.25f}
            },
        }};
        public ObservableCollection<NetworkStorageVM> NetworkStoragesList
        {
            get
            {
                return _networkStoragesList ?? (_networkStoragesList = new ObservableCollection<NetworkStorageVM>());
            }
        }

        private NetworkStorageVM _currentNetworkStorage;
        public NetworkStorageVM CurrentNetworkStorage
        {
            get
            {
                return _currentNetworkStorage ?? (_currentNetworkStorage = new NetworkStorageVM());
            }
            set
            {
                _currentNetworkStorage = value;
                OnPropertyChanged("CurrentNetworkStorage");
            }
        }

        private NetworkVM _currentNetwork;
        public NetworkVM CurrentNetwork
        {
            get 
            {
                return _currentNetwork ?? (_currentNetwork = new NetworkVM()); 
            }
            set
            {
                _currentNetwork = value;
                OnPropertyChanged("CurrentNetworkVM");
            }
        }

        private RelayCommand _selectStorage;
        public RelayCommand SelectStorage
        {
            get
            {
                return _selectStorage ?? (_selectStorage = new RelayCommand(obj=> 
                {
                    MessageBox.Show(obj.ToString());
                }));
            }
        }

    }
}