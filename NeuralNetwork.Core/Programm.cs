using NeuralNetwork.Core.ActivationFuncs;
using NeuralNetwork.Core.Extensions;
using System;

namespace NeuralNetwork.Core
{
    public static class Programm
    {
        static float[] TM1 = { 0.33f, 0.12f, 1.8f };
        static float[,] TM2 = { { 7, 8, 9, 10, 11 }, { 12, 13, 14, 15, 16 }, { 17, 18, 19, 20, 21 } };
        private static void Main(string[] args)
        {
            NrlNet nrlNet = new NrlNet("FirstTest", new int[] { 3, 5, 3 }, new SigmoidFunc());
            //var outputs = nrlNet.Query(TM1);
            Console.WriteLine(TM2.ToMatrix2D());
            Console.WriteLine(TM2.ToMatrix2D().Transpose()); ;
        }
    }
}