using NeuralNetwork.Core.ActivationFuncs;
using NeuralNetwork.Core.Extensions;
using System;

namespace NeuralNetwork.Core
{
    public static class Programm
    {
        static float[,] TM1 = { { 0.33f, 0.12f, 1.8f } };
        static float[,] TM2 = { { 7, 8, 9, 10, 11 }, { 12, 13, 14, 15, 16 }, { 17, 18, 19, 20, 21 } };
        private static void Main(string[] args)
        {
            NrlNet nrlNet = new NrlNet("FirstTest", new int[] { 3, 5, 3 }, new SigmoidFunc());
            var outputs = nrlNet.Query(TM1.ConvertToSingleArray());
            foreach (var o in nrlNet._QueryHiddenOutputs)
            {
                Console.WriteLine("_________________________________");
                WriteMatrix(o);
            }
            for (int i = 0; i < nrlNet.Layers.Length; i++)
            {
                var previousoutputs = MathFuncs.MatrixTranspose(outputs);
                MathFuncs.MatrixForEach<float>(ref previousoutputs, MathFuncs.SigmoidReverse);
                Console.WriteLine("_________________________________");
                WriteMatrix(previousoutputs);
            }
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