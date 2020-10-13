using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services.Strategies.Etc;
using NeuralNetwork.Models;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Services.Strategies
{
    public class StorageModelSaveStrategy : IObjectSaveStrategy<NetworksStorageModel>
    {
        public Task<bool> SaveToFile(NetworksStorageModel obj, string folderPath)
        {
            try
            {
                string storageFolder = Path.Combine(folderPath, obj.Name.ToString());
                string networksFolder = Path.Combine(storageFolder, "Networks");
                string metaFileName = Path.Combine(storageFolder, "meta.json");

                if (!Directory.Exists(storageFolder))
                {
                    Directory.CreateDirectory(storageFolder);
                    Directory.CreateDirectory(networksFolder);
                }

                StorageMetaData storageMeta = new StorageMetaData
                {
                    Id = obj.Id,
                    NetworksIds = obj.Networks.Select(n => n.Id).ToArray(),
                    NetworksFolder = networksFolder,
                    Name = obj.Name
                };

                var metaJson = JsonConvert.SerializeObject(storageMeta);
                File.WriteAllText(metaFileName, metaJson);

                var networkSaveStrategy = new NetworkDataModelSaveStrategy();
                foreach (var n in obj.Networks)
                {
                    networkSaveStrategy.SaveToFile(n.GetNetworkData(), networksFolder);
                }

                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}