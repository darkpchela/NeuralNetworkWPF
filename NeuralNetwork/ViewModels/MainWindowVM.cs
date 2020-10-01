using NeuralNetwork.Model.Interfaces;
using NeuralNetwork.Model.Services;
using NeuralNetwork.Infrastructure;
using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services;
using NeuralNetwork.Model.s;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private IFileDialogService _dialogService;
        private NeuralNetworkM _neuralNetworkM;
        private NeuralNetworkVM _networkVM;
        private RelayCommand _openFileCmd;
        private RelayCommand _queryCmd;

        public NeuralNetworkVM NetworkVM
        {
            get
            {
                return _networkVM ?? (_networkVM = new NeuralNetworkVM());
            }
        }

        public RelayCommand QueryCmd
        {
            get
            {
                return _queryCmd ?? (_queryCmd=new RelayCommand(obj=> { 

                }));
            }
        }

        public RelayCommand OpenFileCmd
        {
            get
            {
                return _openFileCmd ?? (_openFileCmd = new RelayCommand(obj =>
                {
                    try
                    {
                        if (_dialogService.OpenFileDialog(out string fileName) == true)
                        {
                            _dialogService.ShowMessage($"{fileName}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _dialogService.ShowMessage(ex.Message);
                    }
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public MainWindowVM(IFileDialogService dialogService)
        {
            _dialogService = dialogService;
            _neuralNetworkM = new NeuralNetworkM(new NeuralNetworkDefaultService(), new FileService());
        }

    }
}
