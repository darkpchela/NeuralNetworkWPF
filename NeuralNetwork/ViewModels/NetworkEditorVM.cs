using NeuralNetwork.Infrastructure.Commands;
using NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NeuralNetwork.ViewModels
{
    public class EditorVM : INotifyPropertyChanged
    {
        private NetworkWorkshopModel _workshopModel = NetworkWorkshopModel.Instanse;

        private NetworkStorageVM _stoargeAtWork;
        public NetworkStorageVM StorageAtWork
        {
            get
            {
                return _stoargeAtWork;
            }
            set
            {
                _stoargeAtWork = value;
                OnPropertyChanged("StorageAtWork");
            }
        }

        private NetworkVM _networkAtWork;
        public NetworkVM NetworkAtWork
        {
            get
            {
                return _networkAtWork ?? new NetworkVM()
                {
                    IsPrototype = true
                };
            }
            set
            {
                _networkAtWork = value;
                
                if (value != null && StorageAtWork != null && value.IsPrototype && !StorageAtWork.IsPrototype)
                    NetworkEditorEnabled = true;

                OnPropertyChanged("NetworkAtWork");
            }
        }

        private bool _networkEditorEnabled;
        public bool NetworkEditorEnabled
        {
            get
            {
                return _networkEditorEnabled;
            }
            set
            {
                _networkEditorEnabled = value;
                OnPropertyChanged(nameof(NetworkEditorEnabled));
            }
        }

        private RelayCommand _removeLayer;
        public RelayCommand RemoveLayer
        {
            get
            {
                return _removeLayer ?? (_removeLayer = new RelayCommand(obj =>
                {
                    if (NetworkAtWork.LayersCount > 2)
                        NetworkAtWork.Layers.RemoveAt(NetworkAtWork.LayersCount - 2);
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
                    var layer = new NetworkLayerVM();
                    int insertIndex = NetworkAtWork.LayersCount >= 2 ? NetworkAtWork.LayersCount - 1 : 0;
                    
                    if (NetworkAtWork.LayersCount == 0)
                        layer.IsOutputLayer = true;

                    layer.LayerIndex = insertIndex;

                    NetworkAtWork.Layers.Insert(insertIndex, layer);
                    NetworkAtWork.Layers.Last().LayerIndex++;
                }));
            }
        }

        private RelayCommand _createNetwork;
        public RelayCommand CreateNetwork
        {
            get
            {
                return _createNetwork ?? (_createNetwork = new RelayCommand(obj =>
                {
                    _workshopModel.CreateNetwork(NetworkAtWork, StorageAtWork.Id);
                    NetworkAtWork = null;
                }));
            }
        }

        private RelayCommand _createStorage;
        public RelayCommand CreateStorage
        {
            get
            {
                return _createStorage ?? (_createStorage = new RelayCommand(obj =>
                {
                    if (StorageAtWork.Name != NetworkWorkshopModel.DefaultStorageName && _workshopModel.Storages.FirstOrDefault(s => s.Name == StorageAtWork.Name) is null)
                    {
                        _workshopModel.CreateStorage(StorageAtWork);
                        StorageAtWork = null;
                    }
                    else
                        MessageBox.Show("Invalid storage name!");
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
