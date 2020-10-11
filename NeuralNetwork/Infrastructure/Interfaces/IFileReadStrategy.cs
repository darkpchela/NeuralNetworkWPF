using System.IO;

namespace NeuralNetwork.Infrastructure.Interfaces
{
    public interface IFileReadStrategy<T>
    {
        T ReadFile(FileStream fileStream);
    }
}