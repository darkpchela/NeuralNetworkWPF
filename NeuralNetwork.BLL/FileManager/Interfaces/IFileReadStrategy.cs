using System.IO;

namespace NeuralNetwork.Services.FileManager.Interfaces
{
    public interface IFileReadStrategy<T>
    {
        T ReadFile(FileStream fileStream);
    }
}