using Microsoft.Win32;
using System.Windows;

namespace NeuralNetwork.Infrastructure.Services
{
    public class DefaultFileDialogService : IFileDialogService
    {
        private string _filter = "Json files(*.json)|*.json|NeuralNetwork data(*.nnd)|*.nnd";

        public string FileName { get; set; }

        public bool OpenFileDialog(out string fileName)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = _filter;
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
                fileName = openFileDialog.FileName;
                return true;
            }

            fileName = string.Empty;

            return false;
        }

        public bool OpenFileDialog(out string[] fileNames)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = _filter;
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                fileNames = openFileDialog.FileNames;
                return true;
            }

            fileNames = null;

            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = _filter;

            if (saveFileDialog.ShowDialog() == true)
            {
                return true;
            }
            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}