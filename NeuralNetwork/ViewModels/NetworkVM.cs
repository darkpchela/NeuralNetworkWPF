using NeuralNetwork.Model.NeuralNetworkWorkshopModel;
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
        private Guid? _id;
        public string Id 
        {
            get
            {
                if (_id.HasValue)
                    return _id.Value.ToString();
                else
                    return "~~~~~~~~-~~~~-~~~~-~~~~-~~~~~~~~~~~~";
            }
            set
            {
                if (Guid.TryParse(value, out Guid id))
                    _id = id;
                else
                    _id = null;

                OnPropertyChanged("Id");
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _name = value;

                OnPropertyChanged("Name");
            }
        }

        string _currentFunc;
        public string CurrentFunc 
        {
            get
            {
                if (string.IsNullOrEmpty(_currentFunc))
                    return "~~~No function~~~";
                else
                    return _currentFunc;
            }
            set
            {
                _currentFunc = value;
                OnPropertyChanged("ActivationFuncName");
            }
        }

        public IEnumerable<string> AllFuncs
        {
            get 
            { 
                return NetworkWorkshopModel.GetAllFuncsNames();
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