﻿using BenchmarkDotNet.Attributes;

namespace QUIBIQ.QUIFLOW.SIMD.Benchmarks;

public class GreaterThanBenchmark_RandomValues
{
    private int[] data;
    [Params(1024, 8192)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        data = new int[N];
        for(int i = 0; i < N; i++)
        {
            data[i] = i;
        }
    }

    [Benchmark]
    public int GreaterThanNaive() => GreaterThan.GreaterThanNaive( N/2, new ReadOnlySpan<int>(data));

    [Benchmark]
    public int GreaterThanSimd() => GreaterThan.GreaterThanSimd(N / 2, new ReadOnlySpan<int>(data));
}