using NeuralNetwork.Core.Default;
using System.Collections.Generic;

namespace NeuralNetwork.Model.NeuralNetworkWorkshopModel
{
    internal class NeuralNetworkPrototype
    {
        private string ActivationFuncName { get; set; }

        private List<int> Layers { get; set; }

        private float LearningRate { get; set; }

        internal NeuralNetworkDefaultData ToNeuralNetworkDefaultData()
        {
            var defData = new NeuralNetworkDefaultData();
            defData.ActivationFuncName = ActivationFuncName;
            defData.LearningRate = LearningRate;
            defData.Layers = Layers.ToArray();

            return defData;
        }
    }
}