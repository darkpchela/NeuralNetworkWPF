using NeuralNetwork.Infrastructure.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private NetworkWorkshopVM _networkWorkshopVM;
        public NetworkWorkshopVM NetworkWorkshopVM
        {
            get
            {
                return _networkWorkshopVM ?? (_networkWorkshopVM = new NetworkWorkshopVM());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}