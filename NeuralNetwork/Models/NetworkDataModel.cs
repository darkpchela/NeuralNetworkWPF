using NeuralNetwork.Core.Default;
using System;

namespace NeuralNetwork.Models
{
    public class NetworkDataModel : NeuralNetworkDefaultData
    {
        public int Generation { get; set; }
        public string Name { get; set; }
        public Guid StorageId { get; set; }
    }
}