using NeuralNetwork.Infrastructure.Interfaces;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public Task<bool> DeleteFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<T> ReadFromFileAsync<T>(string fileName, IFileReadStrategy<T> fileReadStrategy)
        {
            T obj = await fileReadStrategy.ReadFile(fileName);
            return obj;
        }

        public async Task<bool> SaveToFileAsync<T>(T obj, string folderPath, IObjectSaveStrategy<T> saveStrategy)
        {
            bool saved = await saveStrategy.SaveToFile(obj, folderPath);
            return saved;
        }
    }
}