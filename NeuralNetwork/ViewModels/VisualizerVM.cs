using NeuralNetwork.Infrastructure.Commands;
using NeuralNetwork.Infrastructure.Etc;
using NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace NeuralNetwork.ViewModels
{
    public class VisualizerVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private VisualizerModel visualizerModel = VisualizerModel.Instance;

        private int _width;
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        private int _height;
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        private QueryDataFormat _dataFormat;
        public QueryDataFormat DataFormat
        {
            get
            {
                return _dataFormat;
            }
            set
            {
                _dataFormat = value;
                OnPropertyChanged(nameof(DataFormat));
            }
        }

        private QueryDataVM _inputData;
        public QueryDataVM InputData
        {
            get
            {
                return _inputData;
            }
            set
            {
                _inputData = value;
                
                if (value != null)
                    VisualizeData();

                OnPropertyChanged(nameof(InputData));
            }
        }

        private ObservableCollection<Path> _pixelPaths;
        public ObservableCollection<Path> PixelPaths
        {
            get
            {
                return _pixelPaths;
            }
            set
            {
                _pixelPaths = value;
                OnPropertyChanged(nameof(PixelPaths));
            }
        }

        private void VisualizeData()
        {
            switch (DataFormat)
            {
                case QueryDataFormat.BlackMNIST28x28:
                    Width = VisualizerModel.DEFAULT_POINT_SIZE * 28;
                    Height = VisualizerModel.DEFAULT_POINT_SIZE * 28;
                    PixelPaths = new ObservableCollection<Path>(visualizerModel.VisualizeMnistData(InputData.DataModel));
                    break;
            }
        }
    }
}
