namespace NeuralNetwork.Infrastructure.Services
{
    public interface IFileDialogService
    {
        string FileName { get; set; }

        void ShowMessage(string message);

        bool OpenFileDialog(out string fileName);

        bool OpenFileDialog(out string[] fileNames);

        bool SaveFileDialog();
    }
}