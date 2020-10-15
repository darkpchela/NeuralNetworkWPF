using System.Collections.ObjectModel;
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

        private ObservableCollection<int> _values;
        public ObservableCollection<int> Values
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
    }
}