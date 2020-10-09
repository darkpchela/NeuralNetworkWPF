using NeuralNetwork.Core.Default;
using NeuralNetwork.Infrastructure.Converters;
using NeuralNetwork.ViewModels;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.Models
{
    public class NetworksStorageModel : NeuralNetworksDefaultStorage<NetworkModel>, INotifyPropertyChanged
    {
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
                OnPropertyChanged("Name");
            } 
        }

        public NetworksStorageModel(bool isStrict) : base(isStrict)
        {
        }

        public NetworkStorageVM GetViewModel()
        {

            NetworkStorageVM storageVM = new NetworkStorageVM(this)
            {
                Id = Id.ToString(),
                Name = Name,
                Networks = GetAllInstances().ToViewModels()
            };

            return storageVM;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}