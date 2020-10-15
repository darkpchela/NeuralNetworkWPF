namespace NeuralNetwork.Infrastructure.Interfaces
{
    public interface IFileDialogService
    {
        bool OpenFileDialog(out string fileName, string filter = null);

        bool OpenFileDialog(out string[] fileNames, string filter = null);

        bool OpenFolder(out string folderPath);

        bool SaveFileDialog(string filter);

    }
}