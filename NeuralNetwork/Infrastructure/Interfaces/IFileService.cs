using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Interfaces
{
    public interface IFileService
    {
        Task<T> ReadFromFileAsync<T>(string fileName, IFileReadStrategy<T> readStrategy);

        Task<bool> DeleteFile(string fileName);

        Task<bool> SaveToFileAsync<T>(T obj, string folderPath, IObjectSaveStrategy<T> saveStrategy);
    }
}