using NeuralNetwork.Core;
using System;

namespace NeuralNetwork.BLL.NeuralNetwork
{
    public class NeuralNetworkInstanceData : NrlNetData
    {
        private string Name { get; set; }
        private Guid Id { get; set; }

        public NeuralNetworkInstanceData(INeuralNetworkInstanse neuralNetworkInstanse) : base(neuralNetworkInstanse)
        {
            this.Id = neuralNetworkInstanse.Id;
            this.Name = neuralNetworkInstanse.Name;
        }
    }
}