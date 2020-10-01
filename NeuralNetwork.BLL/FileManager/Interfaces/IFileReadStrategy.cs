using System.IO;

namespace NeuralNetwork.Model.FileManager.Interfaces
{
    public interface IFileReadStrategy<T>
    {
        T ReadFile(FileStream fileStream);
    }
}