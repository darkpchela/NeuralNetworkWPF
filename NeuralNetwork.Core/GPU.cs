using System.Runtime.InteropServices;

namespace NeuralNetwork.Core
{
    public static class GPU
    {
        [DllImport("GPUMath.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void matrixAddMatrix([In] float[,] matrixA, [In] float[,] matrixB, [Out] float[,] resultMatrix, int rowsCount, int columnsCount);

        [DllImport("GPUMath.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void matrixAddNum([In] float[,] matrix, [In] float number, [Out] float[,] resultMatrix, int rowsCount, int columnsCount);

        [DllImport("GPUMath.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void matrixSubMatrix([In] float[,] matrixA, [In] float[,] matrixB, [Out] float[,] resultMatrix, int rowsCount, int columnsCount);

        [DllImport("GPUMath.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void matrixSubNum([In] float[,] matrix, [In] float number, [Out] float[,] resultMatrix, int rowsCount, int columnsCount);

        [DllImport("GPUMath.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void matrixMultMatrix([In] float[,] matrixA, [In] float[,] matrixB, [Out] float[,] resultMatrix, int rowsCount, int columnsCount);

        [DllImport("GPUMath.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void matrixMultNum([In] float[,] matrix, [In] float number, [Out] float[,] resultMatrix, int rowsCount, int columnsCount);

        [DllImport("GPUMath.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void matrixDivMatrix([In] float[,] matrixA, [In] float[,] matrixB, [Out] float[,] resultMatrix, int rowsCount, int columnsCount);

        [DllImport("GPUMath.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void matrixDivNum([In] float[,] matrix, [In] float number, [Out] float[,] resultMatrix, int rowsCount, int columnsCount);

        [DllImport("GPUMath.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void matrixScalerProduct([In] float[,] matrixA, [In] float[,] matrixB, [Out] float[,] resultMatrix, int rowsA, int columnsB, int dimension);
    }
}