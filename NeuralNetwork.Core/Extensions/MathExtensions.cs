using System;

namespace NeuralNetwork.Core.Extensions
{
    public static class MathExtensions
    {
        public static float Sigmoid(float input)
        {
            return (float)(1 / (1 + Math.Pow(Math.E, -input)));
        }

        public static float[,] MatrixMultiply(float[,] matrix1, float[,] matrix2)
        {
            if (matrix1.GetLength(1)!= matrix2.GetLength(0))
                throw new ArithmeticException("Invalid matrixes");

            int rows = matrix1.GetLength(0);
            int columns = matrix2.GetLength(1);

            int iterations = matrix2.GetLength(0);

            float[,] resultMatrix = new float[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    for (int k = 0; k < iterations; k++)
                    {
                        resultMatrix[i, j] += matrix1[i, k] * matrix2[k, j]; 
                    }
                }
            }

            return resultMatrix;
        }

        public static float[,] MatrixTranspose(float[,] matrix)
        {
            int inputRows = matrix.GetLength(0);
            int inputColumns = matrix.GetLength(1);

            float[,] resultMatrix = new float[inputColumns, inputRows];

            for (int i = 0; i < inputRows; i++)
            {
                for (int j = 0; j < inputColumns; j++)
                {
                    resultMatrix[j, i] = matrix[i, j];
                }
            }

            return resultMatrix;
        }
    }
}