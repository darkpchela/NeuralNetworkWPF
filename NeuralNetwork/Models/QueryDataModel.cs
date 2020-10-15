using NeuralNetwork.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.Models
{
    public class QueryDataModel : INotifyPropertyChanged
    {
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

        private IEnumerable<float> _values;
        public IEnumerable<float> Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
                OnPropertyChanged(nameof(Values));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public QueryDataVM GetViewModel()
        {
            var viewModel = new QueryDataVM(this);
            viewModel.Marker = Marker;

            return viewModel;
        }
    }
}