using NeuralNetwork.Core.Default;
using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace NeuralNetwork.Models
{
    public class NetworksStorageModel : NeuralNetworksDefaultStorage<NetworkModel>
    {
        public event NotifyCollectionChangedEventHandler StorageChanged;

        private void OnStorageChanged(NotifyCollectionChangedEventArgs e)
        {
            StorageChanged?.Invoke(this, e);
        }

        public string Name { get; set; }

        public NetworksStorageModel(bool isStrict) : base(isStrict)
        {
        }

        public override void AddInstance(NetworkModel nNetworkInstance)
        {
            base.AddInstance(nNetworkInstance);
            var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, nNetworkInstance.Id);
            OnStorageChanged(e);
        }

        public override void RemoveInstance(Guid id)
        {
            base.RemoveInstance(id);
            var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, id);
            OnStorageChanged(e);
        }
    }
}