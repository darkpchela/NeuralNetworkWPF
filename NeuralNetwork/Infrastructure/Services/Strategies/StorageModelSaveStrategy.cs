using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Models;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Services.Strategies
{
    public class StorageModelSaveStrategy : IObjectSaveStrategy<NetworksStorageModel>
    {
        public Task<bool> SaveToFile(NetworksStorageModel obj, string folderPath)
        {
            try
            {
                string storageFolder = Path.Combine(folderPath, obj.Id.ToString());

                if (!Directory.Exists(storageFolder))
                    Directory.CreateDirectory(storageFolder);

                var metaJson = JsonConvert.SerializeObject(obj);
                var metaFileName = Path.Combine(storageFolder, "Storage", ".json");
                File.WriteAllText(metaFileName, metaJson);

                foreach (var n in obj.Networks)
                {
                    var networkJson = JsonConvert.SerializeObject(n);
                    var networkFileName = Path.Combine(storageFolder, n.Id.ToString(), ".json");
                    File.WriteAllText(networkFileName, networkJson);
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