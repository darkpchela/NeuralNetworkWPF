#define GPU_ON
using System;
using System.Text;
using System.Threading.Tasks;


namespace NeuralNetwork.Core.Structs
{
    public struct Matrix2D
    {
        public float[,] Matrix { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public float this[int row, int column]
        {
            get
            {
                return Matrix[row, column];
            }
            set
            {
                Matrix[row, column] = value;
            }
        }

#if !GPU_ON
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

        public static Matrix2D operator +(float number, Matrix2D matrix)
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
            if (matrix1.Rows != matrix2.Rows && matrix1.Columns != matrix2.Columns)
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


        public static Matrix2D operator /(Matrix2D matrix, float number)
        {
            Matrix2D resultMatrix = new Matrix2D(matrix.Rows, matrix.Columns);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = matrix[i, j] / number;
                }
            }

            return resultMatrix;
        }

        public static Matrix2D operator /(float number, Matrix2D matrix)
        {
            Matrix2D resultMatrix = new Matrix2D(matrix.Rows, matrix.Columns);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] =  number / matrix[i, j];
                }
            }

            return resultMatrix;
        }
        
        public static Matrix2D operator /(Matrix2D matrix1, Matrix2D matrix2)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
                throw new ArgumentException();

            Matrix2D resultMatrix = new Matrix2D(matrix1.Rows, matrix1.Columns);

            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = matrix1[i, j] / matrix2[i, j];
                }
            }

            return resultMatrix;
        }

        public static Matrix2D ScalerProduct(Matrix2D matrix1, Matrix2D matrix2)
        {
            if (matrix1.Columns != matrix2.Rows)
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

#else
         public static Matrix2D operator +(Matrix2D matrix1, Matrix2D matrix2)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
                throw new ArgumentException();

            float[,] resultMatrix = new float[matrix1.Rows, matrix1.Columns];
            GPU.matrixAddMatrix(matrix1.Matrix, matrix2.Matrix, resultMatrix, matrix1.Rows, matrix1.Columns);

            return new Matrix2D(resultMatrix);
        }

        public static Matrix2D operator +(Matrix2D matrix, float number)
        {
            float[,] resultMatrix = new float[matrix.Rows, matrix.Columns];
            GPU.matrixAddNum(matrix.Matrix, number, resultMatrix, matrix.Rows, matrix.Columns);

            return new Matrix2D(resultMatrix);
        }

        public static Matrix2D operator +(float number, Matrix2D matrix)
        {

            return matrix + number;
        }


        public static Matrix2D operator -(Matrix2D matrix1, Matrix2D matrix2)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
                throw new ArgumentException();

            float[,] resultMatrix = new float[matrix1.Rows, matrix1.Columns];
            GPU.matrixSubMatrix(matrix1.Matrix, matrix2.Matrix, resultMatrix, matrix1.Rows, matrix1.Columns);


            return new Matrix2D(resultMatrix);
        }

        public static Matrix2D operator -(Matrix2D matrix, float number)
        {
            float[,] resultMatrix = new float[matrix.Rows, matrix.Columns];

            GPU.matrixAddNum(matrix.Matrix, -number, resultMatrix, matrix.Rows, matrix.Columns);

            return new Matrix2D(resultMatrix);
        }

        public static Matrix2D operator -(float number, Matrix2D matrix1)
        {
            float[,] resultMatrix = new float[matrix1.Rows, matrix1.Columns];
            matrix1 = matrix1.ForEach(x => x = -x);
            GPU.matrixAddNum(matrix1.Matrix, number, resultMatrix, matrix1.Rows, matrix1.Columns);

            return new Matrix2D(resultMatrix);
        }

        public static Matrix2D operator *(Matrix2D matrix1, Matrix2D matrix2)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
                throw new ArgumentException();

            float[,] resultMatrix = new float[matrix1.Rows, matrix1.Columns];
            GPU.matrixMultMatrix(matrix1.Matrix, matrix2.Matrix, resultMatrix, matrix1.Rows, matrix1.Columns);

            return new Matrix2D(resultMatrix);
        }


        public static Matrix2D operator *(Matrix2D matrix, float number)
        {
            float[,] resultMatrix = new float[matrix.Rows, matrix.Columns];

            GPU.matrixMultNum(matrix.Matrix, number, resultMatrix, matrix.Rows, matrix.Columns);

            return new Matrix2D(resultMatrix);
        }

        public static Matrix2D operator *(float number, Matrix2D matrix)
        {
            return matrix * number;
        }


        public static Matrix2D operator /(Matrix2D matrix, float number)
        {
            float[,] resultMatrix = new float[matrix.Rows, matrix.Columns];

            GPU.matrixDivNum(matrix.Matrix, number, resultMatrix, matrix.Rows, matrix.Columns);

            return new Matrix2D(resultMatrix);
        }

        public static Matrix2D operator /(float number, Matrix2D matrix)
        {
            float[,] resultMatrix = new float[matrix.Rows, matrix.Columns];
            matrix = matrix.ForEach(x => x = (float)Math.Pow(x, -1));
            GPU.matrixMultNum(matrix.Matrix, number, resultMatrix, matrix.Rows, matrix.Columns);

            return new Matrix2D(resultMatrix);
        }
        
        public static Matrix2D operator /(Matrix2D matrix1, Matrix2D matrix2)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
                throw new ArgumentException();

            float[,] resultMatrix = new float[matrix1.Rows, matrix1.Columns];
            GPU.matrixDivMatrix(matrix1.Matrix, matrix2.Matrix, resultMatrix, matrix1.Rows, matrix1.Columns);

            return new Matrix2D(resultMatrix);
        }


        public static Matrix2D ScalerProduct(Matrix2D matrix1, Matrix2D matrix2)
        {
            if (matrix1.Columns != matrix2.Rows)
                throw new ArithmeticException("Matrixes can not be multiplied - different amount of rows and columns");

            float[,] resultMatrix = new float[matrix1.Rows, matrix2.Columns];
            GPU.matrixScalerProduct(matrix1.Matrix, matrix2.Matrix, resultMatrix, matrix1.Rows, matrix2.Columns, matrix1.Columns);

            return new Matrix2D(resultMatrix);
        }
#endif
        public Matrix2D(int rows, int columns)
        {
            Matrix = new float[rows, columns];
            Rows = rows;
            Columns = columns;
        }

        public Matrix2D(int rows, int columns, float number)
        {
            Matrix = new float[rows, columns];
            Rows = rows;
            Columns = columns;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Matrix[i, j] = number;
                }
            }
        }

        public Matrix2D(float[,] array)
        {
            Matrix = (float[,])array.Clone();
            Rows = Matrix.GetLength(0);
            Columns = Matrix.GetLength(1);
        }

        public Matrix2D ForEach(Func<float, float> func)
        {
            Matrix2D resultMatrix = new Matrix2D(Rows, Columns);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    resultMatrix[i, j] = func(Matrix[i, j]);
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
                    resultMatrix[i, j] = Matrix[j, i];
                }
            }

            return resultMatrix;
        }

        public float GetMin()
        {
            float minValue = Matrix[0, 0];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (minValue > Matrix[i, j])
                        minValue = Matrix[i, j];
                }
            }

            return minValue;
        }

        public float GetMax()
        {
            float maxValue = Matrix[0, 0];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (maxValue < Matrix[i, j])
                        maxValue = Matrix[i, j];
                }
            }

            return maxValue;
        }

        public float[] ToSingleArray()
        {
            float[] resultArray = new float[Matrix.Length];

            int n = 0;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    resultArray[n] = Matrix[i, j];
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
                    stringBuilder.AppendFormat("{0,13}", Matrix[i, j]);
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}