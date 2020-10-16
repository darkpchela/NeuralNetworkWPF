using NeuralNetwork.Core.Etc;
using NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.ViewModels
{
    public class NetworkVM : INotifyPropertyChanged
    {
        public NetworkVM(NetworkModel model = null)
        {
            _networkModel = model;
        }
        
        private NetworkModel _networkModel;
        public NetworkModel NetworkModel
        {
            get
            {
                return _networkModel;
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

        private string _id;
        public string Id 
        {
            get
            {
                if (IsPrototype)
                    return "Prototype has no Id";

                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return string.IsNullOrEmpty(_name) ? "Unnamed" : _name;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _name = value;

                OnPropertyChanged("Name");
            }
        }

        private IEnumerable<string> _funcsNames;
        public IEnumerable<string> FuncsNames
        {
            get
            {
                return _funcsNames ?? (_funcsNames = FuncDictionary.GetAllFuncsNames());
            }
        }

        string _currentFunc;
        public string CurrentFunc 
        {
            get
            {
                if (string.IsNullOrEmpty(_currentFunc))
                    return "Sigmoid";
                else
                    return _currentFunc;
            }
            set
            {
                _currentFunc = value;
                OnPropertyChanged("ActivationFuncName");
            }
        }

        public int LayersCount 
        {
            get
            {
                return Layers.Count;
            }
        }

        public int InputsCount 
        { 
            get
            {
                return Layers.First().NeuronsCount;
            }
        }

        public int OutputsCount 
        {
            get
            {
                return Layers.Last().NeuronsCount;
            }
        }

        private float _learningRate;
        public float LearningRate
        {
            get
            {
                return _learningRate;
            }
            set
            {
                _learningRate = value;
                OnPropertyChanged("LearningRate");
            }
        }

        private ObservableCollection<NetworkLayerVM> _layers;
        public ObservableCollection<NetworkLayerVM> Layers 
        {
            get
            {
                return _layers ?? (_layers = new ObservableCollection<NetworkLayerVM>());
            }
            set
            {
                _layers = value;
                OnPropertyChanged("Layers");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}