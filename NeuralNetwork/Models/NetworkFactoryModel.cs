using NeuralNetwork.Core;
using NeuralNetwork.Core.Etc;
using System;

namespace NeuralNetwork.Models
{
    public class NetworkFactoryModel : NeuralNetworkAbstractFactory<NetworkModel, NetworkDataModel>
    {
        private NetworkDataModel _prototypeData = new NetworkDataModel
        {
            ActivationFuncName = FuncDictionary.GetFuncName(MathFuncs.Sigmoid),
            Layers = new int[]{2 , 2},
            LearningRate = 0.05f
        };

        public override NetworkModel CreateInstance(NetworkDataModel nNetData)
        {
            var model = new NetworkModel(nNetData);
            return model;
        }

        public NetworkModel CreatePrototype()
        {
            _prototypeData.Id = Guid.NewGuid();
            return new NetworkModel(_prototypeData);
        }
    }
}