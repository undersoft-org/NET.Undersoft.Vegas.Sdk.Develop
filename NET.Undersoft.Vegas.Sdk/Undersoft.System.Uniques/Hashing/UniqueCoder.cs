using System.Collections;
using System.Extract;
using System.Linq;
using System.Runtime.InteropServices;

namespace System.Uniques
{
    using System.Runtime.CompilerServices;

    public static class UniqueCoder64
    {
        public static unsafe Byte[] ComputeUniqueBytes(byte[] bytes, uint seed = 0)
        {
            byte[] b = new byte[8];
            fixed (byte* pb = b, pa = bytes)
            {
                *((ulong*)pb) = xxHash64.UnsafeComputeHash(pa, bytes.Length, seed);
            }
            return b;
        }

        public static unsafe Byte[] ComputeUniqueBytes(byte* bytes, int length, uint seed = 0)
        {
            byte[] b = new byte[8];
            fixed (byte* pb = b)
            {
                *((ulong*)pb) = xxHash64.UnsafeComputeHash(bytes, length, seed);
            }
            return b;
        }

        public static unsafe ulong  ComputeUniqueKey(byte[] bytes, uint seed = 0)
        {
            fixed (byte* pa = bytes)
            {
                return xxHash64.UnsafeComputeHash(pa, bytes.Length, seed);
            }
        }

        public static unsafe ulong  ComputeUniqueKey(byte* ptr, int length, ulong seed = 0)
        {
            return xxHash64.UnsafeComputeHash(ptr, length, seed);
        }
    }

    public static class UniqueCoder32
    {
        public static unsafe Byte[] ComputeUniqueBytes(byte[] bytes, uint seed = 0)
        {
            byte[] b = new byte[4];
            fixed (byte* pb = b, pa = bytes)
            {
                *((uint*)pb) = xxHash32.UnsafeComputeHash(pa, bytes.Length, seed);
            }
            return b;
        }

        public static unsafe Byte[] ComputeUniqueBytes(byte* ptr, int length, uint seed = 0)
        {
            byte[] b = new byte[4];
            fixed (byte* pb = b)
            {
                *((uint*)pb) = xxHash32.UnsafeComputeHash(ptr, length, seed);
            }
            return b;
        }

        public static unsafe uint ComputeUniqueKey(byte[] bytes, uint seed = 0)
        {
            fixed (byte* pa = bytes)
            {
                return xxHash32.UnsafeComputeHash(pa, bytes.Length, seed);
            }
        }

        public static unsafe uint ComputeUniqueKey(byte* ptr, int length, uint seed = 0)
        {
            return xxHash32.UnsafeComputeHash(ptr, length, seed);
        }
    }
}
