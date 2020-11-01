#include "cuda_runtime.h"
#include "device_launch_parameters.h"

#include <malloc.h>
#include <stdio.h>
#include <math.h>
constexpr auto NThreads = 1024;

__global__ void kernelMatrixAddMatrix(float* matrixA, float* matrixB, float* resultMatrix) {
	int i = blockIdx.x;
	resultMatrix[i] = matrixA[i] + matrixB[i];
}

__global__ void kernelMatrixAddNum(float* matrix, float number, float* resultMatrix) {
	int i = blockIdx.x;
	resultMatrix[i] = matrix[i] + number;
}

__global__ void kernelMatrixSubMatrix(float* matrixA, float* matrixB, float* resultMatrix) {
	int i = blockIdx.x;
	resultMatrix[i] = matrixA[i] - matrixB[i];
}

__global__ void kernelMatrixSubNum(float* matrix, float number, float* resultMatrix) {
	int i = blockIdx.x;
	resultMatrix[i] = matrix[i] - number;
}

__global__ void kernelMatrixMultMatrix(float* matrixA, float* matrixB, float* resultMatrix) {
	int i = blockIdx.x;
	resultMatrix[i] = matrixA[i] * matrixB[i];
}

__global__ void kernelMatrixMultNum(float* matrix, float number, float* resultMatrix) {
	int i = blockIdx.x;
	resultMatrix[i] = matrix[i] * number;
}

__global__ void kernelMatrixDivMatrix(float* matrixA, float* matrixB, float* resultMatrix) {
	int i = blockIdx.x;
	resultMatrix[i] = matrixA[i] / matrixB[i];
}

__global__ void kernelMatrixDivNum(float* matrix, float number, float* resultMatrix) {
	int i = blockIdx.x;
	resultMatrix[i] = matrix[i] / number;
}

__global__ void kernelMatrixScalerProduct(float* matrixA, float* matrixB, float* resultMatrix, int bColumnsCount, int dimension) {
	
	__shared__ float cache[NThreads];

	int i = blockIdx.x;
	int k = threadIdx.x;

	int x = i / bColumnsCount;
	int y = i % bColumnsCount;

	if (k < dimension) {
		cache[k] = matrixA[x * dimension + k] * matrixB[y + bColumnsCount * k];
	}
	__syncthreads();

	int n = 0;
	int iterations = log2((double)NThreads);
	int offset = NThreads / 2;

	while (n < iterations) {
		if (k < offset) {
			cache[k] += cache[offset + k];
			offset /= 2;
		}
		__syncthreads();
		n++;
	}

	resultMatrix[i] = cache[0];
}

extern "C" {
	__declspec(dllexport) void matrixAddMatrix(float* matrixA, float* matrixB, float* resultMatrix, const int rowsCount, const int columnsCount) {
		int elemsCount = rowsCount * columnsCount;
		float* dev_matrixA;
		float* dev_matrixB;
		float* dev_resultMatrix;
		cudaMalloc((void**)&dev_matrixA, sizeof(float) * elemsCount);
		cudaMalloc((void**)&dev_matrixB, sizeof(float) * elemsCount);
		cudaMalloc((void**)&dev_resultMatrix, sizeof(float) * elemsCount);
		cudaMemcpy(dev_matrixA, matrixA, sizeof(float) * elemsCount, cudaMemcpyHostToDevice);
		cudaMemcpy(dev_matrixB, matrixB, sizeof(float) * elemsCount, cudaMemcpyHostToDevice);
		kernelMatrixAddMatrix <<<elemsCount, NThreads >>> (dev_matrixA, dev_matrixB, dev_resultMatrix);
		cudaMemcpy(resultMatrix, dev_resultMatrix, sizeof(float) * elemsCount, cudaMemcpyDeviceToHost);
		cudaFree(dev_matrixA);
		cudaFree(dev_matrixB);
		cudaFree(dev_resultMatrix);
	}

	__declspec(dllexport) void matrixAddNum(float* matrix, const float number, float* resultMatrix, const int rowsCount, const int columnsCount) {
		int elemsCount = rowsCount * columnsCount;
		float* dev_matrix;
		float* dev_resultMatrix;
		cudaMalloc((void**)&dev_matrix, sizeof(float) * elemsCount);
		cudaMalloc((void**)&dev_resultMatrix, sizeof(float) * elemsCount);
		cudaMemcpy(dev_matrix, matrix, sizeof(float) * elemsCount, cudaMemcpyHostToDevice);
		kernelMatrixAddNum << <elemsCount, 1 >> > (dev_matrix, number, dev_resultMatrix);
		cudaMemcpy(resultMatrix, dev_resultMatrix, sizeof(float) * elemsCount, cudaMemcpyDeviceToHost);
		cudaFree(dev_matrix);
		cudaFree(dev_resultMatrix);
	}

	__declspec(dllexport) void matrixSubMatrix(float* matrixA, float* matrixB, float* resultMatrix, const int rowsCount, const int columnsCount) {
		int elemsCount = rowsCount * columnsCount;
		float* dev_matrixA;
		float* dev_matrixB;
		float* dev_resultMatrix;
		cudaMalloc((void**)&dev_matrixA, sizeof(float) * elemsCount);
		cudaMalloc((void**)&dev_matrixB, sizeof(float) * elemsCount);
		cudaMalloc((void**)&dev_resultMatrix, sizeof(float) * elemsCount);
		cudaMemcpy(dev_matrixA, matrixA, sizeof(float) * elemsCount, cudaMemcpyHostToDevice);
		cudaMemcpy(dev_matrixB, matrixB, sizeof(float) * elemsCount, cudaMemcpyHostToDevice);
		kernelMatrixSubMatrix << <elemsCount, 1 >> > (dev_matrixA, dev_matrixB, dev_resultMatrix);
		cudaMemcpy(resultMatrix, dev_resultMatrix, sizeof(float) * elemsCount, cudaMemcpyDeviceToHost);
		cudaFree(dev_matrixA);
		cudaFree(dev_matrixB);
		cudaFree(dev_resultMatrix);
	}

	__declspec(dllexport) void matrixSubNum(float* matrix, const float number, float* resultMatrix, const int rowsCount, const int columnsCount) {
		int elemsCount = rowsCount * columnsCount;
		float* dev_matrix;
		float* dev_resultMatrix;
		cudaMalloc((void**)&dev_matrix, sizeof(float) * elemsCount);
		cudaMalloc((void**)&dev_resultMatrix, sizeof(float) * elemsCount);
		cudaMemcpy(dev_matrix, matrix, sizeof(float) * elemsCount, cudaMemcpyHostToDevice);
		kernelMatrixSubNum <<<elemsCount, 1 >> > (dev_matrix, number, dev_resultMatrix);
		cudaMemcpy(resultMatrix, dev_resultMatrix, sizeof(float) * elemsCount, cudaMemcpyDeviceToHost);
		cudaFree(dev_matrix);
		cudaFree(dev_resultMatrix);
	}

	__declspec(dllexport) void matrixMultMatrix(float* matrixA, float* matrixB, float* resultMatrix, const int rowsCount, const int columnsCount) {
		int elemsCount = rowsCount * columnsCount;
		float* dev_matrixA;
		float* dev_matrixB;
		float* dev_resultMatrix;
		cudaMalloc((void**)&dev_matrixA, sizeof(float) * elemsCount);
		cudaMalloc((void**)&dev_matrixB, sizeof(float) * elemsCount);
		cudaMalloc((void**)&dev_resultMatrix, sizeof(float) * elemsCount);
		cudaMemcpy(dev_matrixA, matrixA, sizeof(float) * elemsCount, cudaMemcpyHostToDevice);
		cudaMemcpy(dev_matrixB, matrixB, sizeof(float) * elemsCount, cudaMemcpyHostToDevice);
		kernelMatrixMultMatrix << <elemsCount, 1 >> > (dev_matrixA, dev_matrixB, dev_resultMatrix);
		cudaMemcpy(resultMatrix, dev_resultMatrix, sizeof(float) * elemsCount, cudaMemcpyDeviceToHost);
		cudaFree(dev_matrixA);
		cudaFree(dev_matrixB);
		cudaFree(dev_resultMatrix);
	}

	__declspec(dllexport) void matrixMultNum(float* matrix, const float number, float* resultMatrix, const int rowsCount, const int columnsCount) {
		int elemsCount = rowsCount * columnsCount;
		float* dev_matrix;
		float* dev_resultMatrix;
		cudaMalloc((void**)&dev_matrix, sizeof(float) * elemsCount);
		cudaMalloc((void**)&dev_resultMatrix, sizeof(float) * elemsCount);
		cudaMemcpy(dev_matrix, matrix, sizeof(float) * elemsCount, cudaMemcpyHostToDevice);
		kernelMatrixMultNum << <elemsCount, 1 >> > (dev_matrix, number, dev_resultMatrix);
		cudaMemcpy(resultMatrix, dev_resultMatrix, sizeof(float) * elemsCount, cudaMemcpyDeviceToHost);
		cudaFree(dev_matrix);
		cudaFree(dev_resultMatrix);
	}

	__declspec(dllexport) void matrixDivMatrix(float* matrixA, float* matrixB, float* resultMatrix, const int rowsCount, const int columnsCount) {
		int elemsCount = rowsCount * columnsCount;
		float* dev_matrixA;
		float* dev_matrixB;
		float* dev_resultMatrix;
		cudaMalloc((void**)&dev_matrixA, sizeof(float) * elemsCount);
		cudaMalloc((void**)&dev_matrixB, sizeof(float) * elemsCount);
		cudaMalloc((void**)&dev_resultMatrix, sizeof(float) * elemsCount);
		cudaMemcpy(dev_matrixA, matrixA, sizeof(float) * elemsCount, cudaMemcpyHostToDevice);
		cudaMemcpy(dev_matrixB, matrixB, sizeof(float) * elemsCount, cudaMemcpyHostToDevice);
		kernelMatrixDivMatrix << <elemsCount, 1 >> > (dev_matrixA, dev_matrixB, dev_resultMatrix);
		cudaMemcpy(resultMatrix, dev_resultMatrix, sizeof(float) * elemsCount, cudaMemcpyDeviceToHost);
		cudaFree(dev_matrixA);
		cudaFree(dev_matrixB);
		cudaFree(dev_resultMatrix);
	}

	__declspec(dllexport) void matrixDivNum(float* matrix, const float number, float* resultMatrix, const int rowsCount, const int columnsCount) {
		int elemsCount = rowsCount * columnsCount;
		float* dev_matrix;
		float* dev_resultMatrix;
		cudaMalloc((void**)&dev_matrix, sizeof(float) * elemsCount);
		cudaMalloc((void**)&dev_resultMatrix, sizeof(float) * elemsCount);
		cudaMemcpy(dev_matrix, matrix, sizeof(float) * elemsCount, cudaMemcpyHostToDevice);
		kernelMatrixDivNum << <elemsCount, 1 >> > (dev_matrix, number, dev_resultMatrix);
		cudaMemcpy(resultMatrix, dev_resultMatrix, sizeof(float) * elemsCount, cudaMemcpyDeviceToHost);
		cudaFree(dev_matrix);
		cudaFree(dev_resultMatrix);
	}

	__declspec(dllexport) void matrixScalerProduct(float* matrixA, float* matrixB, float* resultMatrix, const int aRowsCount, const int bColumnsCount, const int dimension) {
		int elemsCount = aRowsCount * bColumnsCount;
		float* dev_matrixA;
		float* dev_matrixB;
		float* dev_resultMatrix;
		cudaMalloc((void**)&dev_matrixA, sizeof(float) * aRowsCount * dimension);
		cudaMalloc((void**)&dev_matrixB, sizeof(float) * bColumnsCount * dimension);
		cudaMalloc((void**)&dev_resultMatrix, sizeof(float) * elemsCount);
		cudaMemcpy(dev_matrixA, matrixA, sizeof(float) * aRowsCount * dimension, cudaMemcpyHostToDevice);
		cudaMemcpy(dev_matrixB, matrixB, sizeof(float) * bColumnsCount * dimension, cudaMemcpyHostToDevice);
		//cudaMemcpy(dev_resultMatrix, resultMatrix, sizeof(float) * elemsCount, cudaMemcpyHostToDevice);
		kernelMatrixScalerProduct << <elemsCount, dimension >> > (dev_matrixA, dev_matrixB, dev_resultMatrix, bColumnsCount, dimension);
		cudaMemcpy(resultMatrix, dev_resultMatrix, sizeof(float) * elemsCount, cudaMemcpyDeviceToHost);
		cudaFree(dev_matrixA);
		cudaFree(dev_matrixB);
		cudaFree(dev_resultMatrix);
	}
}