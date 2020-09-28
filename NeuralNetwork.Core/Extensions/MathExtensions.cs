using System;

namespace NeuralNetwork.Core.Extensions
{
    public static class MathFuncs
    {
        public static float Sigmoid(float x)
        {
            return (float)(1 / (1 + Math.Pow(Math.E, -x)));
        }

        public static float SigmoidReverse(float y)
        {
            return (float)Math.Log((y / (1 - y)), Math.E);
        }
    }
}