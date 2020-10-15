using NeuralNetwork.Infrastructure.Commands;
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

        private IEnumerable<int> _inputs;
        public IEnumerable<int> Inputs
        {
            get
            {
                return _inputs;
            }
            set
            {
                _inputs = value;
                OnPropertyChanged(nameof(Inputs));
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

        private RelayCommand _vizualizeFile;
        public RelayCommand VizualizeFile
        {
            get
            {
                return _vizualizeFile ?? (_vizualizeFile = new RelayCommand(obj =>
                {

                }));
            }
        }
    }
}
