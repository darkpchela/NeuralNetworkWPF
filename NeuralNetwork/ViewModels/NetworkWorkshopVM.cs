using NeuralNetwork.Infrastructure.Commands;
using NeuralNetwork.Model.NeuralNetworkWorkshopModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.ViewModels
{
    public class NetworkWorkshopVM : INotifyPropertyChanged
    {
        private NeuralNetworkWorkshopModel _nrlNetWorkshopModel;

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

        public NetworkWorkshopVM()
        {
            _nrlNetWorkshopModel = new NeuralNetworkWorkshopModel();
        }

        private ObservableCollection<NetworkLayerVM> _layers;

        public ObservableCollection<NetworkLayerVM> Layers
        {
            get
            {
                return _layers ?? (_layers = new ObservableCollection<NetworkLayerVM>() { });
            }
            set
            {
                _layers = value;
                OnPropertyChanged("Items");
            }
        }

        private bool _nrlNetSelected;
        public bool NrlNetSelected
        {
            get
            {
                return _nrlNetSelected;
            }
            set
            {
                _nrlNetSelected = value;
                OnPropertyChanged("NrlNetSelected");
            }
        }

        private string _activationFuncName;
        public string ActivationFuncName
        {
            get
            {
                return _activationFuncName;
            }
            set
            {
                _activationFuncName = value;
                OnPropertyChanged("CurrentActivationFunc");
            }
        }

        private string _currentNrlNetId;
        public string CurrentNrlNetId
        {
            get
            {
                return string.IsNullOrEmpty(_currentNrlNetId) ? "No selected NeuralNetwork" : _currentNrlNetId;
            }
            set
            {
                _currentNrlNetId = value;
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
                if (value > 0)
                    _learningRate = value;
                OnPropertyChanged("LearningRate");
            }
        }

        private ObservableCollection<string> _funcs;
        public ObservableCollection<string> Funcs
        {
            get
            {
                return _funcs ?? (_funcs = new ObservableCollection<string>(_nrlNetWorkshopModel.GetAllFuncsNames()));
            }
        }

        private RelayCommand _create;
        public RelayCommand Create
        {
            get
            {
                return _create ?? (_create = new RelayCommand(obj =>
                {
                    _nrlNetWorkshopModel.Create(_layers, _activationFuncName, _learningRate);
                }));
            }
        }

        private RelayCommand _query;
        public RelayCommand Query
        {
            get
            {
                return _query ?? (_query = new RelayCommand(obj =>
                {
                    var results = _nrlNetWorkshopModel.Query(CurrentInputs.ToArray(), CurrentNrlNetId);
                }));
            }
        }

        private RelayCommand _addLayer;
        public RelayCommand AddLayer
        {
            get
            {
                return _addLayer ?? (_addLayer = new RelayCommand(obj =>
                {
                    _layers.Add(new NetworkLayerVM { LayerIndex = _layers.Count });
                    OnPropertyChanged("Items");
                }));
            }
        }

        private RelayCommand _removeLayer;
        public RelayCommand RemoveLayer
        {
            get
            {
                return _removeLayer ?? (_removeLayer = new RelayCommand(obj=>
                {
                    if (Layers.Count > 0)
                        Layers.RemoveAt(Layers.Count - 1);
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public List<float> CurrentInputs { get; set; } = new List<float> { 0.0f, 0.0f, 0.0f };
        public List<float> CurrentOutputs { get; set; } = new List<float> { 0.0f, 0.0f, 0.0f };

        public string CurrentTrainFile { get; set; }
        public string CurrentFolder { get; set; }
    }
}