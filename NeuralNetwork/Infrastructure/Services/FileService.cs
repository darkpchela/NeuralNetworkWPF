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

        public Task<T> ReadFromFile<T>(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveToFileAsync<T>(T obj, string folderPath, IObjectSaveStrategy<T> saveStrategy)
        {
            bool saved = await saveStrategy.SaveToFile(obj, folderPath);
            return saved;
        }
    }
}