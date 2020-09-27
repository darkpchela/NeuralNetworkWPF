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
    }
}