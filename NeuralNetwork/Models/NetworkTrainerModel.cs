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

        public static NetworkTrainerModel Instance = new NetworkTrainerModel();
        private NetworkTrainerModel()
        {
            _fileService = new FileService();
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

        public async Task LoadTrainFile(string fileName, QueryDataFormat dataFormat)
        {
            TrainDatas.Clear();
            GC.Collect();
            switch (dataFormat)
            {
                case QueryDataFormat.BlackMNIST28x28:
                    var mnistDatas = await _fileService.ReadFromFileAsync(fileName, new CsvMNISTFileReadStrategy());
                    foreach (var data in mnistDatas)
                    {
                        var dataModel = new QueryDataModel();
                        dataModel.Marker = data.Marker.ToString();
                        dataModel.Values = from v in data.PixelsValues select (float)v;
                        TrainDatas.Add(dataModel);
                    }
                    break;
            }
        }

    }
}
