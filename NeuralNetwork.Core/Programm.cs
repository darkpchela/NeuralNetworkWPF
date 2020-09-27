﻿using NeuralNetwork.Core.ActivationFuncs;
using NeuralNetwork.Core.Extensions;
using System;

namespace NeuralNetwork.Core
{
    public static class Programm
    {
        static float[,] TM1 = { { 1, 2, 3 } };
        static float[,] TM2 = { { 7, 8, 9, 10, 11 }, { 12, 13, 14, 15, 16 }, { 17, 18, 19, 20, 21 } };
        private static void Main(string[] args)
        {
            NrlNet nrlNet = new NrlNet("FirstTest", new int[] { 3, 5, 3 }, new SigmoidFunc());
            var answer = nrlNet.Query(new float[] { 1.0f, 0.5f, 0.3f });
            foreach (var a in answer)
            {
                Console.WriteLine(a);
            }
            //for (int i = 0; i < nrlNet.Layers.Length - 1; i++)
            //{
            //    Console.WriteLine($"Layer {i + 1} --> {i + 2}: ");
            //    WriteMatrix<float>(nrlNet.Weights[i]);
            //    Console.WriteLine("-----------------------------------------------------------------------------");
            //}
        }

        private static void WriteMatrix<T>(T[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(String.Format("{0,13}", matrix[i, j]));
                }
                Console.WriteLine();
            }
        }
    }
}