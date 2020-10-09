using NeuralNetwork.Core.Etc;
using NeuralNetwork.Models;
using NeuralNetwork.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NeuralNetwork.Infrastructure.Converters
{
    public static class ModelToViewModelsConverters
    {
        public static IEnumerable<NetworkStorageVM> ToViewModels(this IEnumerable<NetworksStorageModel> models)
        {
            var storageVMs = new List<NetworkStorageVM>();
            foreach (var m in models)
            {
                storageVMs.Add(m.GetViewModel());
            }
            return storageVMs;
        }

        public static IEnumerable<NetworkVM> ToViewModels(this IEnumerable<NetworkModel> models)
        {
            var networksVM = new List<NetworkVM>();

            foreach (var m in models)
            {
                networksVM.Add(m.GetViewModel());
            }

            return networksVM;
        }

        public static IEnumerable<NetworkLayerVM> ToLayerViewModels(this IEnumerable<int> layers)
        {
            var networkLayerVMs = new List<NetworkLayerVM>();
            for (int i = 0; i < layers.Count(); i++)
            {
                NetworkLayerVM layerVM = new NetworkLayerVM
                {
                    LayerIndex = i,
                    NeuronsCount = layers.ElementAt(i)
                };

                networkLayerVMs.Add(layerVM);
            }

            networkLayerVMs.Last().IsOutputLayer = true;

            return networkLayerVMs;
        }
    }
}