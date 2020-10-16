using NeuralNetwork.Infrastructure.Commands;
using NeuralNetwork.Infrastructure.Converters;
using NeuralNetwork.Infrastructure.Etc;
using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services;
using NeuralNetwork.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
        private SynchronizationContext _syncContext;
        public NetworkTrainerVM()
        {
            _syncContext = SynchronizationContext.Current;
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
                return _inputVisualizer ?? (_inputVisualizer = new VisualizerVM());
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
                return _outputVisualizer ?? (_outputVisualizer = new VisualizerVM());
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
                InputVisualizer.InputData = value;
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
                return _outputDatas ?? (_outputDatas = new ObservableCollection<QueryDataVM>());
            }
            set
            {
                _outputDatas = value;
                OnPropertyChanged(nameof(OutputDatas));
            }
        }

        private ObservableCollection<QueryDataVM> _inputDatas;
        public ObservableCollection<QueryDataVM> InputDatas
        {
            get
            {
                return _inputDatas ?? (_inputDatas = new ObservableCollection<QueryDataVM>());
            }
            set
            {
                _inputDatas = value;
                OnPropertyChanged(nameof(InputDatas));
            }
        }

        private IEnumerable<QueryDataFormat> _dataFormats = Enum.GetValues(typeof(QueryDataFormat)).Cast<QueryDataFormat>();
        public IEnumerable<QueryDataFormat> DataFormats
        {
            get
            {
                return _dataFormats;
            }
        }

        private QueryDataFormat _selectedDataFormat;
        public QueryDataFormat SelectedDataFormat
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

        private ObservableCollection<TaskProgressVM> _tasks;
        public ObservableCollection<TaskProgressVM> Tasks
        {
            get
            {
                return _tasks ?? (_tasks = new ObservableCollection<TaskProgressVM>());
            }
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        private float _taskProgress;
        public float TaskProgress
        {
            get
            {
                return _taskProgress;
            }
            set
            {
                _taskProgress = value;
                OnPropertyChanged(nameof(TaskProgress));
            }
        }

        private string _taskname;
        public string TaskName
        {
            get
            {
                return _taskname;
            }
            set
            {
                _taskname = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }

        private RelayCommand _loadTrainFile;
        public RelayCommand LoadTrainFile
        {
            get
            {
                return _loadTrainFile ?? (_loadTrainFile = new RelayCommand(obj =>
                {
                    if (_dialogService.OpenFileDialog(out string fileName))
                    {
                        var taskVM = new TaskProgressVM();
                        taskVM.TaskName = "Loading data";
                        var loadTask = new Task(() => _trainerModel.LoadTrainFile(fileName, SelectedDataFormat, taskVM));
                        var observableLoadTask = new ObservableTask(loadTask);
                        observableLoadTask.TaskRedied += (sender, e) => _syncContext.Send((state) => Tasks.Add(taskVM), null);
                        observableLoadTask.TaskCompleted += (sender, e) =>
                        {
                            _syncContext.Send((state) => Tasks.Remove(taskVM), null);
                            
                            var subTaskVM = new TaskProgressVM()
                            {
                                EndValue = _trainerModel.TrainDatas.Count,
                                TaskName = "Pushing data",
                                Value = 0
                            };

                            Task subTask = new Task(() =>
                             {
                                 InputDatas = new ObservableCollection<QueryDataVM>();
                                 for (int i = 0; i < _trainerModel.TrainDatas.Count(); i += 25)
                                 {
                                     var restModels = _trainerModel.TrainDatas.Skip(i);
                                     var cureModels = _trainerModel.TrainDatas.Take(25);
                                     foreach (var m in cureModels)
                                     {
                                         _syncContext.Send((state) => InputDatas.Add(m.GetViewModel()), null);
                                         _syncContext.Send((state) => subTaskVM.Value++, null);
                                     }
                                 }
                             });

                            var observableSubTask = new ObservableTask(subTask);
                            observableSubTask.TaskRedied += (senderSub, eSub) => _syncContext.Send((state) => Tasks.Add(subTaskVM), null);
                            observableSubTask.TaskCompleted += (senderSub, eSub) => _syncContext.Send((state) => Tasks.Remove(subTaskVM), null);
                            observableSubTask.Start();
                        };

                        observableLoadTask.Start();
                    }
                    else
                        MessageBox.Show("Error");
                }));
            }
        }
    }
}
