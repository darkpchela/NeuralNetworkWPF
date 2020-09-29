using System.IO;

namespace NeuralNetwork.BLL.FileManager.Interfaces
{
    public interface IFileReadStrategy<T>
    {
        T ReadFile(FileStream fileStream);
    }
}