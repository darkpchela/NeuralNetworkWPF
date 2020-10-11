using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Interfaces
{
    public interface IObjectSaveStrategy<T>
    {
        Task<bool> SaveToFile(T obj, string folderPath);
    }
}