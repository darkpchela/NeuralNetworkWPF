namespace NeuralNetwork.Core.Extensions
{
    internal static class ArrayExtensions
    {
        public static T[] ConvertToSingleArray<T>(this T[,] array)
        {
            T[] resultArray = new T[array.Length];
            int n = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    resultArray[n] = array[i, j];
                    n++;
                }
            }

            return resultArray;
        }
        public static Matrix2D ToMatrix2D(this float[,] array)
        {
            return new Matrix2D(array);
        }
        public static Matrix2D ToMatrix2D(this float[] array)
        {
            float[,] resArray = new float[1,array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                resArray[0, i] = array[i];
            }

            return new Matrix2D(resArray);
        }
    }
}