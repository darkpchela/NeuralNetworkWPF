using NeuralNetwork.Core.Etc;
using NeuralNetwork.Core.Structs;
using System;

namespace NeuralNetwork.Core
{
    public class NeuralNetworkData
    {
        public Guid Id { get; }
        public string ActivationFuncName { get; }
        public int[] Layers { get; }
        public Matrix2D[] Weights { get; }

        public NeuralNetworkData(NeuralNetworkAbstract nrlNet)
        {
            this.Id = nrlNet.Id;
            this.ActivationFuncName = FuncDictionary.GetFuncName(nrlNet.ActivationFunc) ?? "Unknown function";
            this.Layers = nrlNet.Layers;
            this.Weights = nrlNet.Weigths;
        }
    }
}