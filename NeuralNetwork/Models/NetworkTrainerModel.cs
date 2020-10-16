using NeuralNetwork.Infrastructure.Etc;
using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services;
using NeuralNetwork.Infrastructure.Services.Strategies;
using NeuralNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuralNetwork.Models
{
    public class NetworkTrainerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private IFileService _fileService;
        private NetworkWorkshopModel workshopModel = NetworkWorkshopModel.Instanse;

        public static NetworkTrainerModel Instance = new NetworkTrainerModel();
        private NetworkTrainerModel()
        {
            _fileService = new FileService();
            _trainDatas = new ObservableCollection<QueryDataModel>();
            _outputDatas = new ObservableCollection<QueryDataModel>();
        }

        private ObservableCollection<QueryDataModel> _trainDatas;
        public ObservableCollection<QueryDataModel> TrainDatas
        {
            get
            {
                return _trainDatas ?? (_trainDatas = new ObservableCollection<QueryDataModel>());
            }
            set
            {
                _trainDatas = value;
                OnPropertyChanged(nameof(TrainDatas));
            }
        }

        private ObservableCollection<QueryDataModel> _outputDatas;
        public ObservableCollection<QueryDataModel> OutputDatas
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

        public async void LoadTrainFile(string fileName, QueryDataFormat dataFormat, TaskProgressVM taskVM = null)
        {
            TrainDatas.Clear();

            switch (dataFormat)
            {
                case QueryDataFormat.BlackMNIST28x28:
                    var mnistDatas = await _fileService.ReadFromFileAsync(fileName, new CsvMNISTFileReadStrategy());

                    if (taskVM != null)
                        taskVM.EndValue = mnistDatas.Count();

                    foreach (var data in mnistDatas)
                    {
                        var dataModel = new QueryDataModel();
                        dataModel.Marker = data.Marker.ToString();
                        dataModel.InputValues = (from v in data.PixelsValues select (float)v).ToArray();
                        TrainDatas.Add(dataModel);

                        if (taskVM != null)
                            taskVM.Value++;
                    }
                    break;
            }
        }

        public void TrainNetwork(NetworkModel network, NetworksStorageModel storage ,QueryDataFormat dataFormat, TaskProgressVM taskProgressVM = null)
        {
            if (taskProgressVM != null)
            {
                taskProgressVM.EndValue = TrainDatas.Count;
                taskProgressVM.TaskName = "Training network";
            }


            Parallel.ForEach(TrainDatas, (model) =>
            {
                var trainData = new MNIST28x28ReadyTrainData(model);
                network.Train(trainData);

                lock (taskProgressVM)
                {
                    if (taskProgressVM != null)
                        taskProgressVM.Value++;
                }
            }
            );

            workshopModel.SaveNetworkAsync(network.Id.ToString(), storage.Id.ToString());
        }
    }
}
