using NeuralNetwork.Infrastructure;
using NeuralNetwork.Infrastructure.Commands;
using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NeuralNetwork.ViewModels
{

    public class MainWindowVM : INotifyPropertyChanged
    {
        private IFileDialogService _dialogService;

        private NetworkWorkshopVM _networkWorkshopVM;
        private RelayCommand _openFileCmd;
        private RelayCommand _queryCmd;

        public NetworkWorkshopVM NetworkWorkshopVM
        {
            get
            {
                return _networkWorkshopVM ?? (_networkWorkshopVM = new NetworkWorkshopVM());
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
        }

    }
}
