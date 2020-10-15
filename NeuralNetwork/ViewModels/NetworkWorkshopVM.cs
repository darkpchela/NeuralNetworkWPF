using NeuralNetwork.Infrastructure.Commands;
using NeuralNetwork.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Reflection;
using System.Windows.Data;
using NeuralNetwork.Infrastructure.Converters;
using NeuralNetwork.Infrastructure.Etc;
using System.Collections.Specialized;
using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services;
using System.Threading;

namespace NeuralNetwork.ViewModels
{
    public class NetworkWorkshopVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        
        private NetworkWorkshopModel _workshopModel = NetworkWorkshopModel.Instanse;
        private IFileDialogService fileDialogService = new DefaultFileDialogService();

        public NetworkWorkshopVM()
        {
            _workshopModel.Storages.CollectionChanged += OnStorageCollectionChanged;
            WorkingFolder = _workshopModel.WorkingFolder;
            Storages = new ObservableCollection<NetworkStorageVM>(_workshopModel.Storages.ToViewModels());

            PropertyDependencyContainer.Regist(nameof(_workshopModel.WorkingFolder), _workshopModel, nameof(WorkingFolder), this);
        }

        private NetworkTrainerVM _trainerVM;
        public NetworkTrainerVM TrainerVM
        {
            get
            {
                return _trainerVM ?? (_trainerVM = new NetworkTrainerVM());
            }
            set
            {
                _trainerVM = value;
                OnPropertyChanged(nameof(TrainerVM));
            }
        }

        private EditorVM _editorVM;
        public EditorVM EditorVM
        {
            get
            {
                return _editorVM ?? (_editorVM = new EditorVM());
            }
            set
            {
                _editorVM = value;
                OnPropertyChanged(nameof(EditorVM));
            }
        }

        private ObservableCollection<NetworkStorageVM> _storages;
        public ObservableCollection<NetworkStorageVM> Storages
        {
            get
            {
                return _storages;
            }
            set
            {
                _storages = value;
                OnPropertyChanged(nameof(Storages));
            }
        }

        private bool _trainerIsActive;
        public bool TrainerIsActive
        {
            get
            {
                return _trainerIsActive;
            }
            set
            {
                _trainerIsActive = value;
                if (value == true)
                    EditorIsActive = false;

                OnPropertyChanged(nameof(TrainerIsActive));
            }
        }

        private bool _editorIsAtcive;
        public bool EditorIsActive
        {
            get
            {
                return _editorIsAtcive;
            }
            set
            {
                _editorIsAtcive = value;
                if (value == true)
                    TrainerIsActive = false;

                OnPropertyChanged(nameof(EditorIsActive));
            }
        }

        private bool _storageSelected;
        public bool StorageSelected
        {
            get
            {
                return _storageSelected;
            }
            set
            {
                _storageSelected = value;
                OnPropertyChanged(nameof(StorageSelected));
            }
        }

        private string _workingFolder;
        public string WorkingFolder
        {
            get
            {
                return _workingFolder;
            }
            set
            {
                _workingFolder = value;
                OnPropertyChanged(nameof(WorkingFolder));
            }
        }

        private NetworkStorageVM _selectedStorage;
        public NetworkStorageVM SelectedStorage
        {
            get
            {
                return _selectedStorage;
            }
            set
            {
                _selectedStorage = value;

                if (value != null)
                {
                    TrainerIsActive = true;
                    StorageSelected = true;
                }
                else
                {
                    EditorIsActive = false;
                    StorageSelected = false;
                }

                _trainerVM.CurrentStorage = SelectedStorage;
                _trainerVM.CurrentNetwork = SelectedNetwork;

                OnPropertyChanged(nameof(SelectedStorage));
            }
        }

        private NetworkVM _selectedNetwork;
        public NetworkVM SelectedNetwork
        {
            get
            {
                return _selectedNetwork;
            }
            set
            {
                _selectedNetwork = value;
                _editorVM.NetworkAtWork = SelectedNetwork;
                _trainerVM.CurrentNetwork = SelectedNetwork;
                TrainerIsActive = true;
                OnPropertyChanged(nameof(SelectedNetwork));
            }
        }

        private RelayCommand _newNetwork;
        public RelayCommand NewNetwork
        {
            get
            {
                return _newNetwork ?? (_newNetwork = new RelayCommand(obj =>
                {
                    EditorIsActive = true;
                    if (SelectedStorage is null)
                        SelectedStorage = _workshopModel.DefaultStorage.GetViewModel();

                    EditorVM.StorageAtWork =_workshopModel.GetStorageModel(SelectedStorage.Id).GetViewModel();
                    EditorVM.NetworkAtWork = new NetworkVM()
                    {
                        IsPrototype = true
                    };
                }));
            }
        }

        private RelayCommand _newStorage;
        public RelayCommand NewStorage
        {
            get
            {
                return _newStorage ?? (_newStorage = new RelayCommand(obj =>
                {
                    _editorVM.StorageAtWork = new NetworkStorageVM()
                    {
                        IsPrototype = true
                    };
                    EditorIsActive = true;
                }));
            }
        }

        private RelayCommand _removeStorage;
        public RelayCommand RemoveStorage
        {
            get
            {
                return _removeStorage ?? (_removeStorage = new RelayCommand(obj =>
                {
                    _workshopModel.RemoveStorage(SelectedStorage.Id);
                }));
            }
        }

        private RelayCommand _openNetwork;
        public RelayCommand OpenNetwork
        {
            get
            {
                return _openNetwork ?? (_openNetwork = new RelayCommand(obj =>
                {
                    fileDialogService.OpenFileDialog(out string fileName, "Json files(*.json)|*.json");
                    _workshopModel.LoadNetworkAsync(fileName);
                }));
            }
        }

        private RelayCommand _openStorage;
        public RelayCommand OpenStorage
        {
            get
            {
                return _openStorage ?? (_openStorage = new RelayCommand(async obj =>
                {
                    if (fileDialogService.OpenFileDialog(out string fileName))
                    {
                        var loaded = await _workshopModel.LoadStorageAsync(fileName);

                        if (loaded)
                            MessageBox.Show("Loaded");
                        else
                            MessageBox.Show("Error");
                    }

                }));
            }
        }

        private RelayCommand _selectWorkingFolder;
        public RelayCommand SelectWorkingFolder
        {
            get
            {
                return _selectWorkingFolder ?? (_selectWorkingFolder = new RelayCommand(obj =>
                {
                    fileDialogService.OpenFolder(out string folder);
                    _workshopModel.ChangeWorkingFolder(folder);
                }));
            }
        }

        private void OnStorageCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        var model = item as NetworksStorageModel;
                        Storages.Add(model.GetViewModel());
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        var model = item as NetworksStorageModel;
                        Storages.Remove(Storages.First(s => s.Id == model.Id.ToString()));
                    }
                    break;
            }
        }

    }
}