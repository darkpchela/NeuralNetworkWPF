namespace NeuralNetwork.Infrastructure.Interfaces
{
    public interface IFileDialogService
    {
        void ShowMessage(string message);

        bool OpenFileDialog(out string fileName);

        bool OpenFileDialog(out string[] fileNames);

        bool SaveFileDialog();
    }
}