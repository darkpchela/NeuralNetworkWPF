using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.ViewModels
{
    public class NetworkLayerVM : INotifyPropertyChanged
    {
        private int _layerIndex;
        public int LayerIndex
        {
            get
            {
                return _layerIndex;
            }
            set
            {
                if (value >= 0)
                    _layerIndex = value;

                OnPropertyChanged("LayerIndex");
            }
        }

        public bool IsOutputLayer { get; set; }

        private string _layerName;
        public string LayerName
        {
            get
            {   if (IsOutputLayer)
                    _layerName = "Output layer";
                else if (LayerIndex == 0)
                    _layerName = "Input layer";
                else
                    _layerName = $"Hidden layer #{LayerIndex}";

                return _layerName;
            }
            private set
            {
                _layerName = value;
                OnPropertyChanged("LayerName");
            }
        }

        private int _neuronsCount = 2;
        public int NeuronsCount
        {
            get
            {
                return _neuronsCount;
            }
            set
            {
                if (value >= 2)
                    _neuronsCount = value;
                OnPropertyChanged("NeuronsCount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public NetworkLayerVM()
        {
        }
    }
}