namespace NeuralNetwork.Model.FileManager.Interfaces
{
    public interface IFileReader<T>
    {
        IFileReadStrategy<T> fileReadStrategy { get; set; }

        T ReadFile(string FileName);
    }
}