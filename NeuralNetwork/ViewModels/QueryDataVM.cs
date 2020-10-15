using NeuralNetwork.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.ViewModels
{
    public class QueryDataVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public QueryDataVM(QueryDataModel model)
        {
            Marker = model.Marker;
            DataModel = model;
        }

        private string _marker;
        public string Marker
        {
            get
            {
                return _marker;
            }
            set
            {
                _marker = value;
                OnPropertyChanged(nameof(Marker));
            }
        }

        private QueryDataModel _dataModel;
        public QueryDataModel DataModel
        {
            get
            {
                return _dataModel;
            }
            private set
            {
                _dataModel = value;
            }
        }
    }
}