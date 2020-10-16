using NeuralNetwork.Core.Default;
using NeuralNetwork.Core.Etc;
using NeuralNetwork.Infrastructure.Converters;
using NeuralNetwork.ViewModels;
using System.Collections.ObjectModel;

namespace NeuralNetwork.Models
{
    public class NetworkModel : NeuralNetworkDefault
    {
        public string Name { get; set; }

        public NetworkModel(NetworkDataModel networkDataModel) : base(networkDataModel)
        {
            Name = networkDataModel.Name;
        }

        public void Train(QueryDataModel queryDataModel)
        {
            base.Train(queryDataModel.InputValues, queryDataModel.OutputValues);
        }

        public NetworkVM GetViewModel()
        {
            NetworkVM networkVM = new NetworkVM(this)
            {
                Id = Id.ToString(),
                Layers = new ObservableCollection<NetworkLayerVM>(Layers.ToLayerViewModels()),
                CurrentFunc = FuncDictionary.GetFuncName(ActivationFunc),
                LearningRate = LearningRate,
                Name = Name,
                IsPrototype = false
            };
            return networkVM;
        }

        public NetworkDataModel GetNetworkData()
        {
            var data = new NetworkDataModel
            {
                ActivationFuncName = FuncDictionary.GetFuncName(ActivationFunc),
                Id = Id,
                Layers = Layers,
                LearningRate = LearningRate,
                Name = Name,
                Weights = Weigths
            };

            return data;
        }
    }
}