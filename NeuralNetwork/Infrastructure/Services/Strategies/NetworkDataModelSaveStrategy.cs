using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Services.Strategies
{
    public class NetworkDataModelSaveStrategy : IObjectSaveStrategy<NetworkDataModel>
    {
        public Task<bool> SaveToFile(NetworkDataModel obj, string folderPath)
        {
            try
            {
                var objJson = JsonConvert.SerializeObject(obj);
                var fileName = Path.Combine(folderPath, obj.Name.ToString() + ".json");
                File.WriteAllText(fileName, objJson);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }
    }
}