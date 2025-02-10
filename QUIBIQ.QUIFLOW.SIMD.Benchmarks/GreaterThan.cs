using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace QUIBIQ.QUIFLOW.SIMD.Benchmarks;

public class GreaterThan
{
    public static int GreaterThanNaive(int value, ReadOnlySpan<int> array)
    {
        int ct = 0; ;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > value)
            {
                ct++;
            }
        }
        return ct;
    }

    public static int GreaterThanSimd(int boundary, ReadOnlySpan<int> array)
    {
        var total = 0;
        var i = 0;

        if (Vector256.IsHardwareAccelerated && array.Length >= Vector256<int>.Count)
        {
            var xmm1 = Vector256.Create(boundary);
            var count = Vector256<int>.Zero;
            foreach (var vector in MemoryMarshal.Cast<int, Vector256<int>>(array))
            {
                count -= Vector256.GreaterThan(vector, xmm1);
                i += Vector256<int>.Count;
            }

            total = Vector256.Sum(count);
        }

        array = array[i..];
        i = 0;

        if (Vector128.IsHardwareAccelerated && array.Length >= Vector128<int>.Count)
        {
            var xmm1 = Vector128.Create(boundary);
            var count = Vector128<int>.Zero;
            foreach (var vector in MemoryMarshal.Cast<int, Vector128<int>>(array))
            {
                count -= Vector128.GreaterThan(vector, xmm1);
                i += Vector128<int>.Count;
            }

            total += Vector128.Sum(count);
        }

        for (; i < array.Length; i++)
        {
            if (array[i] > boundary)
                total++;
        }

        return total;
    }
}
