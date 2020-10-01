using NeuralNetwork.Core.Structs;

namespace NeuralNetwork.Core.Extensions
{
    public static class ArrayExtensions
    {
        public static Matrix2D ToMatrix2D(this float[,] array)
        {
            return new Matrix2D(array);
        }

        public static Matrix2D ToMatrix2D(this float[] array)
        {
            float[,] resArray = new float[1, array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                resArray[0, i] = array[i];
            }

            return new Matrix2D(resArray);
        }
    }
}