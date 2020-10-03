using System;
using System.Collections.Generic;
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

        string _activationFuncName;
        public string ActivationFuncName 
        {
            get
            {
                if (string.IsNullOrEmpty(_activationFuncName))
                    return "~~~No function~~~";
                else
                    return _activationFuncName;
            }
            set
            {
                _activationFuncName = value;
                OnPropertyChanged("ActivationFuncName");
            }
        }

        private int _layersCount;
        public int LayersCount 
        {
            get
            {
                return _layersCount;
            }
            set
            {
                _layersCount = value;
                OnPropertyChanged("LayersCount");
            }
        }

        private int _inputsCount;
        public int InputsCount 
        { 
            get
            {
                return _inputsCount;
            }
            set
            {
                _inputsCount = value;
                OnPropertyChanged("InputsCount");
            }
        }

        private int _outputsCount;
        public int OutputsCount 
        {
            get
            {
                return _outputsCount;
            }
            set
            {
                _outputsCount = value;
                OnPropertyChanged("OutputsCount");
            }
        }

        private List<NetworkLayerVM> _layers;
        public List<NetworkLayerVM> Layers 
        {
            get
            {
                return _layers ?? (_layers = new List<NetworkLayerVM>());
            }
            set
            {
                _layers = value;
                OnPropertyChanged("Layers");
                LayersCount = value.Count;
                InputsCount = value.First()?.NeuronsCount ?? 0;
                OutputsCount = value.Last()?.NeuronsCount ?? 0;
            }
        }

        public IEnumerable<float[,]> Weigths { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}