using NeuralNetwork.Core.Default;
using NeuralNetwork.Infrastructure.Converters;
using NeuralNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.Models
{
    public class NetworksStorageModel : INotifyPropertyChanged
    {
        public  Guid Id { get; }
        public bool IsStrict { get; }

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

        public ICollection<NetworkModel> Networks { get; private set; }

        public NetworkStorageConstraints StorageConstraints { get; private set; }

        public NetworksStorageModel(bool isStrict = true) : this(Guid.NewGuid(), isStrict)
        {
            Networks = new List<NetworkModel>();
        }

        public NetworksStorageModel(Guid id, bool isStrict = true)
        {
            Networks = new List<NetworkModel>();
            Id = id;
            IsStrict = isStrict;
        }

        public NetworkModel GetInstance(Guid id)
        {
            return Networks.FirstOrDefault(i => i.Id == id);
        }

        public void RemoveInstance(Guid id)
        {
            Networks.Remove(Networks.FirstOrDefault(i => i.Id == id));
            OnPropertyChanged(nameof(Networks));
        }

        public void AddInstance(NetworkModel nNetworkInstance)
        {
            int currentInputsCounts = nNetworkInstance.Layers[0];
            int currentOutputsCount = nNetworkInstance.Layers[nNetworkInstance.Layers.Length - 1];

            if (!IsStrict)
            {
                if (Networks.Count == 0 && StorageConstraints is null)
                    StorageConstraints = new NetworkStorageConstraints(currentInputsCounts, currentOutputsCount);

                if (currentInputsCounts != StorageConstraints.InputsCount || currentOutputsCount != StorageConstraints.OutputsCount)
                    throw new ArgumentException("Invalid network properties");
            }

            Networks.Add(nNetworkInstance);
            OnPropertyChanged(nameof(Networks));
        }
        public NetworkStorageVM GetViewModel()
        {
            NetworkStorageVM storageVM = new NetworkStorageVM(this)
            {
                Id = Id.ToString(),
                Name = Name,
                Networks = Networks.ToViewModels(),
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