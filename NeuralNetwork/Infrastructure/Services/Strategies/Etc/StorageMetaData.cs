using System;

namespace NeuralNetwork.Infrastructure.Services.Strategies.Etc
{
    public class StorageMetaData
    {
        public Guid Id { get; set; }

        public Guid[] NetworksIds { get; set; }

        public string NetworksFolder { get; set; }
    }
}