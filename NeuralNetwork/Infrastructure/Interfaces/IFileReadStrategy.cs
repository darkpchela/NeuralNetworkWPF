using System.IO;
using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Interfaces
{
    public interface IFileReadStrategy<T>
    {
        Task<T> ReadFile(string fileName);
    }
}