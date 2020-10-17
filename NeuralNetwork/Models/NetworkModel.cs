using NeuralNetwork.Core.Default;
using NeuralNetwork.Core.Etc;
using NeuralNetwork.Infrastructure.Converters;
using NeuralNetwork.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.Models
{
    public class NetworkModel : NeuralNetworkDefault, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public Guid StorageId { get; set; }

        private string _name;
        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int _generation;
        public int Generation
        {
            get
            {
                return _generation;
            }
            set
            {
                _generation = value;
                OnPropertyChanged(nameof(Generation));
            }
        }

        public NetworkModel(NetworkDataModel networkDataModel) : base(networkDataModel)
        {
            Name = networkDataModel.Name;
            StorageId = networkDataModel.StorageId;
            Generation = networkDataModel.Generation;
        }

        public void Train(QueryDataModel queryDataModel)
        {
            base.Train(queryDataModel.InputValues, queryDataModel.OutputValues);
        }

        public NetworkVM GetViewModel()
        {
            NetworkVM networkVM = new NetworkVM(this)
            {
                Id = Id.ToString(),
                Layers = new ObservableCollection<NetworkLayerVM>(Layers.ToLayerViewModels()),
                CurrentFunc = FuncDictionary.GetFuncName(ActivationFunc),
                LearningRate = LearningRate,
                Name = Name,
                IsPrototype = false,
                Generation = Generation,
                Storageid = StorageId.ToString()
            };
            return networkVM;
        }

        public NetworkDataModel GetNetworkData()
        {
            var data = new NetworkDataModel
            {
                ActivationFuncName = FuncDictionary.GetFuncName(ActivationFunc),
                Id = Id,
                Layers = Layers,
                LearningRate = LearningRate,
                Name = Name,
                Weights = Weigths,
                Generation = Generation,
                StorageId = StorageId
            };

            return data;
        }
    }
}