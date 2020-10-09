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
                return _networkAtWork;
            }
            set
            {
                _networkAtWork = value;
                OnPropertyChanged("NetworkAtWork");
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

        private RelayCommand _testMessage;
        public RelayCommand TestMessage
        {
            get
            {
                return _testMessage ?? (_testMessage = new RelayCommand(obj =>
                {
                    MessageBox.Show(NetworkAtWork.LayersCount.ToString());
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

        private RelayCommand _create;
        public RelayCommand Create
        {
            get
            {
                return _create ?? (_create = new RelayCommand(obj =>
                {
                    _workshopModel.CreateNetwork(NetworkAtWork, StorageAtWork?.Id);
                }));
            }
        }

        private RelayCommand _save;
        public RelayCommand Save
        {
            get
            {
                return _save ?? (_save = new RelayCommand(obj =>
                {
                    if (StorageAtWork.Name != NetworkWorkshopModel.DefaultStorageName)
                        _workshopModel.GetStorageModel(StorageAtWork.Id).Name = StorageAtWork.Name;
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
