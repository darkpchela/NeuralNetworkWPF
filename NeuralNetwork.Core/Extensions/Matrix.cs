using System;

namespace NeuralNetwork.Core.Extensions
{
    public struct Matrix2D
    {
        private float[,] Matrix;
        private int Rows;
        private int Columns;

        public float this[int row, int column]
        {
            get
            {
                return Matrix[row, column];
            }
            private set
            {
                Matrix[row, column] = value;
            }
        }

        public static Matrix2D operator +(Matrix2D matrix1, Matrix2D matrix2)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
                throw new ArgumentException();

            Matrix2D resultMatrix = new Matrix2D(matrix1.Rows, matrix1.Columns);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }

            return resultMatrix;
        }
        public static Matrix2D operator +(Matrix2D matrix1, float number)
        {
            Matrix2D resultMatrix = new Matrix2D(matrix1.Rows, matrix1.Columns);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = matrix1[i, j] + number;
                }
            }

            return resultMatrix;
        }
        public static Matrix2D operator -(Matrix2D matrix1, Matrix2D matrix2)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
                throw new ArgumentException();

            Matrix2D resultMatrix = new Matrix2D(matrix1.Rows, matrix1.Columns);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }

            return resultMatrix;
        }
        public static Matrix2D operator -(Matrix2D matrix1, float number)
        {
            Matrix2D resultMatrix = new Matrix2D(matrix1.Rows, matrix1.Columns);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = matrix1[i, j] - number;
                }
            }

            return resultMatrix;
        }
        public static Matrix2D operator *(Matrix2D matrix1, Matrix2D matrix2)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
                throw new ArgumentException();

            Matrix2D resultMatrix = new Matrix2D(matrix1.Rows, matrix1.Columns);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = matrix1[i, j] * matrix2[i, j];
                }
            }

            return resultMatrix;

        }
        public static Matrix2D ForEach(Matrix2D matrix, Func<float, float> func) //???
        {
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix[i, j] = func(matrix[i, j]);
                }
            }

            return matrix;
        }
        public static Matrix2D ScalerProduct(Matrix2D matrix1, Matrix2D matrix2)
        {
            if (matrix1.Rows != matrix2.Columns && matrix1.Columns != matrix2.Rows)
                throw new ArithmeticException("Matrixes can not be multiplied - different amount of rows and columns");

            Matrix2D resultMatrix = new Matrix2D(matrix1.Rows, matrix2.Columns);

            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0; j < matrix2.Columns; j++)
                {
                    for (int k = 0; k < matrix2.Rows; k++)
                    {
                        resultMatrix[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }

            return resultMatrix;
            
        }
        public Matrix2D(int rows, int columns)
        {
            Matrix = new float[rows, columns];
            Rows = rows;
            Columns = columns;
        }
        public Matrix2D(float[,] array)
        {
            Matrix = (float[,])array.Clone();
            Rows = Matrix.GetLength(0);
            Columns = Matrix.GetLength(1);
        }
        public Matrix2D Transpose()
        {
            Matrix2D resultMatrix = new Matrix2D(Columns, Rows);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[j, i] = Matrix[i, j];
                }
            }

            return resultMatrix;
        }
    }
}