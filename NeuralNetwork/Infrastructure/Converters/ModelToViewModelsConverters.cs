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
        public static StoragePreviewVM ToPreviewModel(this NetworksStorageModel model)
        {
            StoragePreviewVM storageVM = new StoragePreviewVM();
            storageVM.Id = model.Id.ToString();
            storageVM.Name = model.Name;

            return storageVM;
        }

        public static IEnumerable<StoragePreviewVM> ToPreviewViewModels(this IEnumerable<NetworksStorageModel> models)
        {
            var previewVMs = new List<StoragePreviewVM>();
            foreach (var m in models)
            {
                previewVMs.Add(m.ToPreviewModel());
            }
            return previewVMs;
        }

        public static NetworkStorageVM ToViewModel(this NetworksStorageModel model)
        {
            NetworkStorageVM storageVM = new NetworkStorageVM
            {
                Id = model.Id.ToString(),
                Name = model.Name,
                Networks = model.GetAllInstances().ToViewModels(),
            };

            return storageVM;
        }

        public static IEnumerable<NetworkStorageVM> ToViewModels(this IEnumerable<NetworksStorageModel> models)
        {
            var storageVMs = new List<NetworkStorageVM>();
            foreach (var m in models)
            {
                storageVMs.Add(m.ToViewModel());
            }
            return storageVMs;
        }

        public static NetworkInfoVM ToViewModel(this NetworkModel model)
        {
            NetworkInfoVM networkVM = new NetworkInfoVM
            {
                Id = model.Id.ToString(),
                Layers = new ObservableCollection<NetworkLayerVM>(model.Layers.ToLayerViewModels()),
                CurrentFunc = FuncDictionary.GetFuncName(model.ActivationFunc),
                LearningRate = model.LearningRate,
                Name = model.Name
            };
            return networkVM;
        }

        public static IEnumerable<NetworkInfoVM> ToViewModels(this IEnumerable<NetworkModel> models)
        {
            var networksVM = new List<NetworkInfoVM>();

            foreach (var m in models)
            {
                networksVM.Add(m.ToViewModel());
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