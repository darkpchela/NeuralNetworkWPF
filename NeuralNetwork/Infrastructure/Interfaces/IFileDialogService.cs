namespace NeuralNetwork.Infrastructure.Interfaces
{
    public interface IBrowserDialogService
    {
        bool OpenFileDialog(out string fileName, string filter = null);

        bool OpenFileDialog(out string[] fileNames, string filter = null);

        bool OpenFolder(out string folderPath);

        bool SaveFileDialog(string filter);

    }
}