using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NeuralNetwork.Infrastructure.Services.FileManager
{
    public class NetworkDataModelSaveStrategy : IObjectSaveStrategy<NetworkDataModel>
    {
        public async Task<bool> SaveToFile(NetworkDataModel obj, string folderPath)
        {
            try
            {
                var objJson = JsonConvert.SerializeObject(obj);
                File.WriteAllText(folderPath + obj.Id.ToString()+".json", objJson);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}