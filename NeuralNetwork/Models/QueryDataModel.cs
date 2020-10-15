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
    public class QueryDataModel<T> : INotifyPropertyChanged
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

        private ObservableCollection<T> _values;
        public ObservableCollection<T> Values
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
    }
}
