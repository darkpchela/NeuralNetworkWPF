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
                _layerIndex = value;
                OnPropertyChanged("LayerIndex");
            }
        }

        private int _neuronsCount;

        public int NeuronsCount
        {
            get
            {
                return _neuronsCount;
            }
            set
            {
                if (value > 0)
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
            _neuronsCount = 2;
        }
    }
}