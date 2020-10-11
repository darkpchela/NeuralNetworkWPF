using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Models;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Services.Strategies
{
    public class NetworkDataModelReadStrategy : IFileReadStrategy<NetworkDataModel>
    {
        public Task<NetworkDataModel> ReadFile(string fileName)
        {
            var objJson = File.ReadAllText(fileName);
            var obj = JsonConvert.DeserializeObject<NetworkDataModel>(objJson);
            return Task.FromResult(obj);
        }
    }
}