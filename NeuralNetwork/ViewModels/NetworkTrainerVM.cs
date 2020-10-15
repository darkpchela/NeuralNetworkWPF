using NeuralNetwork.Infrastructure.Commands;
using NeuralNetwork.Infrastructure.Converters;
using NeuralNetwork.Infrastructure.Etc;
using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services;
using NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NeuralNetwork.ViewModels
{
    public class NetworkTrainerVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private NetworkTrainerModel _trainerModel = NetworkTrainerModel.Instance;
        private IFileDialogService _dialogService;

        public NetworkTrainerVM()
        {
            _dialogService = new DefaultFileDialogService();
        }

        private NetworkVM _currentNetwork;
        public NetworkVM CurrentNetwork
        {
            get
            {
                return _currentNetwork;
            }
            set
            {
                _currentNetwork = value;
                OnPropertyChanged(nameof(CurrentNetwork));
            }
        }

        private NetworkStorageVM _currentStorage;
        public NetworkStorageVM CurrentStorage
        {
            get
            {
                return _currentStorage;
            }
            set
            {
                _currentStorage = value;
                OnPropertyChanged(nameof(CurrentStorage));
            }
        }

        private VisualizerVM _inputVisualizer;
        public VisualizerVM InputVisualizer
        {
            get
            {
                return _inputVisualizer;
            }
            set
            {
                _inputVisualizer = value;
                OnPropertyChanged(nameof(InputVisualizer));
            }
        }

        private VisualizerVM _outputVisualizer;
        public VisualizerVM OutputVisualizer
        {
            get
            {
                return _outputVisualizer;
            }
            set
            {
                _outputVisualizer = value;
                OnPropertyChanged(nameof(OutputVisualizer));
            }
        }

        private QueryDataVM _selectedInputData;
        public QueryDataVM SelectedInputData
        {
            get
            {
                return _selectedInputData;
            }
            set
            {
                _selectedInputData = value;
                OnPropertyChanged(nameof(SelectedInputData));
            }
        }

        private QueryDataVM _selectedOutputData;
        public QueryDataVM SelectedOutputData
        {
            get
            {
                return _selectedOutputData;
            }
            set
            {
                _selectedOutputData = value;
                OnPropertyChanged(nameof(SelectedOutputData));
            }
        }

        private ObservableCollection<QueryDataVM> _outputDatas;
        public ObservableCollection<QueryDataVM> OutputDatas
        {
            get
            {
                return _outputDatas;
            }
            set
            {
                _outputDatas = value;
                OnPropertyChanged(nameof(OutputDatas));
            }
        }

        private ObservableCollection<QueryDataVM> _intputDatas;
        public ObservableCollection<QueryDataVM> InputDatas
        {
            get
            {
                return _intputDatas;
            }
            set
            {
                _intputDatas = value;
                OnPropertyChanged(nameof(InputDatas));
            }
        }

        private IEnumerable<TrainDataFormat> _dataFormats = Enum.GetValues(typeof(TrainDataFormat)).Cast<TrainDataFormat>();
        public IEnumerable<TrainDataFormat> DataFormats
        {
            get
            {
                return _dataFormats;
            }
        }

        private TrainDataFormat _selectedDataFormat;
        public TrainDataFormat SelectedDataFormat
        {
            get
            {
                return _selectedDataFormat;
            }
            set
            {
                _selectedDataFormat = value;
                OnPropertyChanged(nameof(SelectedDataFormat));
            }
        }

        private RelayCommand _loadTrainFile;
        public RelayCommand LoadTrainFile
        {
            get
            {
                return _loadTrainFile ?? (_loadTrainFile = new RelayCommand(async obj =>
                {
                    if (_dialogService.OpenFileDialog(out string fileName))
                    {
                        await _trainerModel.LoadTrainFile(fileName, SelectedDataFormat);
                        InputDatas = new ObservableCollection<QueryDataVM>(_trainerModel.TrainDatas.ToViewModels());
                    }
                    else
                        MessageBox.Show("Error");

                }));
            }
        }

    }
}
