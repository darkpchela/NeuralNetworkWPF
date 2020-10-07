using NeuralNetwork.Infrastructure.Commands;
using NeuralNetwork.Model.NeuralNetworkWorkshopModel;
using System.Linq;
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
                return _storages ?? (_storages = new ObservableCollection<NetworkStorageVM>());
            }
        }

        private NetworkStorageVM _selectedStorage;
        public NetworkStorageVM SelectedStorage
        {
            get
            {
                return _selectedStorage ?? (_selectedStorage = new NetworkStorageVM());
            }
            set
            {
                _selectedStorage = value;
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

        private RelayCommand _newNetwork;
        public RelayCommand NewNetwork
        {
            get
            {
                return _newNetwork ?? (_newNetwork = new RelayCommand(obj =>
                {
                    SelectedNetwork = new NetworkVM();
                    RedactorVM = new NetworkRedactorVM();
                    RedactorIsActive = true;
                    RedactorVM.NetworkAtWork = SelectedNetwork;
                }));
            }
        }
    }
}