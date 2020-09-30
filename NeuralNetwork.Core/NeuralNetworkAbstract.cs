using NeuralNetwork.Core.Interfaces;
using NeuralNetwork.Core.Structs;
using System;

namespace NeuralNetwork.Core
{
    public abstract class NeuralNetworkAbstract : INeuralNetwork, IDisposable
    {
        public Guid Id { get; protected set; }

        public Func<float, float> ActivationFunc { get; set; }

        public int[] Layers { get; set; }

        public Matrix2D[] Weigths { get; set; }

        public float LearningRate { get; set; }

        abstract public void Train(float[] inputValues, float[] targetValues);

        abstract public float[] Query(float[] inputValues);

        abstract protected void InitStartWeigths();

        #region Disposable

        protected bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
            }
        }

        ~NeuralNetworkAbstract()
        {
            Dispose(false);
        }

        #endregion Disposable
    }
}