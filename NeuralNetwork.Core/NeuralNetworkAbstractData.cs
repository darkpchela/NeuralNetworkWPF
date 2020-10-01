using NeuralNetwork.Core.Structs;
using System;

namespace NeuralNetwork.Core
{
    public abstract class NeuralNetworkAbstractData
    {
        public Guid? Id { get; set; }
        public string ActivationFuncName { get; set; }
        public int[] Layers { get; set; }
        public Matrix2D[] Weights { get; set; }
    }
}