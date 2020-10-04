using NeuralNetwork.Infrastructure.Commands;
using NeuralNetwork.Model.NeuralNetworkWorkshopModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ViewModels
{
    public class NetworkRedactorVM : INotifyPropertyChanged
    {
        private NetworkWorkshopModel _workshopModel = NetworkWorkshopModel.Instanse;

        private bool _singleMode;
        public bool SingleMode
        {
            get
            {
                return _singleMode;
            }
            private set
            {
                _singleMode = value;
                OnPropertyChanged("SingleMode");
            }
        }

        private NetworkStorageVM _stoargeAtWork;
        public NetworkStorageVM StorageAtWork
        {
            get
            {
                return _stoargeAtWork ?? (_stoargeAtWork = new NetworkStorageVM());
            }
        }

        private NetworkVM _networkPrototype;
        public NetworkVM NetworkPrototye
        {
            get
            {
                return _networkPrototype ?? (_networkPrototype = new NetworkVM());
            }
            set
            {
                _networkPrototype = value;
                OnPropertyChanged("NetworkPrototype");
            }
        }

        private ObservableCollection<string> _funcs;
        public ObservableCollection<string> Funcs
        {
            get
            {
                return _funcs ?? (_funcs = new ObservableCollection<string>(_workshopModel.GetAllFuncsNames()));
            }
        }

        private RelayCommand _create;
        public RelayCommand Create
        {
            get
            {
                return _create ?? (_create = new RelayCommand(obj =>
                {
                    _workshopModel.Create(NetworkPrototye);
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
