#include "amp.h"
#include <iostream>

using namespace concurrency;

//extern "C" __declspec (dllexport) void _stdcall square_array(float* arr, int n)
//{
//    // Create a view over the data on the CPU
//    array_view<float, 1> dataView(n, &arr[0]);
//
//    // Run code on the GPU
//    parallel_for_each(dataView.grid, [=](index<1> idx) mutable restrict(direct3d)
//        {
//            dataView[idx] = dataView[idx] * dataView[idx];
//        });
//
//    // Copy data from GPU to CPU
//    dataView.synchronize();
//}

const int size = 5;

void Test() {
    int aCPP[] = { 1, 2, 3, 4, 5 };
    int bCPP[] = { 6, 7, 8, 9, 10 };
    int sumCPP[size];

    // Create C++ AMP objects.
    array_view<const int, 1> a(size, aCPP);
    array_view<const int, 1> b(size, bCPP);
    array_view<int, 1> sum(size, sumCPP);
    sum.discard_data();

    parallel_for_each(
        // Define the compute domain, which is the set of threads that are created.
        sum.extent,
        // Define the code to run on each thread on the accelerator.
        [=](index<1> idx) restrict(amp) {
            sum[idx] = a[idx] + b[idx];
        }
    );

    // Print the results. The expected output is "7, 9, 11, 13, 15".
    for (int i = 0; i < size; i++) {
        std::cout << sum[i] << "\n";
    }
}