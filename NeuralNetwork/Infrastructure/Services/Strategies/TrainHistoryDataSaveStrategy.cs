using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services.Strategies.Etc;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Services.Strategies
{
    public class TrainHistoryDataSaveStrategy : IObjectSaveStrategy<TrainHisoryData>
    {
        public Task<bool> SaveToFile(TrainHisoryData obj, string folderPath)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(obj);
                string historyFolderPath = Path.Combine(folderPath, obj.NetworkData.Name);
                string fileName = Path.Combine(historyFolderPath, obj.Generation + ".json");

                if (!Directory.Exists(historyFolderPath))
                    Directory.CreateDirectory(historyFolderPath);

                File.WriteAllText(fileName, jsonData);

                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}