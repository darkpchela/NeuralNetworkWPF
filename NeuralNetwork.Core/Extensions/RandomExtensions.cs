using System;

namespace NeuralNetwork.Core.Etc
{
    public static class RandomExtensions
    {
        public static double NextDouble(this Random random, double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }
}