using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services.Strategies.Etc;
using NeuralNetwork.Models;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Services.Strategies
{
    public class StorageModelReadStrategy : IFileReadStrategy<NetworksStorageModel>
    {
        public async Task<NetworksStorageModel> ReadFile(string fileName)
        {
            var storageMetaJson = File.ReadAllText(fileName);
            var storageMeta = JsonConvert.DeserializeObject<StorageMetaData>(storageMetaJson);

            var networkReadStrategy = new NetworkDataModelReadStrategy();

            var filesAtNetworksFodler = Directory.GetFiles(storageMeta.NetworksFolder, "*.json");

            NetworksStorageModel storageModel = new NetworksStorageModel(storageMeta.Id)
            {
                Name = storageMeta.Name
            };

            foreach (var item in filesAtNetworksFodler)
            {
                try
                {
                    var network = await networkReadStrategy.ReadFile(item);
                    if (storageMeta.NetworksIds.Contains(network.Id.Value))
                    {
                        storageModel.Networks.Add(new NetworkModel(network));
                    }
                }
                catch
                {
                    continue;
                }
            }

            return storageModel;
        }
    }
}