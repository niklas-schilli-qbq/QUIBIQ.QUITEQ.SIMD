using BenchmarkDotNet.Running;

namespace QUIBIQ.QUIFLOW.SIMD.Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<GreaterThanBenchmark_RandomValues>();
        }
    }
}
