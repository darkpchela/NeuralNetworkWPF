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
        private IBrowserDialogService fileDialogService = new DefaultFileDialogService();

        public NetworkWorkshopVM()
        {
            _workshopModel.Storages.CollectionChanged += OnStorageCollectionChanged;
            WorkingFolder = _workshopModel.WorkingFolder;
            Storages = new ObservableCollection<NetworkStorageVM>(_workshopModel.Storages.ToViewModels());

            PropertyDependencyContainer.Regist(nameof(_workshopModel.WorkingFolder), _workshopModel, nameof(WorkingFolder), this);
        }

        private EditorVM _editorVM;
        public EditorVM EditorVM
        {
            get
            {
                return _editorVM;
            }
            set
            {
                _editorVM = value;
                OnPropertyChanged(nameof(EditorVM));
            }
        }

        private bool _redactorIsAtcive;
        public bool RedactorIsActive
        {
            get
            {
                return _redactorIsAtcive;
            }
            set
            {
                _redactorIsAtcive = value;
                OnPropertyChanged("RedactorIsActive");
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
                if (value is null)
                    RedactorIsActive = false;
                else
                    RedactorIsActive = true;

                EditorVM = new EditorVM
                {
                    StorageAtWork = SelectedStorage
                };

                OnPropertyChanged("SelectedStorage");
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
                OnPropertyChanged("SelectedNetwork");
            }
        }

        private RelayCommand _newNetwork;
        public RelayCommand NewNetwork
        {
            get
            {
                return _newNetwork ?? (_newNetwork = new RelayCommand(obj =>
                {
                    EditorVM = new EditorVM();
                    RedactorIsActive = true;
                    if (SelectedStorage is null)
                        SelectedStorage = _workshopModel.TempStorage.GetViewModel();
                    EditorVM.StorageAtWork =_workshopModel.GetStorageModel(SelectedStorage.Id).GetViewModel();
                    EditorVM.NetworkAtWork = _workshopModel.GetNetworkPrototype().GetViewModel();
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
                    _workshopModel.CreateStorage();
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

        private RelayCommand _saveNetwork;
        public RelayCommand SaveNetwork
        {
            get
            {
                return _saveNetwork ?? (_saveNetwork = new RelayCommand(async obj =>
                {
                    if (await _workshopModel.SaveNetworkAsync(SelectedNetwork.Id, SelectedStorage.Id))
                        MessageBox.Show("Saved");
                    else
                        MessageBox.Show("Save error");
                }));
            }
        }

        private RelayCommand _saveStorage;
        public RelayCommand SaveStorage
        {
            get
            {
                return _saveStorage ?? (_saveStorage = new RelayCommand(async obj =>
                {
                    var saved = await _workshopModel.SaveStorageAsync(_selectedStorage.Id);
                    if (saved)
                        MessageBox.Show("Saved");
                    else
                        MessageBox.Show("Error");
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