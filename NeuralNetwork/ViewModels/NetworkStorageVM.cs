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
        public NetworkStorageVM(NetworksStorageModel model)
        {
            _storageModel = model;
        }

        private Guid _id;
        public string Id 
        {
            get
            {
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

        public IEnumerable<NetworkVM> Networks { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}