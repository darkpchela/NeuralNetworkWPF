﻿using System;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INeuralNetworkMaster : IDisposable
    {
        INeuralNetworksStorage NetworksStorage { get; set; }

        float[] QueryNetwork(float[] inputs, Guid networkId);

        float[][] QueryAll(float[] inputs);

        void TrainNetwork(float[] inputs, float[] targets, Guid networkId);

        void TrainAll(float[] inputs, float[] targets);
    }
}