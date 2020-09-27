namespace NeuralNetwork.Core.Extensions
{
    public struct Matrix2D<T> where T : struct
    {
        private T[,] Values;
        private int Rows;
        private int Columns;

        public Matrix2D(int length1, int length2)
        {
            Values = new T[length1, length2];
            Rows = length1;
            Columns = length2;
        }
    }
}