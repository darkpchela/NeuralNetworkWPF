using NeuralNetwork.Infrastructure.Converters;
using NeuralNetwork.Infrastructure.Etc;
using NeuralNetwork.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.ViewModels
{
    public class StoragePreviewVM : INotifyPropertyChanged
    {
        private string _id;
        public string Id 
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private string _name;
        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}