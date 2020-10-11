using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Interfaces
{
    public interface IFileService
    {
        Task<T> ReadFromFile<T>(string fileName);

        Task<bool> DeleteFile(string fileName);

        Task<bool> SaveToFileAsync<T>(T obj, string folderPath, IObjectSaveStrategy<T> saveStrategy);
    }
}