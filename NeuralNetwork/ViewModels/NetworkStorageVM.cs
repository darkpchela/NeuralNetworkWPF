using System;
using System.Collections.Generic;

namespace NeuralNetwork.ViewModels
{
    public class NetworkStorageVM
    {
        public Guid Id { get; set; }

        public int NetworksCount { get; set; }

        public int InputsCount { get; set; }

        public int OutputsCount { get; set; }

        public IEnumerable<NetworkVM> Networks { get; set; }
    }
}