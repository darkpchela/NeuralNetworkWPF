﻿using System;
using System.Collections.Generic;

namespace NeuralNetwork.Core.Interfaces
{
    public interface INeuralNetworksStorage<T> : IDisposable where T : NeuralNetworkAbstract
    {
        NeuralNetworkStorageConstraints NeuralNetworkStorageConstraints { get; }
        IEnumerable<Guid> GetIds();

        T GetInstance(Guid id);

        IEnumerable<T> GetAllInstances();

        void RemoveInstance(Guid id);

        void AddInstance(T nNetworkInstance);
    }
}