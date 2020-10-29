using NeuralNetwork.Infrastructure.Etc;
using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services;
using NeuralNetwork.Infrastructure.Services.Strategies;
using NeuralNetwork.Infrastructure.Services.Strategies.Etc;
using NeuralNetwork.Models.QueryDataDecorators;
using NeuralNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
        private NetworkWorkshopModel _workshopModel = NetworkWorkshopModel.Instanse;

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

            if (network.Generation == 0)
                SaveHistory(network);

            Parallel.ForEach(TrainDatas, (model) =>
            {
                var trainData = new MNIST28x28TrainData(model);
                network.Train(trainData);


                    if (taskProgressVM != null)
                        taskProgressVM.Value++;

            });

            network.Generation++;
            SaveHistory(network);
            _workshopModel.SaveStorageAsync(storage.Id.ToString());
        }

        public string QueryNetwork(NetworkModel network, QueryDataModel queryData, QueryDataFormat dataFormat)
        {
            switch (dataFormat)
            {
                case QueryDataFormat.BlackMNIST28x28:
                    var outputs = network.Query(queryData.InputValues);
                    return queryData.Marker = Array.IndexOf(outputs, outputs.Max()).ToString();

                default:
                    return "Bad request";
            }
        }

        public QueryDataModel BackQuery(NetworkModel network, QueryDataModel targetData, QueryDataFormat dataFormat)
        {
            switch (dataFormat)
            {
                case QueryDataFormat.BlackMNIST28x28:
                    var inputs = network.BackQuery(targetData.OutputValues);
                    var data = new QueryDataModel()
                    {
                        InputValues = inputs,
                        Marker = targetData.Marker,
                        OutputValues = targetData.OutputValues
                    };
                    var readyData = new MNIST28x28BackQueryData(data);
                    return readyData;

                default:
                    return null;
            }
        }

        private void SaveHistory(NetworkModel networkModel)
        {
            var historyData = new TrainHisoryData()
            {
                Generation = networkModel.Generation,
                NetworkData = networkModel.GetNetworkData()
            };

            string networkFolderPath = Path.Combine(_workshopModel.WorkingFolder, _workshopModel.GetStorageModel(networkModel.StorageId.ToString()).Name, "Networks");
            _fileService.SaveToFileAsync<TrainHisoryData>(historyData, networkFolderPath, new TrainHistoryDataSaveStrategy());
        }
    }
}
