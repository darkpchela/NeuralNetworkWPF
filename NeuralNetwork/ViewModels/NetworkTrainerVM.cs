using NeuralNetwork.Infrastructure.Commands;
using NeuralNetwork.Model.NeuralNetworkWorkshopModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ViewModels
{
    public class NetworkTrainer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private NetworkWorkshopModel _workshopModel;

        private ObservableCollection<Guid> _networksIds; 
        public ObservableCollection<Guid> NetworksIds
        {
            get
            {
                return _networksIds ?? (_networksIds = new ObservableCollection<Guid>());
            }
        }

        private NetworkVM _currentNetwork;
        public NetworkVM CurrentNetwork
        {
            get
            {
                return _currentNetwork ?? (_currentNetwork = new NetworkVM());
            }
            set
            {
                _currentNetwork = value;
                OnPropertyChanged("CurrentNetworkVM");
            }
        }

        public List<float> CurrentInputs { get; set; } = new List<float> { 0.0f, 0.0f, 0.0f };
        public List<float> CurrentOutputs { get; set; } = new List<float> { 0.0f, 0.0f, 0.0f };

        public string CurrentTrainFile { get; set; }
        public string CurrentFolder { get; set; }


        private RelayCommand _query;
        public RelayCommand Query
        {
            get
            {
                return _query ?? (_query = new RelayCommand(obj =>
                {
                    var results = _workshopModel.Query(CurrentInputs.ToArray(), CurrentNetwork.Id);
                }));
            }
        }



    }
}
