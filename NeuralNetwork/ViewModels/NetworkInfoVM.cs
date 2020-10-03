using System;
using System.Collections.Generic;

namespace NeuralNetwork.ViewModels
{
    public class NetworkInfoVM
    {
        public Guid Id { get; set; }

        public string ActivationFuncName { get; set; }

        public int LayersCount { get; set; }

        public int InputsCount { get; set; }

        public int OutputsCount { get; set; }

        public IEnumerable<NetworkLayerVM> Layers { get; set; }

        public IEnumerable<float[,]> Weigths { get; set; }
    }
}