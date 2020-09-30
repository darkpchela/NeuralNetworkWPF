using NeuralNetwork.BLL.Interfaces;
using NeuralNetwork.Infrastructure;
using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services;
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
        private NeuralNetworkVM _networkVM;
        private RelayCommand _openFile;

        public NeuralNetworkVM networkVM
        {
            get
            {
                return _networkVM ?? (_networkVM = new NeuralNetworkVM());
            }
        }

        public RelayCommand OpenFile
        {
            get
            {
                return _openFile ?? (_openFile = new RelayCommand(obj =>
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
        }

    }
}
