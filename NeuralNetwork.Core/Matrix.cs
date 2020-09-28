using System;
using System.Text;

namespace NeuralNetwork.Core
{
    public struct Matrix2D
    {
        public float[,] Array { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public float this[int row, int column]
        {
            get
            {
                return Array[row, column];
            }
            set
            {
                Array[row, column] = value;
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

        public static Matrix2D operator +(Matrix2D matrix, float number)
        {
            Matrix2D resultMatrix = new Matrix2D(matrix.Rows, matrix.Columns);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = matrix[i, j] + number;
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

        public static Matrix2D operator -(Matrix2D matrix, float number)
        {
            Matrix2D resultMatrix = new Matrix2D(matrix.Rows, matrix.Columns);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = matrix[i, j] - number;
                }
            }

            return resultMatrix;
        }

        public static Matrix2D operator -(float number, Matrix2D matrix1)
        {
            Matrix2D resultMatrix = new Matrix2D(matrix1.Rows, matrix1.Columns);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = number - matrix1[i, j];
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

        public static Matrix2D operator *(float number, Matrix2D matrix)
        {
            Matrix2D resultMatrix = new Matrix2D(matrix.Rows, matrix.Columns);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = matrix[i, j] * number;
                }
            }

            return resultMatrix;
        }

        public static Matrix2D operator *(Matrix2D matrix, float number)
        {
            Matrix2D resultMatrix = new Matrix2D(matrix.Rows, matrix.Columns);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = matrix[i, j] * number;
                }
            }

            return resultMatrix;
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
            Array = new float[rows, columns];
            Rows = rows;
            Columns = columns;
        }

        public Matrix2D(float[,] array)
        {
            Array = (float[,])array.Clone();
            Rows = Array.GetLength(0);
            Columns = Array.GetLength(1);
        }

        public Matrix2D ForEach(Func<float, float> func)
        {
            Matrix2D resultMatrix = new Matrix2D(Rows, Columns);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    resultMatrix[i, j] = func(Array[i, j]);
                }
            }

            return resultMatrix;
        }

        public Matrix2D Transpose()
        {
            Matrix2D resultMatrix = new Matrix2D(Columns, Rows);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = Array[j, i];
                }
            }

            return resultMatrix;
        }

        public float[] ToSingleArray()
        {
            float[] resultArray = new float[Array.Length];

            int n = 0;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    resultArray[n] = Array[i, j];
                    n++;
                }
            }

            return resultArray;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    stringBuilder.AppendFormat("{0,13}", Array[i, j]);
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}