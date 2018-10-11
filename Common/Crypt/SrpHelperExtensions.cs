using System;
using System.Linq;
using System.Numerics;

namespace Common.Crypt
{
    public static class SrpHelperExtensions
    {
        public static byte[] ToProperByteArray(this BigInteger b)
        {
            var bytes = b.ToByteArray();
            if (b.Sign == 1 && bytes.Length > 1 && bytes[bytes.Length - 1] == 0)
                Array.Resize(ref bytes, bytes.Length - 1);
            return bytes;
        }

        public static BigInteger ToPositiveBigInteger(this byte[] bytes)
        {
            return new BigInteger(bytes.Concat(new byte[] {0}).ToArray());
        }
    }
}