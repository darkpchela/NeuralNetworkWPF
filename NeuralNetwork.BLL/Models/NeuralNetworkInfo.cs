using NeuralNetwork.Core;
using NeuralNetwork.Core.Extensions;
using NeuralNetwork.Core.Structs;
using System;

namespace NeuralNetwork.Services.Models
{
    public class NeuralNetworkDataDTO
    {
        public Guid Id { get; }

        public string ActivationFuncName { get; }

        public int[] Layers { get; }

        public float[][,] Weigths { get; }

        public virtual T ToNeuralNetworkData<T>() where T : NeuralNetworkAbstractData, new()
        {
            T nNetData = new T();
            nNetData.ActivationFuncName = ActivationFuncName;
            nNetData.Id = Id;
            nNetData.Layers = Layers;

            nNetData.Weights = new Matrix2D[Weigths.Length];

            for (int i = 0; i < Weigths.Length; i++)
            {
                nNetData.Weights[i] = Weigths[i].ToMatrix2D();
            }

            return nNetData;
        }
    }
}