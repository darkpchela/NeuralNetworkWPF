using Microsoft.Win32;
using NeuralNetwork.Infrastructure.Interfaces;
using System.Windows;
using FolderDialog = System.Windows.Forms.FolderBrowserDialog;

namespace NeuralNetwork.Infrastructure.Services
{
    public class DefaultFileDialogService : IBrowserDialogService
    {
        //_filter = "Json files(*.json)|*.json|NeuralNetwork data(*.nnd)|*.nnd";

        public bool OpenFileDialog(out string fileName, string filter = null)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = filter;
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
                return true;
            }

            fileName = string.Empty;

            return false;
        }

        public bool OpenFileDialog(out string[] fileNames, string filter = null)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = filter;
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                fileNames = openFileDialog.FileNames;
                return true;
            }

            fileNames = null;

            return false;
        }

        public bool OpenFolder(out string folderPath)
        {
            FolderDialog folderDialog = new FolderDialog();

            folderDialog.ShowDialog();
            folderPath = folderDialog.SelectedPath;
            return true;
        }

        public bool SaveFileDialog(string filter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = filter;

            if (saveFileDialog.ShowDialog() == true)
            {
                return true;
            }
            return false;
        }
    }
}